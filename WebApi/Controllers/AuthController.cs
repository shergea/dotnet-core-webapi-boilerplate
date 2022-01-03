using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.BLL.Logics.Interfaces;
using WebApi.Model;
using WebApi.Model.ViewModels.AuthController;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IAuthLogic _authLogic;
        private readonly IConfiguration _config;
        private readonly IRefreshTokenLogic _refreshTokenLogic;

        public AuthController(IAuthLogic authLogic, IRefreshTokenLogic refreshTokenLogic, ILogger<ValuesController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _authLogic = authLogic;
            _refreshTokenLogic = refreshTokenLogic;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult CreateToken([FromBody] PostLoginInputViewModel login)
        {
            IActionResult response = Unauthorized();
            var user = _authLogic.Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user: user);
                RefreshToken newRefreshToken = _refreshTokenLogic.CreateNewToken(user.Id, newToken: GenerateRefreshToken());
                response = new ObjectResult(new
                {
                    token = tokenString,
                    refreshToken = newRefreshToken.Token
                });
            }

            return response;
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            Guid userId = new Guid(principal.Claims.FirstOrDefault(c => c.Type == "UserId").Value.ToString());
            RefreshToken savedRefreshToken = _refreshTokenLogic.GetByToken(userId, refreshToken);
            if (savedRefreshToken == null)
                return Unauthorized();

            var newJwtToken = BuildToken(claims: principal.Claims);
            RefreshToken newRefreshToken = _refreshTokenLogic.CreateNewToken(userId, GenerateRefreshToken(), savedRefreshToken.Token);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken.Token
            });
        }


        private string BuildToken(User user = null, IEnumerable<Claim> claims = null)
        {
            IEnumerable<Claim> _claims = null;
            if (user != null)
            {
                _claims = new[] {
                    new Claim("Email", user.Email),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Jti", Guid.NewGuid().ToString())
                };
            }
            if (claims != null)
            {
                _claims = new[] {
                    new Claim("Email", claims.FirstOrDefault(x=>x.Type=="Email").Value.ToString()),
                    new Claim("UserId", claims.FirstOrDefault(x=>x.Type=="UserId").Value.ToString()),
                    new Claim("Jti", Guid.NewGuid().ToString())
                };
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                _claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        public AuthController(IAuthLogic authLogic, ILogger<ValuesController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _authLogic = authLogic;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]PostLoginInputViewModel login)
        {
            IActionResult response = Unauthorized();
            var user = _authLogic.Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }


        private string BuildToken(User user)
        {
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Logics.Interfaces;
using WebApi.Model;
using WebApi.Model.ViewModels.UserController;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IUserLogic _userLogic;
        private readonly IConfiguration _config;

        public UserController(IUserLogic userLogic, ILogger<ValuesController> logger, IConfiguration config) : base(userLogic)
        {
            _logger = logger;
            _config = config;
            _userLogic = userLogic;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public RegisterOutputViewModel Register([FromBody] RegisterInputViewModel login)
        {
            return _userLogic.Register(login);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userLogic.Get();
        }

    }
}

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
using WebApi.Model.ViewModels.UserController;

namespace WebApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        IUserLogic _userLogic;
        public BaseController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public IUserLogic userLogic
        {
            get
            {
                return _userLogic;
            }
        }

        protected new User User
        {
            get
            {
                var user = HttpContext.User;
                if(user!=null){
                    Guid id = new Guid(user.FindFirst("UserId").Value.ToString());
                    return _userLogic.GetById(id);
                }
                return null;
            }
            set
            {
            }
        }

    }
}

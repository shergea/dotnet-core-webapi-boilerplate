using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Logics.Interfaces;
using WebApi.Model;
using WebApi.Model.ViewModels.UserController;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(IUserLogic userLogic, ILogger<ValuesController> logger) : base(userLogic)
        {
            _logger = logger;
        }
        // GET api/values
        [HttpGet, Authorize]
        public GetOutputViewModel Get()
        {
            //var currentUser = HttpContext.User;
            //currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value
            User aaa = this.User;
            GetOutputViewModel a = userLogic.GetFirst();
            return a;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public System.Collections.Generic.IEnumerable<User> Get(int id)
        {
            return userLogic.GetTest();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userLogic.Delete();
        }
    }
}

using System.Collections.Generic;
using WebApi.Model;

namespace WebApi.BLL.Logics.Interfaces
{
    public interface IUserLogic
    {
        IEnumerable<User> Get();
    }
}
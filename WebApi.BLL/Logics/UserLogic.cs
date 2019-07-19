using System.Collections.Generic;
using WebApi.BLL.Logics.Interfaces;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Model;

namespace WebApi.BLL.Logics
{
    public class UserLogic : BaseLogic,IUserLogic
    {
        public UserLogic(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public IEnumerable<User> Get(){
            return this.unitOfWork.User.Get();
        }
    }
}
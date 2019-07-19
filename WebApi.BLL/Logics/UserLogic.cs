using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.BLL.Logics.Interfaces;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Model;
using WebApi.Model.ViewModels.UserController;

namespace WebApi.BLL.Logics
{
    public class UserLogic : BaseLogic,IUserLogic
    {
        public UserLogic(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork,mapper)
        {

        }
        public IEnumerable<User> Get()
        {
            return this.unitOfWork.User.GetAsNoTracking();
        }

        public GetOutputViewModel GetFirst()
        {
            return this._mapper.Map<GetOutputViewModel>(this.unitOfWork.User.GetAsNoTracking().First());
        }
    }
}
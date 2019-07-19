using AutoMapper;
using WebApi.BLL.Logics.Interfaces;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Model;
using WebApi.Model.ViewModels.AuthController;
using WebApi.BLL.Funcs;

namespace WebApi.BLL.Logics
{
    public class AuthLogic : BaseLogic, IAuthLogic
    {
        public AuthLogic(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public User Authenticate(PostLoginInputViewModel entity)
        {
            return this._unitOfWork.User.GetFirstAsNoTracking(x => x.Email == entity.Email && x.Password == CryptologyFuncs.Hash(entity.Password));
        }
    }
}
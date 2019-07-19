using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.BLL.Funcs;
using WebApi.BLL.Logics.Interfaces;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Model;
using WebApi.Model.ViewModels.UserController;

namespace WebApi.BLL.Logics
{
    public class UserLogic : BaseLogic, IUserLogic
    {
        public UserLogic(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public IEnumerable<User> Get()
        {
            return this._unitOfWork.User.GetAsNoTracking();
        }

        public GetOutputViewModel GetFirst()
        {
            return this._mapper.Map<GetOutputViewModel>(this._unitOfWork.User.GetAsNoTracking().First());
        }

        public RegisterOutputViewModel Register(RegisterInputViewModel entity)
        {
            User result = new User()
            {
                FirstName = entity.FirstName,
                Surname = entity.Surname,
                Password = CryptologyFuncs.Hash(entity.Password),
                Email = entity.Email
            };

            this._unitOfWork.User.Insert(result);
            this._unitOfWork.Save();
            return _mapper.Map<RegisterOutputViewModel>(result);
        }

        public User GetById(Guid Id)
        {
            return _unitOfWork.User.GetByIDAsNoTracking(Id);
        }
    }
}
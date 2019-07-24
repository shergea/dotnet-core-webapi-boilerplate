using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IEnumerable<User> GetTest()
        {
            //throw new Exception("Hata oldu aga");
            var task1 = _unitOfWork.RefreshToken.GetAsNoTrackingAsync();
            var task2 = _unitOfWork.User.GetAsNoTrackingAsync();
            Task.WaitAll(task1, task2);
            var result1 = task1.Result;
            var result2 = task2.Result;
            return result2;
        }

        public void Delete()
        {
            User entity = _unitOfWork.User.Get().First();
            _unitOfWork.User.Delete(entity);
            RefreshToken entity2 = _unitOfWork.RefreshToken.Get().First();
            _unitOfWork.RefreshToken.Delete(entity2);
            _unitOfWork.Save();
        }
    }
}
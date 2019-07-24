using System;
using System.Collections.Generic;
using WebApi.Model;
using WebApi.Model.ViewModels.UserController;

namespace WebApi.BLL.Logics.Interfaces
{
    public interface IUserLogic
    {
        IEnumerable<User> Get();
        GetOutputViewModel GetFirst();
        RegisterOutputViewModel Register(RegisterInputViewModel entity);
        User GetById(Guid Id);
        IEnumerable<User> GetTest();
        void Delete();
    }
}
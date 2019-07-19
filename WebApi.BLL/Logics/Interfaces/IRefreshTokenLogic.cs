using System;
using System.Collections.Generic;
using WebApi.Model;
using WebApi.Model.ViewModels.UserController;

namespace WebApi.BLL.Logics.Interfaces
{
    public interface IRefreshTokenLogic
    {
        RefreshToken GetByToken(Guid userId,string token);
        RefreshToken CreateNewToken(Guid userId, string newToken, string oldToken = null);
    }
}
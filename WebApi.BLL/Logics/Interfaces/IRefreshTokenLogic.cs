using System;
using WebApi.Model;

namespace WebApi.BLL.Logics.Interfaces
{
    public interface IRefreshTokenLogic
    {
        RefreshToken GetByToken(Guid userId, string token);
        RefreshToken CreateNewToken(Guid userId, string newToken, string oldToken = null);
    }
}
﻿using System;
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
    public class RefreshTokenLogic : BaseLogic, IRefreshTokenLogic
    {
        public RefreshTokenLogic(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public RefreshToken GetByToken(Guid userId, string token)
        {
            return this._unitOfWork.RefreshToken.GetFirstAsNoTracking(x => x.UserId == userId && x.Token == token && x.ExpiredTime >= DateTime.Now);
        }

        public RefreshToken CreateNewToken(Guid userId, string newToken, string oldToken = null)
        {
            if (oldToken != null)
            {
                RefreshToken oldRefreshToken = this._unitOfWork.RefreshToken.GetFirstAsNoTracking(x => x.Token == oldToken);
                this._unitOfWork.RefreshToken.Delete(oldRefreshToken);
            }

            RefreshToken result = new RefreshToken()
            {
                Token = newToken,
                UserId = userId,
                IssuedTime = DateTime.Now,
                ExpiredTime = DateTime.Now.AddMinutes(60)
            };
            this._unitOfWork.RefreshToken.Insert(result);
            this._unitOfWork.Save();
            return result;
        }
    }
}
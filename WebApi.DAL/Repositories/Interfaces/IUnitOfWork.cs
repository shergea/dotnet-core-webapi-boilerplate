using System;

namespace WebApi.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IUserRepository User { get; }
    }
}
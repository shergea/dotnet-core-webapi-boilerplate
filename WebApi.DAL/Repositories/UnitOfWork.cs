using System;
using WebApi.DAL.Repositories.Interfaces;

namespace WebApi.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private MsSQLContext context;
        public UnitOfWork(MsSQLContext _context)
        {
            context = _context;
        }
        
        private UserRepository userRepository;
        private RefreshTokenRepository refreshTokenRepository;

        public IUserRepository User
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public IRefreshTokenRepository RefreshToken
        {
            get
            {

                if (this.refreshTokenRepository == null)
                {
                    this.refreshTokenRepository = new RefreshTokenRepository(context);
                }
                return refreshTokenRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
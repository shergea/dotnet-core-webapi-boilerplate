using Microsoft.Extensions.Caching.Distributed;
using WebApi.DAL.Repositories.Interfaces;

namespace WebApi.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private MsSQLContext context;
        private IDistributedCache distributedCache;
        public UnitOfWork(MsSQLContext _context, IDistributedCache _distributedCache)
        {
            context = _context;
            distributedCache = _distributedCache;
        }

        private UserRepository userRepository;
        private RefreshTokenRepository refreshTokenRepository;

        public IUserRepository User
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context, distributedCache);
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
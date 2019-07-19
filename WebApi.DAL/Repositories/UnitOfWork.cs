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
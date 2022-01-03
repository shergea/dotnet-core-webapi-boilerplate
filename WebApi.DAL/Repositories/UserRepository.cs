using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Model;

namespace WebApi.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MsSQLContext context, IDistributedCache distributedCache) : base(context, distributedCache)
        {
        }

        public User GetForLogin(string email, string password)
        {
            return context.Users.AsNoTracking().Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
    }
}
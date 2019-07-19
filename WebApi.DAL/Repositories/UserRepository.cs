using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Model;

namespace WebApi.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MsSQLContext context) : base(context)
        {
        }
    }
}
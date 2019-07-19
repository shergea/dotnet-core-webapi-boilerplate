using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Model;

namespace WebApi.DAL.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(MsSQLContext context) : base(context)
        {
        }
    }
}
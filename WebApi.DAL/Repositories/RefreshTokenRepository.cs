using WebApi.DAL.Repositories.Interfaces;
using WebApi.Model;

namespace WebApi.DAL.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(MsSQLContext context) : base(context)
        {
        }

        public RefreshToken GetValidRefreshToken(Guid userId, string token)
        {
            return context.RefreshTokens.Where(x => x.UserId == userId && x.Token == token && x.ExpiredTime >= DateTime.Now).FirstOrDefault();
        }
    }
}
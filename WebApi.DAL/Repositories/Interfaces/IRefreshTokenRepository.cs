using WebApi.Model;

namespace WebApi.DAL.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        RefreshToken GetValidRefreshToken(Guid userId, string token);
    }
}
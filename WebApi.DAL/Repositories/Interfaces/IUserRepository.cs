using WebApi.Model;

namespace WebApi.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetForLogin(string email, string password);
    }
}
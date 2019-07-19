using System.Collections.Generic;
using WebApi.Model;

namespace WebApi.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByEmail(string email);
        IEnumerable<User> GetWithoutCostumer();
        void IncreaseLoginErrorCount(User currentUser);
        void ResetLoginErrorCount(User currentUser);
        User GetByIdAsNoTracking(int id);
        User GetMe(int id);
        IEnumerable<User> GetByType(string type);
        bool hasPermission(string v, User user);
    }
}
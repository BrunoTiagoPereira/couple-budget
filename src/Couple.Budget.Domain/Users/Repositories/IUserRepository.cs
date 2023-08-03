using Couple.Budget.Core.Data.Repositories;
using Couple.Budget.Domain.Users.Entities;

namespace Couple.Budget.Domain.Users.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UserNameIsTakenAsync(string userName);

        Task<User?> FindByUserNameAsync(string userName);
    }
}
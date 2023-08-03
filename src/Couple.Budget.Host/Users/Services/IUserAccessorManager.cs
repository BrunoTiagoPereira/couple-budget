using Couple.Budget.Domain.Users.Entities;

namespace Couple.Budget.Host.Users.Services
{
    public interface IUserAccessorManager
    {
        Guid GetCurrentUserId();
        Task<User> GetCurrentUser();
    }
}

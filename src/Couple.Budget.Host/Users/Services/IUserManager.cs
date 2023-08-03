using Couple.Budget.Domain.Users.Entities;

namespace Couple.Budget.Host.Users.Services
{
    public interface IUserManager
    {
        string GenerateToken(User user);
    }
}
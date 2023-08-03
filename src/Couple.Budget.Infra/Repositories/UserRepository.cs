using Couple.Budget.Core.Data.Repositories;
using Couple.Budget.Domain.Users.Entities;
using Couple.Budget.Domain.Users.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Couple.Budget.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override Task<User?> FindAsync(Guid id)
        {
            return Set
                .Include(x => x.UserPreference)
                .ThenInclude(x => x.UserPreferenceDaysOfWeeks)
                .FirstOrDefaultAsync(x => x.Id == id)
                ;
        }

        public Task<bool> UserNameIsTakenAsync(string userName)
        {
            return Set.AnyAsync(x => x.UserName == userName);
        }

        public Task<User?> FindByUserNameAsync(string userName)
        {
            return Set
                .SingleOrDefaultAsync(x => x.UserName == userName)
                ;
        }
    }
}
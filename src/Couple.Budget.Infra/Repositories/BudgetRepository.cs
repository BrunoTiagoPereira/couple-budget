using Couple.Budget.Core.Data.Repositories;
using Couple.Budget.Domain.Budgets.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Couple.Budget.Infra.Repositories
{
    public class BudgetRepository : Repository<Domain.Budgets.Entities.Budget>, IBudgetRepository
    {
        public BudgetRepository(DbContext context) : base(context)
        {
        }

        public override Task<Domain.Budgets.Entities.Budget?> FindAsync(Guid id)
        {
            return Set
                .Include(x => x.User)
                .Include(x => x.BudgetDays)
                .ThenInclude(x => x.Expenses)
                .Include(x => x.Suggests)
                .FirstOrDefaultAsync(x => x.Id == id);
                ;
        }

        public Task<Domain.Budgets.Entities.Budget[]> GetBudgetsFromUser(Guid userId)
        {
            return Set
                .Where(x => x.UserId == userId)
                .ToArrayAsync();
        }
    }
}
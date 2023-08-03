using Couple.Budget.Core.Data.Repositories;

namespace Couple.Budget.Domain.Budgets.Repositories
{
    public interface IBudgetRepository : IRepository<Entities.Budget>
    {
        Task<Entities.Budget[]> GetBudgetsFromUser(Guid userId);
    }
}
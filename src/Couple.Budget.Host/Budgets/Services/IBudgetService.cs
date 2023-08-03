using Couple.Budget.Host.Budget.Commands.Requests;
using Couple.Budget.Host.Budget.Commands.Responses;
using Couple.Budget.Host.Budget.Queries.Requests;
using Couple.Budget.Host.Budget.Queries.Responses;

namespace Couple.Budget.Host.Budgets.Services
{
    public interface IBudgetService
    {
        public Task<CreateBudgetCommandResponse> CreateBudget(CreateBudgetCommandRequest request);
        public Task<GetBudgetsQueryResponse> GetBudgets(GetBudgetsQueryRequest request);
        public Task<GetBudgetQueryResponse> GetBudget(GetBudgetQueryRequest request);
        public Task<UpdateBudgetCommandResponse> UpdateBudget(UpdateBudgetCommandRequest request);
        public Task<DeleteBudgetCommandResponse> DeleteBudget(DeleteBudgetCommandRequest request);
    }
}

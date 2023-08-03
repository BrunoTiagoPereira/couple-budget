using Couple.Budget.Host.ApiResponses;
using Couple.Budget.Host.Budget.Commands.Requests;
using Couple.Budget.Host.Budget.Queries.Requests;
using Couple.Budget.Host.Budgets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Couple.Budget.Host.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/budgets")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService ?? throw new ArgumentNullException(nameof(budgetService));
        }

        [HttpPost]
        [Route("")]
        public async Task<ApiResponse> CreateBudget(CreateBudgetCommandRequest request)
        {
            return ApiResponse.Success(await _budgetService.CreateBudget(request));
        }

        [HttpGet]
        [Route("")]
        public async Task<ApiResponse> GetBudgets()
        {
            return ApiResponse.Success(await _budgetService.GetBudgets(new GetBudgetsQueryRequest()));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ApiResponse> GetBudget([FromRoute] Guid id)
        {
            return ApiResponse.Success(await _budgetService.GetBudget(new GetBudgetQueryRequest { BudgetId = id }));
        }

        [HttpPut]
        [Route("")]
        public async Task<ApiResponse> UpdateBudget([FromBody] UpdateBudgetCommandRequest request)
        {
            return ApiResponse.Success(await _budgetService.UpdateBudget(request));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ApiResponse> DeleteBudget([FromRoute] Guid id)
        {
            return ApiResponse.Success(await _budgetService.DeleteBudget(new DeleteBudgetCommandRequest { BudgetId = id }));
        }
    }
}
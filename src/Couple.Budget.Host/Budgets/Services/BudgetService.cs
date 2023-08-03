using Couple.Budget.Core.Exceptions;
using Couple.Budget.Core.Services;
using Couple.Budget.Core.Transaction;
using Couple.Budget.Domain.Budgets.Entities;
using Couple.Budget.Domain.Budgets.Repositories;
using Couple.Budget.Domain.Users.Entities;
using Couple.Budget.Host.Budget.Commands.Requests;
using Couple.Budget.Host.Budget.Commands.Responses;
using Couple.Budget.Host.Budget.Queries.Requests;
using Couple.Budget.Host.Budget.Queries.Responses;
using Couple.Budget.Host.Users.Services;

namespace Couple.Budget.Host.Budgets.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IUserAccessorManager _userAccessorManager;
        private readonly IDatesManager _datesManager;
        private readonly IUnitOfWork _uow;
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IUserAccessorManager userAccessorManager, IDatesManager datesManager, IUnitOfWork uow, IBudgetRepository budgetRepository)
        {
            _userAccessorManager = userAccessorManager ?? throw new ArgumentNullException(nameof(userAccessorManager));
            _datesManager = datesManager ?? throw new ArgumentNullException(nameof(datesManager));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _budgetRepository = budgetRepository ?? throw new ArgumentNullException(nameof(budgetRepository));
        }

        public async Task<CreateBudgetCommandResponse> CreateBudget(CreateBudgetCommandRequest request)
        {
            var currentUser = await _userAccessorManager.GetCurrentUser();

            var budget = new Domain.Budgets.Entities.Budget(currentUser, request.Month, request.Value);

            AddBudgetDaysToBudget(budget, currentUser);

            await _budgetRepository.AddAsync(budget);
            await _uow.CommitAsync();

            return new CreateBudgetCommandResponse
            {
                BudgetId = budget.Id
            };
        }

        private void AddBudgetDaysToBudget(Domain.Budgets.Entities.Budget budget, User currentUser)
        {
            var userPreferredDaysOfWeek = currentUser.UserPreference.UserPreferenceDaysOfWeeks.Select(x => (DayOfWeek)x.DayOfWeek);
            var budgetDatesInMonth = _datesManager.GetAllDaysOfWeekDatesFromMonth(budget.Month.MonthNumber, userPreferredDaysOfWeek);

            foreach (var budgetDate in budgetDatesInMonth)
            {
                budget.AddBudgetDay(new BudgetDay(budget, budgetDate, $"Dia {budgetDate:dd}", 0));
            }
        }

        public async Task<DeleteBudgetCommandResponse> DeleteBudget(DeleteBudgetCommandRequest request)
        {
            var currentUserId = _userAccessorManager.GetCurrentUserId();
            var budget = await _budgetRepository.FindAsync(request.BudgetId);

            if (budget is null)
            {
                throw new ValidationException("O orçamento não existe.");
            }

            if (budget.UserId != currentUserId)
            {
                throw new ValidationException("O usuário não é o mesmo do orçamento.");
            }

            _budgetRepository.Remove(budget);
            await _uow.CommitAsync();

            return new DeleteBudgetCommandResponse();
        }
        public async Task<GetBudgetsQueryResponse> GetBudgets(GetBudgetsQueryRequest request)
        {
            var currentUserId = _userAccessorManager.GetCurrentUserId();
            var budgets = await _budgetRepository.GetBudgetsFromUser(currentUserId);

            return new GetBudgetsQueryResponse
            {
                Budgets = budgets.OrderByDescending(x => x.CreatedAt).Select(budget =>
                {
                    return new GetBudgetsBudgetQueryResponse
                    {
                        Id = budget.Id,
                        Month = budget.Month.MonthNumber,
                        MonthName = budget.Month.MonthName,
                        Value = budget.Value,
                    };
                })
            };
        }

        public async Task<GetBudgetQueryResponse> GetBudget(GetBudgetQueryRequest request)
        {
            var budget = await _budgetRepository.FindAsync(request.BudgetId);

            if (budget is null)
            {
                throw new ValidationException("O orçamento não existe.");
            }

            var budgetDays = budget.BudgetDays.OrderBy(x => x.Date).Select(x =>
            {
                return new GetBudgetBudgetDayQueryResponse
                {
                    Name = x.Name,
                    Value = x.Value,
                    Date = x.Date,
                    Expenses = x.Expenses.Select(y => new GetBudgetBudgetDayExpenseQueryResponse
                    {
                        Name = y.Name,
                        Value = y.Value,
                    })
                };
            });

            return new GetBudgetQueryResponse
            {
                Budget = new GetBudgetBudgetQueryResponse
                {
                    Id = budget.Id,
                    BudgetDays = budgetDays,
                    Suggests = budget.Suggests.OrderBy(x => x.Name).Select(x => new GetBudgetSuggestQueryResponse { Name = x.Name, Value = x.Value }),
                    Month = budget.Month.MonthNumber,
                    MonthName = budget.Month.MonthName,
                    Value = budget.Value,
                }
            };
        }

        public async Task<UpdateBudgetCommandResponse> UpdateBudget(UpdateBudgetCommandRequest request)
        {
            var currentUserId = _userAccessorManager.GetCurrentUserId();
            var budget = await _budgetRepository.FindAsync(request.Budget.Id);

            if (budget is null)
            {
                throw new ValidationException("O orçamento não existe.");
            }

            if (budget.UserId != currentUserId)
            {
                throw new ValidationException("O usuário não é o mesmo do orçamento.");
            }

            if(budget.BudgetDays.Sum(x => x.Value) > request.Budget.Value)
            {
                throw new ValidationException("O valor dos dias está excendendo o valor do orçamento.");
            }

            budget.UpdateMonth(request.Budget.Month);
            budget.UpdateValue(request.Budget.Value); 

            budget.ClearBudgetDays();
            budget.ClearSuggests();

            var budgetDays = request.Budget.BudgetDays.Select(x =>
            {
                var budgetDay = new BudgetDay(budget, x.Date, x.Name, x.Value);
                var expenses = x.Expenses.Select(x => new BudgetDayExpense(budgetDay, x.Name, x.Value));
                budgetDay.AddExpenses(expenses);

                return budgetDay;
            });

            budget.AddBudgetDays(budgetDays);

            var suggests = request.Budget.Suggests.Select(x => new Suggest(budget, x.Name, x.Value));
            budget.AddSuggests(suggests);

            _budgetRepository.Update(budget);
            await _uow.CommitAsync();

            return new UpdateBudgetCommandResponse();
        }
    }
}
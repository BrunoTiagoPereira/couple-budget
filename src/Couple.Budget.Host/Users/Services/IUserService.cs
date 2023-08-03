using Couple.Budget.Host.Users.Commands.Requests;
using Couple.Budget.Host.Users.Commands.Responses;
using Couple.Budget.Host.Users.Queries.Requests;
using Couple.Budget.Host.Users.Queries.Responses;

namespace Couple.Budget.Host.Users.Services
{
    public interface IUserService
    {
        Task<CreateUserCommandResponse> CreateUser(CreateUserCommandRequest request);
        Task<LoginQueryResponse> Login(LoginQueryRequest request);
    }
}

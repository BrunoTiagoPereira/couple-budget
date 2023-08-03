using Couple.Budget.Core.Exceptions;
using Couple.Budget.Core.Transaction;
using Couple.Budget.Core.ValueObjects;
using Couple.Budget.Domain.Users.Entities;
using Couple.Budget.Domain.Users.Repositories;
using Couple.Budget.Host.Users.Commands.Requests;
using Couple.Budget.Host.Users.Commands.Responses;
using Couple.Budget.Host.Users.Queries.Requests;
using Couple.Budget.Host.Users.Queries.Responses;

namespace Couple.Budget.Host.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly IUserManager _userManager;

        public UserService(IUserRepository userRepository, IUnitOfWork uow, IUserManager userManager)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<CreateUserCommandResponse> CreateUser(CreateUserCommandRequest request)
        {
            var userNameIsTaken = await _userRepository.UserNameIsTakenAsync(request.UserName);

            if (userNameIsTaken)
            {
                throw new ValidationException("O nome de usuário está em uso.");
            }

            if (request.PreferredDaysOfWeek is null || !request.PreferredDaysOfWeek.Any())
            {
                throw new ValidationException("É necessário preencher ao menos um dia da semana para a criação dos orçamentos");
            }

            await _userRepository.AddAsync(new User(request.UserName, request.Password, request.PreferredDaysOfWeek));
            await _uow.CommitAsync();

            return new CreateUserCommandResponse();
        }

        public async Task<LoginQueryResponse> Login(LoginQueryRequest request)
        {
            var user = await _userRepository.FindByUserNameAsync(request.UserName);

            if (user is null)
            {
                throw new ValidationException("Usuário não encontrado.");
            }

            var isPasswordInvalid = new Password(request.Password) != user.Password;

            if (isPasswordInvalid)
            {
                throw new ValidationException("Usuário não encontrado.");
            }

            return new LoginQueryResponse { Token = _userManager.GenerateToken(user) };
        }
    }
}
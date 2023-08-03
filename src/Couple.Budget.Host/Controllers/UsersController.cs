using Couple.Budget.Host.ApiResponses;
using Couple.Budget.Host.Users.Commands.Requests;
using Couple.Budget.Host.Users.Queries.Requests;
using Couple.Budget.Host.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace Couple.Budget.Host.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [Route("")]
        public async Task<ApiResponse> CreateUserAsync([FromBody] CreateUserCommandRequest request)
        {
            return ApiResponse.Success(await _userService.CreateUser(request));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ApiResponse> LoginAsync([FromBody] LoginQueryRequest request)
        {
            var response = await _userService.Login(request);

            return ApiResponse.Success(response.Token);
        }
    }
}
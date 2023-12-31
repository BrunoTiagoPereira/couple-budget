﻿using Couple.Budget.Domain.Users.Entities;
using Couple.Budget.Domain.Users.Repositories;
using System.Security.Claims;

namespace Couple.Budget.Host.Users.Services
{
    public class UserAccessorManager : IUserAccessorManager
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserRepository _userRepository;

        public UserAccessorManager(IHttpContextAccessor accessor, IUserRepository userRepository)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Task<User> GetCurrentUser()
        {
            return _userRepository.FindAsync(GetCurrentUserId());
        }

        public Guid GetCurrentUserId()
        {
            return Guid.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
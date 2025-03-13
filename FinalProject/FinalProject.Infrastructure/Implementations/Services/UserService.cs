using FinalProject.Application.Abstractions.Services;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FinalProject.Infrastructure.Implementations.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }
    }
}

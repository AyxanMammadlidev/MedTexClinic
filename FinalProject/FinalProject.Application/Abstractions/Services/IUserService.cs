using System.Security.Claims;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal user);
    }
}

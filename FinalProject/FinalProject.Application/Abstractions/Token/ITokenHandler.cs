using FinalProject.Application.DTOs.Tokens;
using FinalProject.Domain.Entities;
using System.Security.Claims;

namespace FinalProject.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Task<TokenResponseDto> CreateAccessToken(AppUser user, int minutes, List<Claim> claims);
        string CreateRefreshToken();
        Task<List<Claim>> CreateClaimsAsync(AppUser user);


    }
}

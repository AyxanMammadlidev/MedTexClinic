using FinalProject.Application.Abstractions.Token;
using FinalProject.Application.DTOs.Tokens;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinalProject.Infrastructure.Implementations.Token
{
    public class TokenService : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration configuration,
            UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<List<Claim>> CreateClaimsAsync(AppUser user)
        {

            var claims = new List<Claim>
        {


            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)


        };
            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            }

            return claims;
        }
        public async Task<TokenResponseDto> CreateAccessToken(AppUser user, int minutes, List<Claim> claims)
        {
            var secretKey = _configuration["JWT:SecretKey"]
        ?? throw new InvalidOperationException("JWT:SecretKey configuration is missing");
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));


            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);


            claims = await CreateClaimsAsync(user);

            var expiration = DateTime.UtcNow.AddMinutes(minutes);


            JwtSecurityToken securityToken = new(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
            );


            JwtSecurityTokenHandler tokenHandler = new();


            return new TokenResponseDto(tokenHandler.WriteToken(securityToken),
                 securityToken.ValidTo, CreateRefreshToken(), securityToken.ValidTo.AddDays(7));



        }


        public string CreateRefreshToken()
        {
            byte[] randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }


    }
}


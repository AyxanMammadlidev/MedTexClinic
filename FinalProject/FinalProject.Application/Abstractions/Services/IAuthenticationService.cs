
using FinalProject.Application.DTOs.Account;
using FinalProject.Application.DTOs.Tokens;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
        Task RegisterAsync(RegisterDto registerDto);
        Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<TokenResponseDto> LoginWithRefreshToken(string refreshToken);
        Task Logout(string userId);


    }
}

namespace FinalProject.Application.DTOs.Tokens
{
    public record TokenResponseDto(string Token, DateTime Expire, string RefreshToken, DateTime RefreshTokenExpireTime);

}

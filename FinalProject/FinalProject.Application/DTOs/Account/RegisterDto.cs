namespace FinalProject.Application.DTOs.Account
{
    public record RegisterDto(string Name,
        string Surname,
        string UserName,
        string Password,
        string Email,
        string PhoneNumber,
        bool IsAgree);

}

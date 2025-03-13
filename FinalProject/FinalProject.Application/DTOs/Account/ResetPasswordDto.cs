namespace FinalProject.Application.DTOs.Account
{
    public record ResetPasswordDto(

     string ResetCode,
     string NewPassword

        );
}

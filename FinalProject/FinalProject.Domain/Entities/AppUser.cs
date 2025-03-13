using Microsoft.AspNetCore.Identity;

namespace FinalProject.Domain.Entities
{
    public class AppUser : IdentityUser
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public string? ResetCode { get; set; }
        public DateTime? ResetCodeExpireTime { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireTime { get; set; }

        public ICollection<Comment>? Comments { get; set; }

    }
}

using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Patient : BaseNameableEntity
    {
        public string IdentityCode { get; set; }
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }




    }
}

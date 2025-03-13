using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Department : BaseNameableEntity
    {
        public int? ChiefDoctorId { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    }
}

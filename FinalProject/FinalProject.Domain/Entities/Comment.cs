using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        // Relational
        public int DoctorId { get; set; }
        public string UserId { get; set; }
    }
}

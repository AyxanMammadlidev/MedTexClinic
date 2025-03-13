using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int ProductId { get; set; }
        public int Rating { get; set; }
        //public Product Product { get; set; }
        public string UserId { get; set; }

        public string? Content { get; set; }
    }
}

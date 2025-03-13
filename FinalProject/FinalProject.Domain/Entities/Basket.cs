using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public ICollection<BasketItem>? Items { get; set; }
    }
}

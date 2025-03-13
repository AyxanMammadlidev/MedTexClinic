using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

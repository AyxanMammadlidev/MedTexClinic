using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Category : BaseNameableEntity
    {
        public ICollection<Product>? Products { get; set; }
    }
}

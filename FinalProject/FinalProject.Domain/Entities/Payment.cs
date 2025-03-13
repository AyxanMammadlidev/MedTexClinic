using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public long? Amount { get; set; }
        public string Currency { get; set; }

    }
}
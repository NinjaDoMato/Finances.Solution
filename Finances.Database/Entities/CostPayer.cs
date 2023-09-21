using Finances.Database.Enums;

namespace Finances.Database.Entities;

public class CostPayer : BaseEntity
{
    public AccountUser AccountUser { get; set; }
    public decimal PaymentPercentage { get; set; }

    public Guid CostId { get; set; }

    public Cost Cost { get; set; } = new Cost();
}

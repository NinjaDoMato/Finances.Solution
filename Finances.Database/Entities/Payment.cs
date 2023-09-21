namespace Finances.Database.Entities;

public class Payment : BaseEntity
{
    public decimal PaidAmount { get; set; }

    public Guid CostId { get; set; }

    public Cost Cost { get; set; } = new();
}

using Finances.Database.Enums;

namespace Finances.Database.Entities;

/// <summary>
/// Fixed costs by time.
/// </summary>
public class Cost : BaseEntity
{
    public decimal Amount { get; set; }
    public CostType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public IList<CostPayer> CostPayers { get; set; } = new List<CostPayer>();
    public IList<Payment> Payments { get; set; } = new List<Payment>();
}

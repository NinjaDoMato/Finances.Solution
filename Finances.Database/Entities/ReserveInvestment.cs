namespace Finances.Database.Entities;

public class ReserveInvestment : BaseEntity
{
    public Guid ReserveId { get; set; }
    public Guid InvestmentId { get; set; }
    public decimal Amount { get; set; }

    public Reserve Reserve { get; set; } = new();
    public Investment Investment { get; set; } = new();
}

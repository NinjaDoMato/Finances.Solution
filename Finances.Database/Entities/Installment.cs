namespace Finances.Database.Entities;

/// <summary>
/// Installment of the purchase.
/// </summary>
public class Installment : BaseEntity
{
    public decimal Amount { get; set; }
    public int InstallmentNumber { get; set; }
    public bool Paid { get; set; }
    public DateTime DueDate { get; set; }
    public string PaymentUrl { get; set; } = string.Empty;


    public Purchase Purchase { get; set; } = new();
}

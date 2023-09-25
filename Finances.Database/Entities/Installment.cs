using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Finances.Database.Entities;

/// <summary>
/// Installment of the purchase.
/// </summary>
public class Installment : BaseEntity
{
    [DisplayName("Valor")]
    public decimal Amount { get; set; }
    
    [DisplayName("Número da parcela")]
    public int InstallmentNumber { get; set; }
    
    [DisplayName("Pago")]
    public bool Paid { get; set; }

    [DisplayName("Vencimento")]
    public DateTime DueDate { get; set; }

    [DisplayName("DataPagamento")]
    public DateTime? PaidDate { get; set; }

    [DisplayName("Url pagamento")]
    public string PaymentUrl { get; set; } = string.Empty;


    public Purchase Purchase { get; set; } = new();
}

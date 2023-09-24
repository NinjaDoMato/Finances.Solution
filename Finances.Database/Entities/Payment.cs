using System.ComponentModel;

namespace Finances.Database.Entities;

public class Payment : BaseEntity
{
    [DisplayName("Valor Pago")]
    public decimal PaidAmount { get; set; }

    [DisplayName("Date de Pagamento")]
    public DateTime DatePaid { get; set; }

    public Guid CostId { get; set; }

    public Cost Cost { get; set; } = new();
}

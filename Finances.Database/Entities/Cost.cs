using Finances.Database.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Finances.Database.Entities;

/// <summary>
/// Fixed costs by time.
/// </summary>
public class Cost : BaseEntity
{
    [DisplayName("Valor")]
    public decimal Amount { get; set; }
    [DisplayName("Período")]
    public CostType Type { get; set; }
    [DisplayName("Nome")]
    public string Name { get; set; } = string.Empty;
    [DisplayName("Descrição")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Percentual Mi Moço")]
    [Range(0, 100)]
    public decimal DanielPercentage { get; set; }

    [DisplayName("Percentual Mi Moça")]
    [Range(0, 100)]
    public decimal CassiaPercentage { get; set; }

    [DisplayName("Reserva")]
    public Guid? ReserveId { get; set; }

    [DisplayName("Reserva")]
    public Reserve? Reserve { get; set; }

    [DisplayName("Pagamentos")]
    public IList<Payment> Payments { get; set; } = new List<Payment>();
}

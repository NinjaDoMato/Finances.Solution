using System.ComponentModel;

namespace Finances.Database.Entities;

/// <summary>
/// Representation of the amount added or subtracted from the Reserve.
/// </summary>
public class Entry : BaseEntity
{
    [DisplayName("Valor")]
    public decimal Amount { get; set; }

    [DisplayName("Observação")]
    public string Observation { get; set; } = string.Empty;
    public Guid ReserveId { get; set; }


    [DisplayName("Reserva")]
    public Reserve Reserve { get; set; } = new();
}

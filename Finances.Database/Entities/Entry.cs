using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Database.Entities;

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

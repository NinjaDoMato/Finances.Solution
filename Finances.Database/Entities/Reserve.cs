using Finances.Database.Enums;
using System.ComponentModel;

namespace Finances.Database.Entities;

public class Reserve : BaseEntity
{
    [DisplayName("Nome")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Descrição")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Proprietário")]
    public ReserveOwner Owner { get; set; }

    [DisplayName("Meta")]
    public decimal Goal { get; set; }

    public IList<Entry> Entries { get; set; } = new List<Entry>();
    public IList<ReserveInvestment> LinkedInvestments { get; set; } = new List<ReserveInvestment>();
}

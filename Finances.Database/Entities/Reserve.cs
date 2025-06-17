using Finances.Database.Enums;
using System.ComponentModel;

namespace Finances.Database.Entities;

/// <summary>
/// Representation of the money reserves, each one must have a goal amount.
/// </summary>
public class Reserve : BaseEntity
{
    [DisplayName("Nome")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Descrição")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Proprietário")]
    public AccountUser Owner { get; set; }

    [DisplayName("Meta")]
    public decimal Goal { get; set; }

    [DisplayName("Cor de Fundo")]
    public string DisplayColor { get; set; } = string.Empty;

    [DisplayName("Meta Mensal")]
    public decimal? MonthlyGoal { get; set; } = 0;

    public IList<Entry> Entries { get; set; } = new List<Entry>();
    public IList<ReserveInvestment> LinkedInvestments { get; set; } = new List<ReserveInvestment>();
}

using Finances.Database.Enums;
using System.ComponentModel;

namespace Finances.Database.Entities;

/// <summary>
/// Representation of the Purchases that have been paid in installments.
/// </summary>
public class Purchase : BaseEntity
{
    [DisplayName("Nome")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Url")]
    public string ProductUrl { get; set; } = string.Empty;
    
    [DisplayName("Proprietário")]
    public AccountUser Owner { get; set; }

    [DisplayName("Parcelas")]
    public IList<Installment> Installments { get; set; } = new List<Installment>();
}

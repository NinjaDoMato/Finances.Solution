namespace Finances.Database.Entities;

/// <summary>
/// Representation of the Purchases that have been paid in installments.
/// </summary>
public class Purchase : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = string.Empty;

    public IList<Installment> Installments { get; set; } = new List<Installment>();
}

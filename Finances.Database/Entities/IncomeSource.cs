using Finances.Database.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Finances.Database.Entities;

/// <summary>
/// Income sources for the family.
/// </summary>
public class IncomeSource : BaseEntity
{
    [DisplayName("Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Valor")]
    [Required(ErrorMessage = "O valor é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public decimal Amount { get; set; }

    [DisplayName("Proprietário")]
    [Required(ErrorMessage = "O proprietário é obrigatório")]
    public AccountUser Owner { get; set; }

    [DisplayName("Descrição")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Ativo")]
    public bool IsActive { get; set; } = true;
} 
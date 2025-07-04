using Finances.Database.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Finances.APP.Models.IncomeSource;

public class EditIncomeSourceViewModel
{
    public Guid Id { get; set; }

    [DisplayName("Nome")]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Valor")]
    [Required(ErrorMessage = "O valor é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public decimal Amount { get; set; }

    [DisplayName("Proprietário")]
    [Required(ErrorMessage = "O proprietário é obrigatório")]
    public AccountUser Owner { get; set; }

    [DisplayName("Descrição")]
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Ativo")]
    public bool IsActive { get; set; }
} 
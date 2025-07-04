using Finances.Database.Enums;
using System.ComponentModel;

namespace Finances.APP.Models.IncomeSource;

public class IncomeSourceViewModel
{
    public Guid Id { get; set; }

    [DisplayName("Nome")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Valor")]
    public decimal Amount { get; set; }

    [DisplayName("Proprietário")]
    public AccountUser Owner { get; set; }

    [DisplayName("Descrição")]
    public string Description { get; set; } = string.Empty;

    [DisplayName("Ativo")]
    public bool IsActive { get; set; }

    [DisplayName("Data de Criação")]
    public DateTime DateCreated { get; set; }

    [DisplayName("Última Atualização")]
    public DateTime LastUpdate { get; set; }
} 
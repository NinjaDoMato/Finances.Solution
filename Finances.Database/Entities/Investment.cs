using Finances.Database.Enums;
using System.ComponentModel;

namespace Finances.Database.Entities;

public class Investment : BaseEntity
{
    [DisplayName("Nome")]
    public string Name { get; set; } = string.Empty;
    
    [DisplayName("Valor inicial")]
    public decimal StartAmount { get; set; }
    
    [DisplayName("Valor atual")]
    public decimal CurrentAmount { get; set; }
    
    [DisplayName("Rentabilidade")]
    public decimal Rentability { get; set; }
    
    [DisplayName("Tipo")]
    public InvestmentType Type { get; set; }
   
    [DisplayName("Conta")]
    public AccountType Account { get; set; }
    public IList<ReserveInvestment> SourceReserves { get; set; } = new List<ReserveInvestment>();
}

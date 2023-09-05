using Finances.Database.Enums;
using System.ComponentModel;

namespace Finances.APP.Models.Investment
{
    public class InvestmentCreateViewModel
    {
        // Investment properties
        [DisplayName("Nome")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Rentabilidade")]
        public decimal Rentability { get; set; }

        [DisplayName("Tipo")]
        public InvestmentType Type { get; set; }

        [DisplayName("Conta")]
        public AccountType Account { get; set; }

        // Collection to store selected Reserve IDs and their corresponding amounts
        public List<ReserveAmountViewModel> SelectedReserves { get; set; } = new List<ReserveAmountViewModel>();
    }

    public class ReserveAmountViewModel
    {
        public Guid ReserveId { get; set; }
        public decimal Amount { get; set; }
    }
}

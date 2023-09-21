using Finances.Database.Enums;
using System.ComponentModel;

namespace Finances.APP.Models.Investment
{
    public class UpdateInvestmentViewModel
    {
        public Guid Id { get; set; }

        // Investment properties
        [DisplayName("Nome")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Rentabilidade")]
        public decimal Rentability { get; set; }

        [DisplayName("Valor Atual")]
        public decimal CurrentAmount { get; set; }

        [DisplayName("Data de Resgate")]
        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        [DisplayName("Tipo")]
        public InvestmentType Type { get; set; }

        [DisplayName("Conta")]
        public AccountType Account { get; set; }

        // Collection to store selected Reserve IDs and their corresponding amounts
        public List<UpdateReserveAmountViewModel> SelectedReserves { get; set; } = new();
    }

    public class UpdateReserveAmountViewModel
    {
        public Guid ReserveId { get; set; }
        public string ReserveName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}

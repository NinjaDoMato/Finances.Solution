using Finances.APP.Models.Entry;
using Finances.APP.Models.Investment;
using Finances.Database.Enums;
using System.ComponentModel;

namespace Finances.APP.Models.Reserve
{
    public class ReserveViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Descrição")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Valor atual")]
        public decimal CurrentAmount { get; set; }

        [DisplayName("Valor investido")]
        public decimal InvestedAmount { get; set; }

        [DisplayName("Objetivo")]
        public decimal Goal { get; set; }

        [DisplayName("Proprietário")]
        public AccountUser Owner { get; set; }

        public List<EntryViewModel> Entries { get; set; } = new();
        public List<InvestmentViewModel> LinkedInvestments { get; set; } = new();
    }
}

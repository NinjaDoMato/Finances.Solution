using Finances.Database.Entities;
using Finances.Database.Enums;
using Microsoft.Identity.Client;
using System.ComponentModel;

namespace Finances.APP.Models.Purchase
{
    public class CreatePurchaseViewModel
    {
        [DisplayName("Nome")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Url")]
        public string ProductUrl { get; set; } = string.Empty;

        [DisplayName("Preço")]
        public decimal Amount { get; set; }

        [DisplayName("Proprietário")]
        public AccountUser Owner { get; set; }

        [DisplayName("Parcelas")]
        public int Installments { get; set; }

        [DisplayName("Data da compra")]
        public DateTime PurchaseDate { get; set; }

    }
}

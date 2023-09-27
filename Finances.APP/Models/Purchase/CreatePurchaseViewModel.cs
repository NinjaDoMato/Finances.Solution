using Finances.Database.Entities;
using Finances.Database.Enums;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Finances.APP.Models.Purchase
{
    public class CreatePurchaseViewModel
    {
        [DisplayName("Nome")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Url")]
        public string ProductUrl { get; set; } = string.Empty;

        [DisplayName("Preço")]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        [DisplayName("Proprietário")]
        public AccountUser Owner { get; set; }

        [DisplayName("Parcelas")]
        [Range(1, 50)]
        public int Installments { get; set; }

        [DisplayName("Data da compra")]
        public DateTime PurchaseDate { get; set; }

    }
}

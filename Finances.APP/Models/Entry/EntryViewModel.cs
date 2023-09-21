using System.ComponentModel;

namespace Finances.APP.Models.Entry
{
    public class EntryViewModel
    {
        [DisplayName("Valor")]
        public decimal Amount { get; set; }

        [DisplayName("Obrsvação")]
        public string Observation { get; set; } = string.Empty;
    }
}

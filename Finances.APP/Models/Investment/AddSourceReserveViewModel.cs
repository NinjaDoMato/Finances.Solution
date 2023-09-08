namespace Finances.APP.Models.Investment
{
    public class AddSourceReserveViewModel
    {
        public Guid InvestmentId { get; set; }
        public Guid ReserveId { get; set; }
        public decimal Amount { get; set; }
    }
}

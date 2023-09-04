namespace Finances.APP.Models.Investment
{
    public class CreateInvestmentViewModel
    {
        public Database.Entities.Investment Investment { get; set; } = new();
        public List<Database.Entities.Reserve> Reserves { get; set; } = new();
        public List<ReserveAmount> ReserveMaps { get; set; } = new();
    }

    public class ReserveAmount
    {
        public decimal Amount { get; set;}
        public Guid ReserveId { get; set; }
    }
}

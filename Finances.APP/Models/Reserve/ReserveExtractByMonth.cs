namespace Finances.APP.Models.Reserve
{
    public class ReserveExtractByMonth
    {
        public List<string> Labels { get; set; } = new();
        public List<ReserveValue> Reserves { get; set; } = new();
    }

    public class ReserveValue
    {
        public string ReserveName { get; set; } = string.Empty;
        public string ReserveDisplayColor { get; set; } = string.Empty;
        public List<decimal> AmountByMonth { get; set; } = new();
    }
}

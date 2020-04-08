namespace CoinShare.Api.Shared.Dtos
{
    public class PersonOverviewDto
    {
        public int PersonId { get; set; }
        public decimal Paid { get; set; }
        public decimal Consumed { get; set; }
    }
}

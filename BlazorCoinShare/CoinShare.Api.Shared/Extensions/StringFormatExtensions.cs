namespace CoinShare.Api.Shared.Extensions
{
    public static class StringFormatExtensions
    {
        public static string AsCurrency(this decimal money)
            => money.ToString("0.00€");
    }
}

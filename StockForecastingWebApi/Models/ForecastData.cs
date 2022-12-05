namespace StockForecastingWebApi.Models
{
    public class ForecastData
    {
        public float[] ForecastedPrices { get; set; }

        public float[] LowerBoundPrices { get; set; }

        public float[] UpperBoundPrices { get; set; }
    }
}

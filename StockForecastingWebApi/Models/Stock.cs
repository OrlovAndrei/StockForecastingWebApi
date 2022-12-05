namespace StockForecastingWebApi.Models
{
    public class Stock
    {
        public Stock(DateTime stockDate, float closingPrice)
        {
            StockDate=stockDate;
            ClosingPrice=closingPrice;
        }

        public DateTime StockDate { get; set; }
        public float ClosingPrice { get; set; }
    }
}

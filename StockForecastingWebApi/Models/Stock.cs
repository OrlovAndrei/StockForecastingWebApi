namespace StockForecastingWebApi.Models
{
    public class Stock
    {
        public Stock(DateTime stockDate, decimal closingPrice)
        {
            StockDate=stockDate;
            ClosingPrice=closingPrice;
        }

        public DateTime StockDate { get; set; }
        public decimal ClosingPrice { get; set; }
    }
}

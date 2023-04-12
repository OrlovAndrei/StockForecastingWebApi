namespace StockForecastingWebApi.Models
{
    public class Stock
    {
        public Stock() { }

        public Stock(DateTime stockDate, double closingPrice)
        {
            StockDate=stockDate;
            ClosingPrice=closingPrice;
        }

        public DateTime StockDate { get; set; }
        public double ClosingPrice { get; set; }
    }
}

namespace StockForecastingWebApi.Models
{
    public class StockModel
    {
        public StockModel(DateTime stockDate, decimal closingPrice)
        {
            StockDate=stockDate;
            ClosingPrice=closingPrice;
        }

        public DateTime StockDate { get; set; }
        public decimal ClosingPrice { get; set; }
    }
}

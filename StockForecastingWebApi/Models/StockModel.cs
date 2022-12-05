namespace StockForecastingWebApi.Models
{
    public class StockModel
    {
        public StockModel(DateTime stockDate, int closingPrice)
        {
            StockDate=stockDate;
            ClosingPrice=closingPrice;
        }

        public DateTime StockDate { get; set; }
        public int ClosingPrice { get; set; }
    }
}

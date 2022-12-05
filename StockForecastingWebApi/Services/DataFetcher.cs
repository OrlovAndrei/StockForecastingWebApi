using StockForecastingWebApi.Models;
using YahooFinanceApi;

namespace StockForecastingWebApi.Services
{
    public static class DataFetcher
    {
        public static async Task<IEnumerable<StockModel>> GetHistoricalData(string symbol)
        {
            var stockData = new List<StockModel>();
            try
            {
                var historicalData = await Yahoo.GetHistoricalAsync(symbol, new DateTime(2015, 1, 1), DateTime.Now, Period.Daily);
                foreach(var item in historicalData)
                {
                    stockData.Add(new StockModel(item.DateTime, item.Close));
                }
                return stockData;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

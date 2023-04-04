using StockForecastingWebApi.Models;

namespace StockForecastingWebApi.Services
{
    public interface IDataFetcher
    {
        Task<List<Stock>> GetHistoricalData(string symbol);
    }
}

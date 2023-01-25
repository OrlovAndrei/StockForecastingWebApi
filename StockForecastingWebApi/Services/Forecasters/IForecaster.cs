using StockForecastingWebApi.Models;

namespace StockForecastingWebApi.Services
{
    public interface IForecaster
    {
        public ForecastData Forecast(List<Stock> historicData);
    }
}

using StockForecastingWebApi.Models;

namespace StockForecastingWebApi.Services
{
    [Forecaster("C2")]
    public class Class2 : IForecaster
    {
        public ForecastData Forecast(List<Stock> historicData)
        {
            return new ForecastData();
        }
    }
}

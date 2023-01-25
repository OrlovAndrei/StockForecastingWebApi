using StockForecastingWebApi.Models;

namespace StockForecastingWebApi.Services
{
    [Forecaster("C1")]
    public class Class1 : IForecaster
    {
        public ForecastData Forecast(List<Stock> historicData)
        {
            return new ForecastData();
        }
    }
}

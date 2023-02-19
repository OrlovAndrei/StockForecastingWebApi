using StockForecastingWebApi.Models;

namespace StockForecastingWebApi.Services
{
    [Forecaster("arima")]
    public class ARIMAForecaster : IForecaster
    {
        public ForecastData Forecast(List<Stock> historicData)
        {
			var arimaModel = AutoArima(historicData);
            return arimaModel(historicData);
        }

        private Func<List<Stock>,ForecastData> AutoArima(List<Stock> historicData)
        {
            return data => ARIMA(0, 0, 0, data);
        }

        private ForecastData ARIMA(int p, int d, int q, List<Stock> historicData)
        {
            for (int i = 0; i < d; i++)
                historicData = Differentiate(historicData);

            ARMA(p, q, historicData);
            return new ForecastData();
        }

		private ForecastData ARMA(int p, int q, List<Stock> historicData)
		{
			throw new NotImplementedException();
		}

		private List<Stock> Differentiate(List<Stock> historicData)
		{
			throw new NotImplementedException();
		}
	}
}

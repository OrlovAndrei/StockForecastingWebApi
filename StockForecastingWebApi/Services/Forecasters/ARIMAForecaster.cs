using StockForecastingWebApi.Models;

namespace StockForecastingWebApi.Services
{
    [Forecaster("arima")]
    public class ARIMAForecaster : IForecaster
    {
        public ForecastData Forecast(List<Stock> historicData)
        {
			var arimaParams = Fit(historicData);
            return ARIMA(arimaParams.p, arimaParams.d, arimaParams.q, historicData);
        }

        private (int p, int d, int q) Fit(List<Stock> historicData)
        {
            return (0, 0, 0);
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

using TimeSeriesPrediction;

namespace StockForecastingWebApi.Services
{
	public class ForecasterProvider : IForecasterProvider
	{
		public Dictionary<string, StockForecaster> Forecasters { get; } = new Dictionary<string, StockForecaster>
		{
			{"arima",      new StockForecaster(new ArimaForecastingEstimator()) },
			{"es",         new StockForecaster(new EsForecastingEstimator()) },
			{"polinomial", new StockForecaster(new PolinomialForecastingEstimator()) },
			{"ssa",        new StockForecaster(new SsaForecastingEstimator()) }
		};
	}
}

using TimeSeriesPrediction;

namespace StockForecastingWebApi.Services
{
    [Forecaster("arima")]
    public class ArimaForecaster : StockForecaster<ArimaForecastingModel, ArimaForecastingEstimator> { }

	[Forecaster("es")]
	public class EsForecaster : StockForecaster<EsForecastingModel, EsForecastingEstimator> { }

	[Forecaster("polinomial")]
	public class PolinomialForecaster : StockForecaster<PolinomialForecastingModel, PolinomialForecastingEstimator> { }
}

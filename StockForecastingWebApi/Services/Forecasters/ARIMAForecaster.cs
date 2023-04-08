using StockForecastingWebApi.Models;
using TimeSeriesPrediction;

namespace StockForecastingWebApi.Services
{
    [Forecaster("arima")]
    public class ArimaForecaster : IForecaster
    {
        public ForecastData Forecast(List<Stock> historicData)
        {
			var transformer = new TimeSeriesTransformer<Stock>("StockDate", "ClosingPrice");
            var series = transformer.ToTimeSeries(historicData, new TimeSpan(TimeSpan.TicksPerDay));
            var estimator = new ArimaForecastingEstimator();
            var model = estimator.Fit(series);
            var forecast = model.Predict(100, series);
            return transformer.FromTimeSeries(forecast);
        }
	}
}

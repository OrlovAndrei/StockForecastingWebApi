using StockForecastingWebApi.Models;
using TimeSeriesPrediction;

namespace StockForecastingWebApi.Services
{
    public class StockForecaster
    {
		private readonly IForecastingEstimator<ForecastingModel> _estimator;

		public StockForecaster(IForecastingEstimator<ForecastingModel> estimator)
		{
			_estimator = estimator;
		}

        public List<Stock> Forecast(List<Stock> historicData, int horizont = 10) 
        {
			var transformer = new TimeSeriesTransformer<Stock>("StockDate", "ClosingPrice");
			var series = transformer.ToTimeSeries(historicData, new TimeSpan(TimeSpan.TicksPerDay));
			var model = _estimator.Fit(series);
			var forecast = model.Predict(horizont, series);
			return transformer.FromTimeSeries(forecast);
		}
    }
}

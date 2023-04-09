using StockForecastingWebApi.Models;
using TimeSeriesPrediction;

namespace StockForecastingWebApi.Services
{
    public abstract class StockForecaster<TModel, TEstimator>
        where TModel : ForecastingModel
		where TEstimator : IForecastingEstimator<TModel>, new()
    {
        public List<Stock> Forecast(List<Stock> historicData, int horizont = 10) 
        {
			var transformer = new TimeSeriesTransformer<Stock>("StockDate", "ClosingPrice");
			var series = transformer.ToTimeSeries(historicData, new TimeSpan(TimeSpan.TicksPerDay));
			var estimator = new TEstimator();
			var model = estimator.Fit(series);
			var forecast = model.Predict(horizont, series);
			return transformer.FromTimeSeries(forecast);
		}
    }
}

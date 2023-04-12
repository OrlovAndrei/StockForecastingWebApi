using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace TimeSeriesPrediction
{
	public class SsaForecastingModel : ForecastingModel
	{
		public override TimeSeries Predict(int horizont, TimeSeries series)
		{
			var mlContext = new MLContext();
			var dataView = mlContext.Data.LoadFromEnumerable(series.GetList());

			var forecastingPipline = mlContext.Forecasting.ForecastBySsa(
				outputColumnName: "Forecast",
				inputColumnName: "Value",
				windowSize: 7,
				seriesLength: 30,
				trainSize: series.Count,
				horizon: 10);

			var forecaster = forecastingPipline.Fit(dataView);
			var forecastingEngine = forecaster.CreateTimeSeriesEngine<List<Record>, ForecastResult>(mlContext);
			var predictedSeries = forecastingEngine.Predict().Forecast.ToList();

			return GetForecast(series, predictedSeries, horizont);
		}
	}
}

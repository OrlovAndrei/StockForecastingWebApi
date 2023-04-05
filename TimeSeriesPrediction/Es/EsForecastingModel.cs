namespace TimeSeriesPrediction
{
	public class EsForecastingModel : ForecastingModel
	{
		public int N { get; set; }

		public EsForecastingModel(int n) => N = n;

		public override TimeSeries Predict(int horizont, TimeSeries series)
		{
			var predictedSeries = Forecasting.Es(series.GetValues(), N, horizont);

			return GetForecast(series, predictedSeries, horizont);
		}
	}
}

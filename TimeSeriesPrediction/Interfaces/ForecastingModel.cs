namespace TimeSeriesPrediction
{
	public abstract class ForecastingModel 
	{
		public abstract TimeSeries Predict(int horizont, TimeSeries series);

		protected static TimeSeries GetForecast(TimeSeries series, List<double> predictedSeries, int horizont)
		{
			var forecast = new TimeSeries(series.LastDate + series.Interval, series.Interval);
			var count = predictedSeries.Count;
			for (int i = count - horizont; i < count; i++)
				forecast.Add(predictedSeries[i]);

			return forecast;
		}
	}
}

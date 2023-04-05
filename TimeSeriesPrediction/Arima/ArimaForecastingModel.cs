namespace TimeSeriesPrediction
{
	public class ArimaForecastingModel : IForecastingModel
	{
		public int P { get; set; }
		public int D { get; set; }
		public int Q { get; set; }

		public ArimaForecastingModel(int p, int d, int q) 
		{
			P = p;
			D = d;
			Q = q;
		}

		public TimeSeries Predict(int horizont, TimeSeries series)
		{
			var predictedSeries = Forecasting.Arima(P, D, Q, series.GetValues(), horizont);

			var forecast = new TimeSeries(series.LastDate + series.Interval, series.Interval);
			var count = series.Count;

			for (int i = count; i < count + horizont; i++)
				forecast.Add(predictedSeries[i]);

			return forecast;
		}
	}
}

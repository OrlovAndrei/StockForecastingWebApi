namespace TimeSeriesPrediction
{
	public class ArimaForecastingModel : ForecastingModel
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

		public override TimeSeries Predict(int horizont, TimeSeries series)
		{
			var predictedSeries = Forecasting.Arima(P, D, Q, series.GetValues(), horizont);

			return GetForecast(series, predictedSeries, horizont);
		}
	}
}

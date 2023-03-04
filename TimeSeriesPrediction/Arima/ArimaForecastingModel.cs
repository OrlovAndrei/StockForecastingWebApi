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

		public List<double> Predict(int horizont, List<double> series)
		{
			return TimeSeriesProcessing.Arima(P, D, Q, series, horizont);
		}
	}
}

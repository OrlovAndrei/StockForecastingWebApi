namespace TimeSeriesPrediction
{
	public class PolinomialForecastingModel : ForecastingModel
	{
		public int P { get; set; }
		public int D { get; set; }
		public int Q { get; set; }

		public PolinomialForecastingModel(int p, int d, int q) 
		{
			P = p;
			D = d;
			Q = q;
		}

		public List<double> Predict(int horizont, List<double> series)
		{
			return Forecasting.Arima(P, D, Q, series, horizont);
		}
	}
}

namespace TimeSeriesPrediction
{
	public class PolinomialForecastingModel : ForecastingModel
	{
		public double[] Parameters { get; set; }

		public PolinomialForecastingModel(double[] parameters) => Parameters = parameters;

		public override TimeSeries Predict(int horizont, TimeSeries series)
		{
			var predictedSeries = Forecasting.Polynomial(Parameters, horizont);

			return GetForecast(series, predictedSeries, horizont);
		}
	}
}

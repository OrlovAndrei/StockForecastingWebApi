namespace TimeSeriesPrediction
{
	public class PolinomialForecastingEstimator : IForecastingEstimator<PolinomialForecastingModel>
	{
		public PolinomialForecastingModel Fit(List<double> series)
		{
			return new PolinomialForecastingModel(0, 0, 0);
		}
	}
}

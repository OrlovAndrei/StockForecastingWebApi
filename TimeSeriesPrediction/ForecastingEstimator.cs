namespace TimeSeriesPrediction
{
	public abstract class ForecastingEstimator<T>
		where T : IForecastingModel
	{
		public abstract T Fit(List<double> series);
	}
}
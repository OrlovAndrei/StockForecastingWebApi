namespace TimeSeriesPrediction
{
	public class EsForecastingEstimator : IForecastingEstimator<EsForecastingModel>
	{
		public EsForecastingModel Fit(List<double> series)
		{
			return new EsForecastingModel(0, 0, 0);
		}
	}
}

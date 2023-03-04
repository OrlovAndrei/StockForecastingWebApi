namespace TimeSeriesPrediction
{
	public class ArimaForecastingEstimator : IForecastingEstimator<ArimaForecastingModel>
	{
		public ArimaForecastingModel Fit(List<double> series)
		{
			return new ArimaForecastingModel(0, 0, 0);
		}
	}
}

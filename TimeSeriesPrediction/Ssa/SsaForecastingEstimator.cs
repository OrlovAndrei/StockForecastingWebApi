namespace TimeSeriesPrediction
{
	public class SsaForecastingEstimator : IForecastingEstimator<SsaForecastingModel>
	{
		public SsaForecastingModel Fit(TimeSeries series)
		{
			return new SsaForecastingModel();
		}
	}
}

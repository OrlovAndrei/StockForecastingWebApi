namespace TimeSeriesPrediction
{
	public interface IForecastingEstimator<out T>
		where T : ForecastingModel
	{
		public T Fit(TimeSeries series);
	}
}

namespace TimeSeriesPrediction
{
	public interface IForecastingEstimator<T>
		where T : ForecastingModel
	{
		public T Fit(TimeSeries series);
	}
}

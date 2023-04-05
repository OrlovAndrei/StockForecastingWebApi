namespace TimeSeriesPrediction
{
	public interface IForecastingModel 
	{
		TimeSeries Predict(int horizont, TimeSeries series);
	}
}

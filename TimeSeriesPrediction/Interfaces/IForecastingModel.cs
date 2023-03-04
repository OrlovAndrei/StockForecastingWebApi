namespace TimeSeriesPrediction
{
	public interface IForecastingModel 
	{
		List<double> Predict(int horizont, List<double> series);
	}
}

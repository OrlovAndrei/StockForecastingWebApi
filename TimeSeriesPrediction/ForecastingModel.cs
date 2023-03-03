namespace TimeSeriesPrediction
{
	public abstract class ForecastingModel<T> : IForecastingModel
		where T : IForecastingModelParameters
	{
		public T Parameters { get; }

		public ForecastingModel(T parameters) 
		{
			Parameters = parameters;
		}

		public abstract List<double> Predict(int horizont, List<double> series);
	}
}

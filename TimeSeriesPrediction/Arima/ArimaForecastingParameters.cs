namespace TimeSeriesPrediction
{
	public class ArimaForecastingParameters : IForecastingModelParameters
	{
		public int P { get; set; }
		public int D { get; set; }
		public int Q { get; set; }
	}
}

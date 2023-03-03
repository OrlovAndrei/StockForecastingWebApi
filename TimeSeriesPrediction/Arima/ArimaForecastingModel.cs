namespace TimeSeriesPrediction
{
	public class ArimaForecastingModel : ForecastingModel<ArimaForecastingParameters>
	{
		public ArimaForecastingModel(ArimaForecastingParameters parameters) : base(parameters) { }

		public override List<double> Predict(int horizont, List<double> series)
		{
			return TimeSeries.Arima(Parameters.P, Parameters.D, Parameters.Q, series, horizont);
		}
	}
}

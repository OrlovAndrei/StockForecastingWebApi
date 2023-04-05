namespace TimeSeriesPrediction
{
	public class EsForecastingEstimator : IForecastingEstimator<EsForecastingModel>
	{
		public EsForecastingModel Fit(TimeSeries series)
		{
			var minAic = int.MaxValue;
			EsForecastingModel bestModel = null;

			for (int n = 0; n < 10; n++)
			{
				var values = series.GetValues();
				var predictedSeries = Forecasting.Es(values, n, 0);
				var rss = Forecasting.Rss(values, predictedSeries);
				var aic = Forecasting.Aic(n, rss, values.Count());

				if (aic < minAic)
					bestModel = new EsForecastingModel(n);
			}

			return bestModel;
		}
	}
}

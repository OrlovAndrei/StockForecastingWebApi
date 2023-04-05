namespace TimeSeriesPrediction
{
	public class PolinomialForecastingEstimator : IForecastingEstimator<PolinomialForecastingModel>
	{
		public PolinomialForecastingModel Fit(TimeSeries series)
		{
			var minAic = int.MaxValue;
			PolinomialForecastingModel bestModel = null;

			for (int n = 0; n < 10; n++)
			{
				var values = series.GetValues();
				var parameters = Forecasting.Mls(series.GetValues(), n);
				var predictedSeries = Forecasting.Polynomial(parameters, 0);
				var rss = Forecasting.Rss(values, predictedSeries);
				var aic = Forecasting.Aic(n, rss, values.Count);

				if (aic < minAic)
					bestModel = new PolinomialForecastingModel(parameters);
			}

			return bestModel;
		}
	}
}

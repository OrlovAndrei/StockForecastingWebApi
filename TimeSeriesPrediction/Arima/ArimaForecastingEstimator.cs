namespace TimeSeriesPrediction
{
	public class ArimaForecastingEstimator : IForecastingEstimator<ArimaForecastingModel>
	{
		public ArimaForecastingModel Fit(TimeSeries series)
		{
			int d = 2;
			var diffSeries = series.GetValues();
			for (int i = 0; i < 10; i++)
			{				
				if (Forecasting.DickeyFullerTest(diffSeries))
				{
					d = i;
					break;
				}

				diffSeries = Forecasting.Differentiate(diffSeries);
			}

			var minAic = double.MaxValue;
			ArimaForecastingModel bestModel = null;

			for (int p = 0; p < 10; p++)
			{
				for (int q = 0; q < 10; q++)
				{
					var l = p > q ? p : q;
					var values = series.GetValues();
					var predictedSeries = Forecasting.Arima(p, d, q, values, 0);
					var rss = Forecasting.Rss(values.Skip(l).ToList(), predictedSeries);
					var aic = Forecasting.Aic(p + d + q, rss, values.Count);

					if (aic < minAic)
					{
						minAic = aic;
						bestModel = new ArimaForecastingModel(p, d, q);
					}
						
				}
			}
			return bestModel;
		}
	}
}

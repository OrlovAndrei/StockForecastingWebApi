﻿namespace TimeSeriesPrediction
{
	public class ArimaForecastingEstimator : IForecastingEstimator<ArimaForecastingModel>
	{
		public ArimaForecastingModel Fit(TimeSeries series)
		{
			int d = 5;
			var diffSeries = series.GetValues();
			for (int i = 0; i < 5; i++)
			{				
				if (Forecasting.DickeyFullerTest(diffSeries))
				{
					d = i;
					break;
				}

				diffSeries = Forecasting.Differentiate(diffSeries);
			}

			var minAic = int.MaxValue;
			ArimaForecastingModel bestModel = null;

			for (int p = 0; p < 10; p++)
			{
				for (int q = 0; q < 10; q++)
				{
					var values = series.GetValues();
					var predictedSeries = Forecasting.Arima(p, d, q, values, 0);
					var rss = Forecasting.Rss(values, predictedSeries);
					var aic = Forecasting.Aic(p + d + q, rss, values.Count());

					if (aic < minAic)
						bestModel = new ArimaForecastingModel(p, d, q);
				}
			}
			return bestModel;
		}
	}
}

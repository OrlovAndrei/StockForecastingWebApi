﻿namespace TimeSeriesPrediction
{
	public interface IForecastingEstimator<T>
		where T : IForecastingModel
	{
		public T Fit(List<double> series);
	}
}
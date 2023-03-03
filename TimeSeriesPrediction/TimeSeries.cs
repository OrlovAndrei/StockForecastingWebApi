namespace TimeSeriesPrediction
{
	public static class TimeSeries
	{
		public static List<double> Differentiate (List<double> series)
		{
			var diffSeries = new List<double>();
			for (int i = 1; i < series.Count; i++)
			{
				diffSeries.Add(series[i] - series[i - 1]);
			}
			return diffSeries;
		}

		public static List<double> Integrate(double first, List<double> series)
		{
			var intSeries = new List<double>() { first };
			for (int i = 0; i < series.Count; i++)
			{
				intSeries.Add(intSeries[i] + series[i]);
			}
			return intSeries;
		}

		public static List<double> Arima(int p, int d, int q, List<double> series)
		{
			var diffSeries = new List<double>();
			series.ForEach(s => diffSeries.Add(s));
			var remains = new Stack<double>();

			for (int i = 0; i < d; i++)
			{
				remains.Push(diffSeries[0]);
				diffSeries = Differentiate(diffSeries);
			}

			var predictedSeries = Arma(p, q, diffSeries);

			for (int i = 0; i < d; i++)
			{
				var first = remains.Pop();
				predictedSeries = Integrate(first, predictedSeries);
			}

			return predictedSeries;
		}

		private static List<double> Arma(int p, int q, List<double> diffSeries)
		{
			throw new NotImplementedException();
		}
	}
}
using MathNet.Numerics.LinearAlgebra;

namespace TimeSeriesPrediction
{
	public static class Forecasting
	{
		public static List<double> Differentiate (this List<double> series)
		{
			var diffSeries = new List<double>();

			for (int i = 1; i < series.Count; i++)
				diffSeries.Add(series[i] - series[i - 1]);

			return diffSeries;
		}

		public static List<double> Integrate(double first, List<double> series)
		{
			var intSeries = new List<double>() { first };

			for (int i = 0; i < series.Count; i++)
				intSeries.Add(intSeries[i] + series[i]);

			return intSeries;
		}

		public static List<double> Arima(int p, int d, int q, List<double> series, int horizont)
		{
			var diffSeries = new List<double>();
			series.ForEach(s => diffSeries.Add(s));
			var remains = new Stack<double>();

			for (int i = 0; i < d; i++)
			{
				remains.Push(diffSeries[0]);
				diffSeries = Differentiate(diffSeries);
			}

			var predictedSeries = Arma(p, q, diffSeries, horizont);

			for (int i = 0; i < d; i++)
			{
				var first = remains.Pop();
				predictedSeries = Integrate(first, predictedSeries);
			}

			return predictedSeries;
		}

		public static List<double> Arma(int p, int q, List<double> diffSeries, int horizont)
		{
			var a = 1 / (double) p;
			var b = 1 / (double)q;
			var l = p > q ? p : q;
			var predictedSeries = new List<double>();
			predictedSeries.Add(diffSeries[l]);

			for (int i = l; i < diffSeries.Count + horizont; i++)
			{
				var ar = .0;
				for (int j = 0; j < p; j++)
				{
					if(i - j < diffSeries.Count)
						ar += a * diffSeries[i - j];
					
					else
						ar += a * predictedSeries[^(1 + j)];
				}
					
				var ma = .0;
				for (int j = 0; j < q; j++)
				{
					if (i - j < diffSeries.Count || i - j >= l)
					{
						var e = predictedSeries[^(1 + j)] - diffSeries[i - j];
						ma += a * e;
					}
				}

				predictedSeries.Add(ar + ma);
			}

			return predictedSeries;
		}

		public static List<double> ExponentialSmoothing(List<double> series, int n, int horizont)
		{
			var a = 2 / (1 + n);
			var t = new List<double>();
			var l = new List<double>();
			t.Add(0);
			l.Add(series[0]);

			for (int i = 1; i < series.Count; i++)
			{
				l.Add(a * series[i] + (1 - a) * (l[i - 1] - t[i - 1]));
				t.Add(a * (l[i] - l[i - 1]) + (1 - a) * t[i - 1]);
			}

			var predictedSeries = new List<double>();

			for (int j = 0; j < horizont; j++)
				predictedSeries.Add(l[^1] + j * t[^1]);

			return predictedSeries;
		}

		public static List<double> Polynomial(double[] parameters, int horizont, int start = 0)
		{
			var predictedSeries = new List<double>();

			for (int i = 0; i < horizont; i++)
			{
				var res = .0;

				for (int j = 0; j < parameters.Length; j++)
					res += parameters[j] * Math.Pow(i + start, j);

				predictedSeries.Add(res);
			}

			return predictedSeries;
		}

		public static double Mse(List<double> list1, List<double> list2)
		{
			return Rss(list1, list2) / list1.Count;
		}

		public static double Rss(List<double> list1, List<double> list2)
		{
			var err = .0;

			for (int i = 0; i < list1.Count; i++)
				err += Math.Pow(list1[i] - list2[i], 2);

			return err;
		}

		public static double[] Mls(List<double> series, int degree)
		{				
			var x = new double[series.Count, degree + 1];

			for (int i = 0; i < series.Count; i++)
				for (int j = 0; j < degree + 1; j++)
					x[i, j] = Math.Pow(i, j);

			var X = Matrix<double>.Build.DenseOfArray(x);
			var XT = X.Transpose();
			var XTX = XT.Multiply(X);
			var InvXTX = XTX.Inverse();
			var Y = Vector<double>.Build.DenseOfArray(series.ToArray());
			var XTY = XT.Multiply(Y);
			var B = InvXTX.Multiply(XTY);

			return B.ToArray();
		}

		public static double Aic(double k, double mse, double n)
		{
			return 2 * k + n * Math.Log(mse);
		}
	}
}
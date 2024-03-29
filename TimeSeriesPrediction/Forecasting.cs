﻿using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;

namespace TimeSeriesPrediction
{
	public static class Forecasting
	{
		public static List<double> Differentiate(this List<double> series)
		{
			if (series.Count < 2)
				throw new Exception("The series cannot be differentiated");

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
			var l = p > q ? p : q;

			if (series.Count < l + d)
				throw new Exception($"The series is too short to predict the ARIMA model with parameters p={p}, d={d}, q={q}");

			var diffSeries = new List<double>(series);
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

		public static List<double> Arma(int p, int q, List<double> series, int horizont)
		{
			var l = p > q ? p : q;
			if (series.Count < l)
				throw new Exception($"The series is too short to predict the ARIMA model with parameters p={p}, q={q}");
			
			//коэффициенты расчитываются с помощью уравнения Юла — Уокера
			var a = YuleWalker(series, p); //авторегрессия

			var errors = new List<double>();
			for (int i = p; i < series.Count; i++)
			{
				var ar = .0;
				for (int j = 0; j < p; j++)
				{
					ar += a[j] * series[i - j - 1];
				}

				errors.Add(series[i] - ar);
			}

			var b = YuleWalker(errors, q);//скользящие среднее

			var predictedSeries = new List<double>();

			for (int i = l; i < series.Count + horizont; i++)
			{
				var ar = .0;
				for (int j = 0; j < p; j++)
				{
					if (i - j - 1 < series.Count)
						ar += a[j] * series[i - j - 1];

					else
						ar += a[j] * predictedSeries[^(1 + j)];
				}

				var ma = .0;
				for (int j = 0; j < q; j++)
				{
					if (i - j - 1 < series.Count && i - j - 1 >= l)
					{
						var e = predictedSeries[^(1 + j)] - series[i - j - 1];
						ma += b[j] * e;
					}
				}

				predictedSeries.Add(ar + ma);
			}

			return predictedSeries;
		}

		public static List<double> Es(List<double> series, int n, int horizont)
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

			var predictedSeries = new List<double>(l);

			for (int j = 0; j < horizont; j++)
				predictedSeries.Add(l[^1] + j * t[^1]);

			return predictedSeries;
		}

		public static List<double> Polynomial(double[] parameters, int horizont, int length)
		{
			var predictedSeries = new List<double>();

			for (int i = 0; i < length + horizont ; i++)
			{
				var res = .0;

				for (int j = 0; j < parameters.Length; j++)
					res += parameters[j] * Math.Pow(i, j);

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
			if (list1.Count != list2.Count)
				throw new Exception("list1 and list2 have different lengths");

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

		public static double Aic(double k, double rss, double n)
		{
			return 2 * k + n * Math.Log(rss);
		}

		public static bool DickeyFullerTest(List<double> series)
		{
			var n = series.Count;

			//Находим первую разность
			var diffSeries = Differentiate(series);

			// Находим среднее и отклонение
			var mean = series.Average();
			var variance = series.Variance();

			// Рассчитываем тестовую статистику
			var testStatistic = (diffSeries.Sum() - (n - 1) * mean) / (variance * Math.Sqrt(n - 1));

			// Расчитываем критическое значение
			double criticalValue;
			if (n <= 100)
					criticalValue = -2.89;
			else
					criticalValue = -3.43;

			return testStatistic < criticalValue;
		}

		public static double[] YuleWalker(List<double> series, int order)
		{
			var data = Vector<double>.Build.DenseOfArray(series.ToArray());

			// Создаем матрицу системы уравнений
			var matrix = Matrix<double>.Build.Dense(order, order);
			for (int i = 0; i < order; i++)
				for (int j = 0; j < order; j++)
					matrix[i, j] = data.SubVector(order - i - 1, data.Count - order - 1)
						.DotProduct(data.SubVector(order - j - 1, data.Count - order - 1));

			// Создаем вектор правой части системы уравнений
			var rightHandSide = Vector<double>.Build.Dense(order);
			for (int i = 0; i < order; i++)
				rightHandSide[i] = data.SubVector(order, data.Count - order - 1).DotProduct(data.SubVector(order - i - 1, data.Count - order - 1));

			// Решаем систему уравнений
			var coefficients = matrix.QR().Solve(rightHandSide);

			// Создаем вектор коэффициентов модели авторегрессии
			var arCoefficients = Vector<double>.Build.Dense(order);
			for (int i = 0; i < order; i++)
				arCoefficients[i] = coefficients[i];

			return arCoefficients.ToArray();
		}
	}
}
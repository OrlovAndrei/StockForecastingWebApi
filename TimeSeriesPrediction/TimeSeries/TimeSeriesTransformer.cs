using System.Collections;

namespace TimeSeriesPrediction
{
	public class TimeSeriesTransformer<T>
		where T : IEnumerable
	{
		private readonly string _datePropertyName;
		private readonly string _valuePropertyName;

		public TimeSeriesTransformer(string datePropertyName, string valuePropertyName)
		{
			_datePropertyName = datePropertyName;
			_valuePropertyName = valuePropertyName;
		}

		public TimeSeries TransformToTimeSeries(List<T> series, TimeSpan interval)
		{
			var type = typeof(T);
			var timeSeries = new List<Record>();

			foreach (var item in series) 
			{
				var date = (DateTime)type.GetProperty(_datePropertyName).GetValue(item);
				var value = (double)type.GetProperty(_valuePropertyName).GetValue(item);
				timeSeries.Add(new Record(date, value));
			}

			timeSeries = timeSeries.OrderBy(s => s.Date).ToList();
			return new TimeSeries(timeSeries, interval);
		}
	}
}

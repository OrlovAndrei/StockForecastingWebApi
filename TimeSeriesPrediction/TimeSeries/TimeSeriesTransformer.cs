namespace TimeSeriesPrediction
{
	public class TimeSeriesTransformer<T>
		where T : new()
	{
		private readonly string _datePropertyName;
		private readonly string _valuePropertyName;

		public TimeSeriesTransformer(string datePropertyName, string valuePropertyName)
		{
			_datePropertyName = datePropertyName;
			_valuePropertyName = valuePropertyName;
		}

		public TimeSeries ToTimeSeries(List<T> series, TimeSpan interval)
		{
			var type = typeof(T);
			var timeSeries = new List<Record>();

			foreach (var item in series) 
			{
				var date = (DateTime)type.GetProperty(_datePropertyName).GetValue(item);
				var value = (double)type.GetProperty(_valuePropertyName).GetValue(item);
				timeSeries.Add(new Record(date, (float)value));
			}

			timeSeries = timeSeries.OrderBy(s => s.Date).ToList();
			return new TimeSeries(timeSeries, interval);
		}

		public List<T> FromTimeSeries(TimeSeries series) 
		{
			var type = typeof(T);
			var res = new List<T>();

			foreach (Record item in series)
			{
				var elem = new T();
				type.GetProperty(_datePropertyName).SetValue(elem, item.Date);
				type.GetProperty(_valuePropertyName).SetValue(elem, item.Value);
				res.Add(elem);
			}

			return res;
		}
	}
}

using System.Collections;

namespace TimeSeriesPrediction
{
	public class TimeSeries : IEnumerable
	{
		private readonly List<Record> _series;
		private readonly TimeSpan _interval;

		public TimeSeries(List<Record> series, TimeSpan interval) 
		{
			_series = series;
			_interval = interval;
		}

		public void Add(double value)
		{
			var lastDate = _series.Count != 0 ? _series[^1].Date : DateTime.Now;
			_series.Add(new Record(lastDate + _interval, value));
		}

		public double this[DateTime date]
		{
			get => _series.Find(s => s.Date == date).Value;

			set
			{
				var index = _series.IndexOf(_series.Find(s => s.Date == date));
				_series[index] = new Record(date, value);
			}
		}

		public IEnumerator GetEnumerator() => _series.GetEnumerator();
	}
}
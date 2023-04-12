using System.Collections;

namespace TimeSeriesPrediction
{
	public class TimeSeries : IEnumerable
	{
		private readonly List<Record> _series;
		private readonly TimeSpan _interval;
		private readonly DateTime _startDate;

		public int Count { get { return _series.Count; } }
		public TimeSpan Interval { get { return _interval; } }
		public DateTime FirstDate { get { return _series[0].Date; } }
		public DateTime LastDate { get { return _series[^1].Date; } }

		public TimeSeries(List<Record> series, TimeSpan interval) 
		{
			_series = series;
			_interval = interval;
		}

		public TimeSeries(DateTime startDate, TimeSpan interval)
		{
			_series = new List<Record>();
			_startDate = startDate;
			_interval = interval;
		}

		public void Add(double value)
		{
			var lastDate = _series.Count != 0 ? _series[^1].Date : _startDate;
			_series.Add(new Record(lastDate + _interval, value));
		}

		public List<double> GetValues()
		{
			var series = new List<double>();
			foreach (var record in _series)
				series.Add(record.Value);

			return series;
		}

		public List<Record> GetList()
		{
			return _series.ToList();
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
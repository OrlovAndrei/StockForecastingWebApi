namespace TimeSeriesPrediction
{
	public class Record
	{
		public DateTime Date { get; }
		public double Value { get; set; }

		public Record(DateTime date, double value)
		{
			Date = date;
			Value = value;
		}
	}
}

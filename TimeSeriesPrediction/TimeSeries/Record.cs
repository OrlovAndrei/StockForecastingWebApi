namespace TimeSeriesPrediction
{
	public class Record
	{
		public DateTime Date { get; }
		public float Value { get; set; }

		public Record(DateTime date, float value)
		{
			Date = date;
			Value = value;
		}
	}
}

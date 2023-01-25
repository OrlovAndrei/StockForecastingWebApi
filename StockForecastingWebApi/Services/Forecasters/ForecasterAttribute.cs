namespace StockForecastingWebApi.Services
{
    public class ForecasterAttribute : Attribute
    {
        public string Name { get; set; }

        public ForecasterAttribute(string name)
        {
            Name=name;
        }
    }
}

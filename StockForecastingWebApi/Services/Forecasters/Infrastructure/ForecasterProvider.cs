using System.Reflection;

namespace StockForecastingWebApi.Services
{
    public class ForecasterProvider : IForecasterProvider
    {
        public Dictionary<string, StockForecaster> Forecasters { get => GetForecasters(); }

        private Dictionary<string, StockForecaster> GetForecasters()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(a => a.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance)
                .OfType<StockForecaster>()
                .ToDictionary(x => x
                    .GetType()
                    .GetCustomAttribute<ForecasterAttribute>()
                    .Name);
        }
    }
}

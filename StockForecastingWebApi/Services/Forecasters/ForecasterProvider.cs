using System.Reflection;

namespace StockForecastingWebApi.Services
{
    public class ForecasterProvider : IForecasterProvider
    {
        public Dictionary<string, IForecaster> Forecasters { get => GetForecasters(); }

        private Dictionary<string, IForecaster> GetForecasters()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(a => a.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance)
                .OfType<IForecaster>()
                .ToDictionary(x => x
                    .GetType()
                    .GetCustomAttribute<ForecasterAttribute>()
                    .Name);
        }
    }
}

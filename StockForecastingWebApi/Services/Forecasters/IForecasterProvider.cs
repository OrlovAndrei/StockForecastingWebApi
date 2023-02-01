using System.Reflection;

namespace StockForecastingWebApi.Services
{
    public interface IForecasterProvider
    {
        Dictionary<string, IForecaster> Forecasters { get; }
    }
}

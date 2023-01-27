using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using StockForecastingWebApi.Models;

namespace StockForecastingWebApi.Services
{
    [Forecaster("ssa")]
    public class SSAForecaster : IForecaster
    {
        public ForecastData Forecast(List<Stock> historicData)
        {
            var mlContext = new MLContext();
            var dataView = mlContext.Data.LoadFromEnumerable(historicData);

            var forecastingPipline = mlContext.Forecasting.ForecastBySsa(
                outputColumnName: "ForecastedPrices",
                inputColumnName: "ClosingPrice",
                windowSize: 7,
                seriesLength: 30,
                trainSize: historicData.Count,
                horizon: 100,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "LowerBoundPrices",
                confidenceUpperBoundColumn: "UpperBoundPrices");

            var forecaster = forecastingPipline.Fit(dataView);
            var forecastingEngine = forecaster.CreateTimeSeriesEngine<Stock, ForecastData>(mlContext);

            return forecastingEngine.Predict();
        }
    }
}

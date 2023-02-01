using Microsoft.AspNetCore.Mvc;
using StockForecastingWebApi.Services;

namespace StockForecastingWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        IForecasterProvider _forecastService;
        IDataFetcher _dataFetcher;

        public StockController(IForecasterProvider forecastService, IDataFetcher dataFetcher)
        {
            _forecastService = forecastService;
            _dataFetcher = dataFetcher;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetForecast(string symbol, string method)
        {
            symbol = symbol.ToUpper();
            try
            {
                var historicData = await _dataFetcher.GetHistoricalData(symbol);
                var forecast = _forecastService.Forecasters[method].Forecast(historicData);
                return historicData == null ? NotFound() : Ok(new {HistoricData=historicData, Forecast=forecast });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult<object> GetMethods()
        {
            try
            {
                var methods = _forecastService.Forecasters.Keys.ToArray();
                return methods == null ? NotFound() : Ok(methods);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

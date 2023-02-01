using Microsoft.AspNetCore.Mvc;
using StockForecastingWebApi.Services;

namespace StockForecastingWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IForecasterProvider _forecasterProvider;
        private readonly IDataFetcher _dataFetcher;

        public StockController(IForecasterProvider forecasterProvider, IDataFetcher dataFetcher)
        {
            _forecasterProvider = forecasterProvider;
            _dataFetcher = dataFetcher;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetForecast(string symbol, string method)
        {
            symbol = symbol.ToUpper();
            try
            {
                var historicData = await _dataFetcher.GetHistoricalData(symbol);
                var forecast = _forecasterProvider.Forecasters[method].Forecast(historicData);
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
                var methods = _forecasterProvider.Forecasters.Keys.ToArray();
                return methods == null ? NotFound() : Ok(methods);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

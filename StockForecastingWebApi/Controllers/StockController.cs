using Microsoft.AspNetCore.Mvc;
using StockForecastingWebApi.Services;

namespace StockForecastingWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<object>> GetForecast(string symbol, string method)
        {
            symbol = symbol.ToUpper();
            try
            {
                var historicData = await DataFetcher.GetHistoricalData(symbol);
                var forecast = Program.Forecasters[method].Forecast(historicData);
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
                var methods = Program.Forecasters.Keys.ToArray();
                return methods == null ? NotFound() : Ok(methods);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

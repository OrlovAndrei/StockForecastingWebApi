using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockForecastingWebApi.Models;
using StockForecastingWebApi.Services;

namespace StockForecastingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<object>> Get(string symbol, string method)
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
    }
}

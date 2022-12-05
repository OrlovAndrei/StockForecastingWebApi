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
        [HttpGet("{symbol}")]
        public async Task<ActionResult<object>> Get(string symbol)
        {
            symbol = symbol.ToUpper();
            try
            {
                var historicData = await DataFetcher.GetHistoricalData(symbol);
                var forecast = StockForecaster.Forecast(historicData);
                return historicData == null ? NotFound() : Ok(new {HistoricData=historicData, Forecast=forecast });
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}

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
        public async Task<ActionResult<IEnumerable<Stock>>> Get(string symbol)
        {
            symbol = symbol.ToUpper();
            try
            {
                var historicData = await DataFetcher.GetHistoricalData(symbol);
                return historicData == null ? NotFound() : Ok(historicData);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}

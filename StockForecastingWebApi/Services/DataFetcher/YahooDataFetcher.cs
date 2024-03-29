﻿using StockForecastingWebApi.Models;
using YahooFinanceApi;

namespace StockForecastingWebApi.Services
{
    public class YahooDataFetcher : IDataFetcher
    {
        public async Task<List<Stock>> GetHistoricalData(string symbol)
        {
            var stockData = new List<Stock>();
            try
            {
                var historicalData = await Yahoo.GetHistoricalAsync(symbol, new DateTime(2010, 1, 1), DateTime.Now, Period.Daily);
                foreach(var item in historicalData)
                {
                    stockData.Add(new Stock(item.DateTime, (double)item.Close));
                }
                return stockData;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

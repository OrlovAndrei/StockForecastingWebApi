using StockForecastingWebApi.Services;
using System.Collections.Generic;
using Xunit;

namespace StockForecastingWebApi.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void GetForecastersTest()
        {
            Dictionary<string, IForecaster> forecasters = Program.GetForecasters();

            Assert.True(forecasters["C1"] is Class1);
            Assert.True(forecasters["C2"] is Class2);
        }

        [Fact]
        public void GetForecastersIntegrationTest()
        {
            Dictionary<string, IForecaster> forecasters = Program.GetForecasters();

            foreach(var forecaster in forecasters)
            {
                Assert.True(forecaster.Value is IForecaster);
            }
        }
    }
}
using StockForecastingWebApi.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace StockForecastingWebApi.Tests
{
    public class ForecasterProviderTests
    {
        [Theory]
        [InlineData("ssa", typeof(SSAForecaster))]
		[InlineData("arima", typeof(ArimaForecaster))]
		public void ForecastersContainClassTest(string name, Type forecasterClass)
        {
            var provider = new ForecasterProvider();
            Dictionary<string, StockForecaster> forecasters = provider.Forecasters;

            Assert.True(forecasters[name].GetType() == forecasterClass);
        }

        [Fact]
        public void ForecastersIsIForecasterTest()
        {
			var provider = new ForecasterProvider();
			Dictionary<string, StockForecaster> forecasters = provider.Forecasters;

			foreach (var forecaster in forecasters)
            {
                Assert.True(forecaster.Value is StockForecaster);
            }
        }
    }
}
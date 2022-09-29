using AndreasReitberger.API;
using AndreasReitberger.API.Structs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaVantageStocksApiSharp.Test
{
    public class Tests
    {
        const string web = "https://data.lemon.markets/v1/";
        const string api = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJsZW1vbi5tYXJrZXRzIiwiaXNzIjoibGVtb24ubWFya2V0cyIsInN1YiI6InVzcl9xeU1sTmxsUFBKUk5kWXd0Z0JaaHpmVE1IMXliMEd5NGhjIiwiZXhwIjoxNjY5NjMzMzgzLCJpYXQiOjE2NjQ0NDkzODMsImp0aSI6ImFwa19xeU1sUEZGR0cwZHNLWnhuOFdWNDBmRkZTRjZjbWdSSjNDIiwibW9kZSI6Im1hcmtldF9kYXRhIn0.atZz99IVoCKNbQpTuU34bv-fqIDkmWdwz7Lwkn_9Msc";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ConnectionTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithWebAddressAndApiKey(webAddress: LemonMarketsAPIs.MarketDataAPI, apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetInstrumentsTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithWebAddressAndApiKey(webAddress: LemonMarketsAPIs.MarketDataAPI, apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var instruments = await client.GetInstrumentsAsync(new List<string>() { LemonMarketsSymbols.BASF, LemonMarketsSymbols.MercedesBenzAG });
                Assert.IsTrue(instruments?.Results?.Count > 0);
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetVenuesTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithWebAddressAndApiKey(webAddress: LemonMarketsAPIs.MarketDataAPI, apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var venues = await client.GetVenuesAsync();
                Assert.IsTrue(venues?.Results?.Count > 0);
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetQuotesTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithWebAddressAndApiKey(webAddress: LemonMarketsAPIs.MarketDataAPI, apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var venues = await client.GetQuotesAsync(LemonMarketsSymbols.BASF);
                Assert.IsTrue(venues?.Results?.Count > 0);
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
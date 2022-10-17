using AndreasReitberger.API;
using AndreasReitberger.API.Enums;
using AndreasReitberger.API.Structs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LemonMarketsStocksApiSharp.Test
{
    public class Tests
    {
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

                var instruments = await client.GetInstrumentsAsync(new List<string>() { LemonMarketsSymbols.BASF, LemonMarketsSymbols.MercedesBenzGroup });
                Assert.IsTrue(instruments?.Results?.Count > 0);

                var searchResult = await client.GetInstrumentsAsync("", "BASF");
                Assert.IsTrue(searchResult?.Results?.Count > 0);
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

                var quotes = await client.GetQuotesAsync(LemonMarketsSymbols.BASF);
                Assert.IsTrue(quotes?.Results?.Count > 0);
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetOHLCTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithWebAddressAndApiKey(webAddress: LemonMarketsAPIs.MarketDataAPI, apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var ohlc = await client.GetOHLCAsync(LemonMarketsSymbols.BASF, LemonMarketsIntervals.PerDay, -1, -1);
                Assert.IsTrue(ohlc?.Results?.Count > 0);
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetTradeTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithWebAddressAndApiKey(webAddress: LemonMarketsAPIs.MarketDataAPI, apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var trades = await client.GetTradesAsync(LemonMarketsSymbols.BASF);
                Assert.IsTrue(trades?.Results?.Count > 0);
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task AccountTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithPaperTrading()
                    .WithApiKey(apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var accountInfo = await client.GetAccountInformationAsync();
                Assert.IsNotNull(accountInfo?.Results);
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task WithdrawalsTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithPaperTrading()
                    .WithApiKey(apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var withdrawal = await client.WithdrawalMoneyAsync(amountOfMoney: 100, 1234);
                Assert.IsNotNull(withdrawal);
                Assert.IsTrue(withdrawal.Status == "ok");
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetBankStatementsTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithPaperTrading()
                    .WithApiKey(apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var statements = await client.GetBankStatementsAsync(type: LemonMarketsBankStatementTypes.Dividend);
                Assert.IsNotNull(statements);
                Assert.IsTrue(statements.Status == "ok");
            }
            catch(Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetDocumentsTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithPaperTrading()
                    .WithApiKey(apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var documents = await client.GetDocumentsAsync();
                Assert.IsNotNull(documents);
                Assert.IsTrue(documents.Status == "ok");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetOrdersTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithPaperTrading()
                    .WithApiKey(apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var orders = await client.GetOrdersAsync();
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Status == "ok");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetPositionsTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithPaperTrading()
                    .WithApiKey(apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var positions = await client.GetPositionsAsync();
                Assert.IsNotNull(positions);
                Assert.IsTrue(positions.Status == "ok");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetStatementsTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithPaperTrading()
                    .WithApiKey(apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var statements = await client.GetStatementsAsync();
                Assert.IsNotNull(statements);
                Assert.IsTrue(statements.Status == "ok");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetPerformanceTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithPaperTrading()
                    .WithApiKey(apiKey: api)
                    .Build();
                Assert.IsNotNull(client);
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var performance = await client.GetPerformanceAsync();
                Assert.IsNotNull(performance);
                Assert.IsTrue(performance.Status == "ok");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
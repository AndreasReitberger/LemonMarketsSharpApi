using AndreasReitberger.API;
using AndreasReitberger.API.Enums;
using AndreasReitberger.API.Models.REST.Respones;
using AndreasReitberger.API.Structs;
using IO.Ably;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LemonMarketsStocksApiSharp.Test
{
    public class Tests
    {
        const string api_trading = "your_api_key";
        const string api_data = "your_api_key";
        [SetUp]
        public void Setup()
        {
        }

        async Task CooldownAsync(int time)
        {
            // Avoid api cooldowns for automated tests
            await Task.Delay(time);
        }

        [Test]
        public async Task ConnectionTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithWebAddressAndApiKey(webAddress: LemonMarketsAPIs.MarketDataAPI, apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);
            }
            catch (Exception exc)
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
                    .WithMarketData()
                    .WithApiKey(apiKey: api_data)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var instruments = await client.GetInstrumentsAsync(new List<string>() { LemonMarketsSymbols.BASF, LemonMarketsSymbols.MercedesBenzGroup });
                Assert.IsTrue(instruments?.Results?.Count > 0);

                var searchResult = await client.GetInstrumentsAsync("", "BASF");
                Assert.IsTrue(searchResult?.Results?.Count > 0);

                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
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
                    .WithMarketData()
                    .WithApiKey(apiKey: api_data)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var venues = await client.GetVenuesAsync();
                Assert.IsTrue(venues?.Results?.Count > 0);

                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
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
                    .WithMarketData()
                    .WithApiKey(apiKey: api_data)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var quotes = await client.GetQuotesAsync(LemonMarketsSymbols.BASF);
                Assert.IsTrue(quotes?.Results?.Count > 0);
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
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
                    .WithMarketData()
                    .WithApiKey(apiKey: api_data)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var ohlc = await client.GetOHLCAsync(LemonMarketsSymbols.BASF, LemonMarketsIntervals.PerDay, -1, -1);
                Assert.IsTrue(ohlc?.Results?.Count > 0);
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
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
                    .WithMarketData()
                    .WithApiKey(apiKey: api_data)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var trades = await client.GetTradesAsync(LemonMarketsSymbols.BASF);
                Assert.IsTrue(trades?.Results?.Count > 0);
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
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
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                LemonMarketsAccountInfoRespone accountInfo = await client.GetAccountInformationAsync();
                Assert.IsNotNull(accountInfo?.Results);
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
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
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var withdrawal = await client.WithdrawalMoneyAsync(amountOfMoney: 100, 1234);
                Assert.IsNotNull(withdrawal);
                Assert.IsTrue(withdrawal.Status == "ok");
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
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
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var statements = await client.GetBankStatementsAsync(type: LemonMarketsBankStatementTypes.Dividend);
                Assert.IsNotNull(statements);
                Assert.IsTrue(statements.Status == "ok");
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
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
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var documents = await client.GetDocumentsAsync();
                Assert.IsNotNull(documents);
                Assert.IsTrue(documents.Status == "ok");
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
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
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var orders = await client.GetOrdersAsync();
                Assert.IsNotNull(orders);
                Assert.IsTrue(orders.Status == "ok");
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
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
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var positions = await client.GetPositionsAsync();
                Assert.IsNotNull(positions);
                Assert.IsTrue(positions.Status == "ok");
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
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
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var statements = await client.GetStatementsAsync();
                Assert.IsNotNull(statements);
                Assert.IsTrue(statements.Status == "ok");
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
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
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var performance = await client.GetPerformanceAsync();
                Assert.IsNotNull(performance);
                Assert.IsTrue(performance.Status == "ok");
                // Avoid api cooldowns for automated tests
                await CooldownAsync(50000);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task LiveStreamingTest()
        {
            try
            {
                LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
                    .WithLiveStreaming()
                    .WithApiKey(apiKey: api_trading)
                    .Build();
                Assert.IsNotNull(client);
                client.Error += (sender, args) =>
                {
                    if (args is UnhandledExceptionEventArgs unhandled)
                    {
                        Assert.Fail($"{unhandled.ExceptionObject}");
                    }
                };
                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                var auth = await client.LiveStreamAuthAsync();
                Assert.IsNotNull(auth);
                Assert.IsNotNull(auth.UserId);

                AblyRealtime ably = client.SetupRealtimeLiveStreamConnection(auth);
                Assert.IsNotNull(ably);

                List<string> subscribedIsins = new List<string>()
                {
                    "US64110L1061", // Netflix
                    "US88160R1014", // Tesla
                };

                ably = await client.SubscribeMessagesAsync(ably, auth, (msg) =>
                {
                    Debug.WriteLine($"Message: {msg.Name} => {msg.Data}");
                }, subscribedIsins);

                ably = client.SetupMessagingChannel(ably, auth.UserId, (msg) =>
                {
                    Debug.WriteLine($"Message: {msg.Name} => {msg.Data}");
                });
                CancellationTokenSource cts = new CancellationTokenSource(new TimeSpan(0, 30, 0));
                do
                {
                    await Task.Delay(1000);
                }
                while (!cts.IsCancellationRequested);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
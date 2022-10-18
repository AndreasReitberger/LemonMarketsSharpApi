# LemonMarketsSharpApi
A C# wrapper around the Lemon Markets Stocks &amp; ETF API (https://docs.lemon.markets/)

# Nuget
Get the latest version from nuget.org<br>
[![NuGet](https://img.shields.io/nuget/v/LemonMarketsApiSharp.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/LemonMarketsApiSharp/)
[![NuGet](https://img.shields.io/nuget/dt/LemonMarketsApiSharp.svg)](https://www.nuget.org/packages/LemonMarketsApiSharp)

# Supported Endpoints

| Endpoint                            | Implemented  | Tests available  |
| ----------------------------------- |:------------:| ----------------:|
| MarketData                          | ✅           | ✅              |
| PaperTrading                        | ✅           | ✅              |
| LiveTrading                         | ✅           | ✅              |
| LiveStreaming                       | ✅           | ✅              |

# Usage
Some usage examples.

## Create a client
In order to create a `LemonMarketsClient` you either can create a new instance of it, or yous the `LemonMarketsConnectionBuilder`.

```cs 
const string api = "demo";
        
 LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
  .WithMarketTrading()       // The api has an endpoint for trading and market data api. Use one instance per api you want to use
  .WithApiKey(apiKey: api)   // Keep in mind that you need different api keys for marketdata and trading
  .Build();
// Make sure that you check if the server is online first, otherwise no requests will work!
await client.CheckOnlineAsync();
```

# MarketData

## Search instruments
The api works with ISIN (International Securities Identification Number). Most known stocks symbols are available with the `LemonMarketsSymbols` struct.
However, you also can search the api.

```cs
var searchResult = await client.GetInstrumentsAsync("", "BASF");
var results = searchResult.Results; // Holds the results of the search
```
For missing default symbols, you can commit to the `LemonMarketsSymbols` struct file.

## Get Venues

```cs
var venues = await client.GetVenuesAsync();
var result = venues.Result; // Will hold the found venues
```

## Get Quotes

```cs
var quotes = await client.GetQuotesAsync(LemonMarketsSymbols.BASF);
var result = quotes.Result; // Will hold the found quotes
```

## Get OHLC (Open, High, Low, Close)

```cs
var ohlc = await client.GetOHLCAsync(LemonMarketsSymbols.BASF, LemonMarketsIntervals.PerDay);
var result = ohlc.Result; // Will hold the result
```

## Get Trades

```cs
var trades = await client.GetTradesAsync(LemonMarketsSymbols.BASF);
var result = trades.Result; // Will hold the trades
```

# Trading (Paper and LiveTrading)
With the trading enpoint fetch account infos, bank statements and documents. Moreover you can manage orders, withdrawals and many more.

```cs
LemonMarketsAccountInfoRespone accountInfo = await client.GetAccountInformationAsync();
Assert.IsNotNull(accountInfo?.Results);
```

Please visit the UnitTests for more examples.

# LiveStreaming
With LiveStreaming you can subscribe to events. This feature uses the Ably .NET Nuget (https://github.com/ably/ably-dotnet), which is mandatory.

## Example

```cs
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
```

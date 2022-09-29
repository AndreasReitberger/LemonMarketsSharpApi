# LemonMarketsSharpApi
A C# wrapper around the Lemon Markets Stocks &amp; ETF API (https://docs.lemon.markets/)

# Usage
Some usage examples.

## Create a client
In order to create a `LemonMarketsClient` you either can create a new instance of it, or yous the `LemonMarketsConnectionBuilder`.

```cs 
const string api = "demo";
        
 LemonMarketsClient client = new LemonMarketsClient.LemonMarketsConnectionBuilder()
  .WithMarketTrading()       // The api has an endpoint for trading and market data api. Use one instance per api you want to use
  .WithApiKey(apiKey: api)
  .Build();
// Make sure that you check if the server is online first, otherwise no requests will work!
await client.CheckOnlineAsync();
```

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

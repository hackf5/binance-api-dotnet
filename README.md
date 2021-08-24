# A(nother) `dotnet` API for the Binance trading platform

There already exist a few other `dotnet` APIs for the Binance trading platform:

- [JKorf/Binance.Net](https://github.com/JKorf/Binance.Net): the most well developed and still maintained.
- [glitch100/BinanceDotNet](https://github.com/glitch100/BinanceDotNet): probably abandoned.
- [sonvister/Binance](https://github.com/sonvister/Binance): probably abandoned.
- [morpheums/Binance.API.Csharp.Client](https://github.com/morpheums/Binance.API.Csharp.Client): abandoned.

## Why write another?

Looking at the volatility in crypto markets on Binance there is clearly a significant opportunity to make profits trading cryptos, but to do that efficiently you need to can't trade manually, you need a bot to trade. However, if I'm writing a bot that trades on my own account, I want full control over and confidence in the library that communicates with the market, so that's why.

## Why not fork one of the existing libraries?

I don't really like any of the existing APIs. They all do more than I think they should and none of them do it as well as I think they could. Overly heavyweight, convoluted implementations, under tested, old frameworks. So if I forked one of them I would just be deleting most of the code anyway, it wouldn't really be a fork. In truth the majority of the work in creating the API is in creating the object model.

## What are the aims of this Api?

- A minimal subset of the Binance API for creating a spot trading bot.
- Latest `dotnet` framework.
- Fully asynchronous.
- Simple and consistent.
- Fully tested.
- Easily maintainable.
- StyleCop standard compliant.
- No compilation warnings.

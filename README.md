# StaticData 


· Description:

Tool with Common Data used in multiple .NET applications like Countries, Cities, Currencies, ExchangeRates...


· Getting started:

There are two ways for using StaticData tool.
Examples on Wiki are availaible

- By using StaticData nuget:
In this case, if the intention is to use only the core features of the project, there is a factory called StaticDataServiceFactory with some static methods that will provide the different services within the tool. It is only needed to pass a ConfigurationBase that could be create by default or could be configured before.

- By using ASPNET project (Only ASP.NET Core 2.0 and above)
In this case, the tool provides of an extension method for configuring services inside the DI Container. 
ConfigurationHelper -> RegisterStaticDataServices

Once dependencies are registered, you can use by injecting the different availaible services on the needed class
- ICountryService
- ICityService
- ICurrencyService
- IExcangeRateService

Another implementation that the ASPNET nuget provides is a Controller with all needed methods for retrieving information of all services, without doing anything else.

- Root path: api/staticdata
  - cities/{country}
  - cities  [Countries array on body]
  - countries
  - countries/{countryName}
  - countries/{alphaType}/{alphaCode} *
  - currencies
  - currencies/{currencySymbol}
  - exchangerates/{currencyFrom}/{currencyTo}/{date} **
  - exchangerates/latest/{currency}
  - exchangerates/historical/{currency}/{beginDate}/{endDate} **
  - exchangerates/bidirectional/{currencyFrom}/{currencyTo}/{date}
  - exchangerates/configured/latest
  - exchangerates/configured/historical/{beginDate}/{endDate} **
 
 
 *: Alpha types: Alpha3, Alpha2
 
 **: All date dormats: dd-MM-yyyy
 

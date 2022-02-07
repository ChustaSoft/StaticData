# StaticData 
---
- *Base library:*
  - [![Build Status](https://dev.azure.com/chustasoft/SocialNET/_apis/build/status/OpenStack/StaticData/%5BRELEASE%5D%20-%20ChustaSoft%20StaticData%20(NuGet)?branchName=master)](https://dev.azure.com/chustasoft/SocialNET/_build/latest?definitionId=9&branchName=master)
  - [![NuGet](https://img.shields.io/nuget/v/ChustaSoft.StaticData)](https://www.nuget.org/packages/ChustaSoft.StaticData)

- *AspNet library:*
  - [![Build Status](https://dev.azure.com/chustasoft/SocialNET/_apis/build/status/OpenStack/StaticData/%5BRELEASE%5D%20-%20ChustaSoft%20StaticData%20AspNet%20(NuGet)?branchName=master)](https://dev.azure.com/chustasoft/SocialNET/_build/latest?definitionId=4&branchName=master)
  - [![NuGet](https://img.shields.io/nuget/v/ChustaSoft.StaticData.AspNet)](https://www.nuget.org/packages/ChustaSoft.StaticData.AspNet)

Prerequisites:
- .NET Core 3.1
- .NET 5.0
- .NET 6.0

· Description:

Tool with Common Data used in multiple .NET applications like Countries, Cities, Currencies, ExchangeRates...


· Getting started:

There are two ways for using StaticData tool.
Examples on Wiki are availaible

- By using StaticData nuget:
In this case, if the intention is to use only the core features of the project, there is a factory called StaticDataServiceFactory with some static methods that will provide the different services within the tool. It is only needed to pass a ConfigurationBase that could be create by default or could be configured before.

In addition, in both cases you have an static configuration method ConfigurationHelper -> RegisterStaticDataServices that needs a IStaticDataConfigurationBuilder in order to register the specific configuration (Examples of configuration availabile also in the wiki


- By using ASPNET project
In this case, the tool provides of an extension method for configuring services inside the DI Container. 


NOTE: NuGet has been renamed from ChustaSoft.StaticData.AspMvc to ChustaSoft.StaticData.AspNet since version 1.0.1.2, which is compatible with the last ChustaSoft.StaticData.AspMvc version. Update the NuGet to the new one is recomended

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
 
 *Thanks for using and contributing*
---
[![Twitter Follow](https://img.shields.io/twitter/follow/ChustaSoft?label=Follow%20us&style=social)](https://twitter.com/ChustaSoft)


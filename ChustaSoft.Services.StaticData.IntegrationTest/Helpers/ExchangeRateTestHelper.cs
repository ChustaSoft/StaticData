using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Configuration;
using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;
using System.Collections.Generic;

namespace ChustaSoft.Services.StaticData.IntegrationTest.Helpers
{
    public class ExchangeRateTestHelper
    {

        #region Internal methods

        internal static IExchangeRateMultipleRepository CreateMockMultipleRepository()
        {
            var mockedConfiguration = new InternalConfiguration(StaticDataConfigurationBuilder.Generate());

            return new ExchangeRateMultipleExternalService(mockedConfiguration);
        }

        internal static IExchangeRateSingleRepository CreateMockSingleRepository()
        {
            var mockedConfiguration = new InternalConfiguration(StaticDataConfigurationBuilder.Generate().AddCurrencyConverterApiKey(TestKeys.TestFreeCurrencyConverterKey));

            return new ExchangeRateSingleExternalService(mockedConfiguration);
        }

        internal static IExchangeRateService CreateMockService(bool goodConfiguration)
        {
            var mockedConfiguration = StaticDataConfigurationBuilder.Generate().SetBaseCurrency(GetMockedConfiguredCurrencyBase());
            
            if(goodConfiguration)
                mockedConfiguration.AddConfiguredCurrencies(GetMockedConfiguredCurrencies());
            else
                mockedConfiguration.AddConfiguredCurrencies(GetMockedConfiguredCurrenciesWithUnknown());

            var multipleRepository = CreateMockMultipleRepository();
            var singleRepository = CreateMockSingleRepository();

            return new ExchangeRateService(new InternalConfiguration(mockedConfiguration), singleRepository, multipleRepository);
        }

        internal static string GetMockedConfiguredCurrencyBase() => "USD";


        internal static List<string> GetMockedConfiguredCurrencies() => new List<string> { "XOF", "EUR" };

        internal static List<string> GetMockedConfiguredCurrenciesWithUnknown() => new List<string> { "XOF", "TEST" };


        #endregion

    }
}

using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers
{
    public class ExchangeRateTestHelper
    {

        #region Internal methods

        internal static IExchangeRateMultipleRepository CreateMockMultipleRepository()
        {
            var mockedConfiguration = new ConfigurationBase();

            return new ExchangeRateMultipleExternalService(mockedConfiguration);
        }

        internal static IExchangeRateSingleRepository CreateMockSingleRepository()
        {
            var mockedConfiguration = new ConfigurationBase();

            return new ExchangeRateSingleExternalService(mockedConfiguration);
        }

        internal static IExchangeRateService CreateMockService()
        {
            var configuration = new ConfigurationBase();
            configuration.SetBaseCurency(GetMockedConfiguredCurrencyBase());
            configuration.SetConfiguredCurrencies(GetMockedConfiguredCurrencies());

            var multipleRepository = CreateMockMultipleRepository();
            var singleRepository = CreateMockSingleRepository();

            return new ExchangeRateService(configuration, singleRepository, multipleRepository);
        }

        internal static string GetMockedConfiguredCurrencyBase() => "USD";

        internal static List<string> GetMockedConfiguredCurrencies() => new List<string> { "XOF", "HKD" };


        #endregion

    }
}

using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Configuration;
using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;

namespace ChustaSoft.Services.StaticData.IntegrationTest.Helpers
{
    internal static class CurrencyTestHelper
    {

        #region Internal methods

        internal static ICurrencyRepository CreateMockRepository()
        {
            var mockedConfiguration = new InternalConfiguration(StaticDataConfigurationBuilder.Generate().AddCurrencyConverterApiKey(TestKeys.TestFreeCurrencyConverterKey));

            return new CurrencyExternalService(mockedConfiguration);
        }

        internal static ICurrencyService CreateMockService()
        {
            var repository = CreateMockRepository();

            return new CurrencyService(repository);
        }

        #endregion

    }
}

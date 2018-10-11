using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;
using System;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers
{
    internal static class CurrencyTestHelper
    {

        #region Internal methods

        internal static ICurrencyRepository CreateMockRepository()
        {
            var mockedConfiguration = new ConfigurationBase();

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

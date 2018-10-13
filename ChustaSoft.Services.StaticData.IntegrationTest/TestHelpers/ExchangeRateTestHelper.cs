using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers
{
    public class ExchangeRateTestHelper
    {

        #region Internal methods

        internal static IExchangeRateRepository CreateMockRepository()
        {
            var mockedConfiguration = new ConfigurationBase();

            return new ExchangeRateExternalService(mockedConfiguration);
        }

        internal static IExchangeRateQueryableRepository CreateMockQueryableRepository()
        {
            var mockedConfiguration = new ConfigurationBase();

            return new ExchangeRateQueryableExternalService(mockedConfiguration);
        }

        //internal static IExchangeRateService CreateMockService()
        //{
        //    var repository = CreateMockRepository();

        //    return new ExchangeRateService(repository);
        //}

        #endregion

    }
}

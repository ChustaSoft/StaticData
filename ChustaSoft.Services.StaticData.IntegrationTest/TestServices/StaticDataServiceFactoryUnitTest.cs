using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Factories;
using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestServices
{
    [TestClass]
    [TestCategory(TestCategories.StaticDataServiceFactoryCategory)]
    public class StaticDataServiceFactoryUnitTest
    {

        #region Test cases

        [TestMethod]
        public void Given_Configuration_When_GetExchangeRateServiceInvoked_Then_IExchangeRateServiceRetrived()
        {
            var configuration = new ConfigurationBase();

            var service = StaticDataServiceFactory.GetExchangeRateService(configuration);

            Assert.IsNotNull(service);
            Assert.AreEqual(typeof(ExchangeRateService), service.GetType());
        }

        [TestMethod]
        public void Given_Configuration_When_GetCurrencyServiceInvoked_Then_ICurrencyServiceRetrived()
        {
            var configuration = new ConfigurationBase();

            var service = StaticDataServiceFactory.GetCurrencyService(configuration);

            Assert.IsNotNull(service);
            Assert.AreEqual(typeof(CurrencyService), service.GetType());
        }

        [TestMethod]
        public void Given_ConfigurationWithApiTrue_When_GetCountryServiceInvoked_Then_ICountryServiceWithExternalServiceRetrived()
        {
            var configuration = new ConfigurationBase();
            configuration.SetCountriesFromApi(true);

            var service = StaticDataServiceFactory.GetCountryService(configuration);

            Assert.IsNotNull(service);
            Assert.AreEqual(typeof(CountryService), service.GetType());
        }

        [TestMethod]
        public void Given_ConfigurationWithApiFalse_When_GetCountryServiceInvoked_Then_ICountryServiceWithLocalRepositoryRetrived()
        {
            var configuration = new ConfigurationBase();
            configuration.SetCountriesFromApi(false);

            var service = StaticDataServiceFactory.GetCountryService(configuration);

            Assert.IsNotNull(service);
            Assert.AreEqual(typeof(CountryService), service.GetType());
        }

        [TestMethod]
        public void Given_Configuration_When_GetCityServiceInvoked_Then_ICityServiceRetrived()
        {
            var configuration = new ConfigurationBase();
            configuration.SetCountriesFromApi(false);

            var service = StaticDataServiceFactory.GetCityService(configuration);

            Assert.IsNotNull(service);
            Assert.AreEqual(typeof(CityService), service.GetType());
        }

        #endregion

    }
}

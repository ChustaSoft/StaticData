using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestRepositories
{
    [TestClass]
    [TestCategory(TestCategories.ExchangeRateTestCategory)]
    public class ExchangeRateExternalServiceIntegrationTest
    {

        #region Test Fields

        private IExchangeRateRepository _serviceUnderTest;

        #endregion


        #region Test Workflow

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockRepository();
        }

        #endregion


        #region Test Cases

        [TestMethod]
        public void Given_Currency_When_GetLatestInvoked_Then_ExchangeRatesCollectionRetrived()
        {
            var currency = "USD";

            var data = _serviceUnderTest.GetLatest(currency).Result;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void Given_UnknownCurrency_When_GetLatestInvoked_Then_ExceptionThrown()
        {
            var currency = "UKCUR";

            Assert.ThrowsExceptionAsync<CurrencyNotFoundException>(() => _serviceUnderTest.GetLatest(currency));
        }

        [TestMethod]
        public void Given_CurrencyAndDateRange_When_GetHistoricalInvoked_Then_HistoricalExchangeRatesRetrived()
        {
            var currency = "EUR";
            DateTime beginDate = new DateTime(2017, 1, 1), endDate = new DateTime(2017, 12, 31);

            var historicalData = _serviceUnderTest.GetHistorical(currency, beginDate, endDate).Result;

            Assert.IsNotNull(historicalData);
            Assert.IsTrue(historicalData.All(x => x.To == currency));
            Assert.IsTrue(historicalData.All(x => x.Date >= beginDate && x.Date <= endDate));
        }

        [TestMethod]
        public void Given_UnexistingCurrencyAndDateRange_When_GetHistoricalInvoked_Then_ExceptionThrown()
        {
            var currency = "UKCUR";
            DateTime beginDate = new DateTime(2017, 1, 1), endDate = new DateTime(2017, 12, 31);

            Assert.ThrowsExceptionAsync<CurrencyNotFoundException>(() => _serviceUnderTest.GetHistorical(currency, beginDate, endDate));
        }


        #endregion

    }
}

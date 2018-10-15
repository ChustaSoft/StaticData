using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestServices
{

    [TestClass]
    [TestCategory(TestCategories.ExchangeRateTestCategory)]
    public class ExchangeRateServiceIntegrationTest
    {

        #region Test Fields

        private IExchangeRateService _serviceUnderTest;

        #endregion


        #region Test Workflow

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockService(true);
        }

        #endregion


        #region Test Cases

        [TestMethod]
        public void Given_CurrenciesAndDate_When_GetInvoked_Then_ActionResponseWithExchangeRateRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            var date = DateTime.Today;

            var actionResponse = _serviceUnderTest.Get(currencyFrom, currencyTo, date);

            Assert.IsNotNull(actionResponse);
            Assert.AreEqual(currencyFrom, actionResponse.Data.From);
            Assert.AreEqual(currencyTo, actionResponse.Data.To);
            Assert.AreNotEqual(actionResponse.Data.Rate, 0);
            Assert.AreEqual(date, actionResponse.Data.Date);
        }

        [TestMethod]
        public void Given_Currencies_When_GetInvoked_Then_ActionResponseWithCurrentExchangeRateRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";

            var actionResponse = _serviceUnderTest.Get(currencyFrom, currencyTo);

            Assert.IsNotNull(actionResponse);
            Assert.IsNotNull(actionResponse.Data.Date);
            Assert.AreEqual(currencyFrom, actionResponse.Data.From);
            Assert.AreEqual(currencyTo, actionResponse.Data.To);
            Assert.AreNotEqual(actionResponse.Data.Rate, 0);
        }

        [TestMethod]
        public void Given_UnexistingCurrencyAndDate_When_GetInvoked_Then_CActionResponseWithWithErrorsRetrived()
        {
            string currencyFrom = "INVCUR", currencyTo = "EUR";
            var date = DateTime.Today;

            var actionResult = _serviceUnderTest.Get(currencyFrom, currencyTo, date);

            Assert.IsTrue(actionResult.Errors.Any());
        }

        [TestMethod]
        public void Given_CurrenciesAndDate_When_GetBidirectionalInvoked_Then_ActionResponseWithExchangeRatesBothRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            var date = DateTime.Today;

            var actionResponse = _serviceUnderTest.GetBidirectional(currencyFrom, currencyTo, date);

            Assert.IsNotNull(actionResponse);
            Assert.AreEqual(actionResponse.Data.Count(), 2);
            Assert.IsTrue(actionResponse.Data.Any(x => x.From == currencyFrom));
            Assert.IsTrue(actionResponse.Data.Any(x => x.From == currencyTo));
            Assert.IsTrue(actionResponse.Data.Any(x => x.To == currencyTo));
            Assert.IsTrue(actionResponse.Data.Any(x => x.To == currencyFrom));
            Assert.IsTrue(actionResponse.Data.All(x => x.Rate > 0));
            Assert.IsTrue(actionResponse.Data.All(x => x.Date == date));
        }

        [TestMethod]
        public void Given_Currency_When_GetLatestInvoked_Then_ActionResponseWithExchangeRatesCollectionRetrived()
        {
            var currency = "USD";

            var actionResponse = _serviceUnderTest.GetLatest(currency);

            Assert.IsNotNull(actionResponse);
            Assert.IsTrue(actionResponse.Data.Any());
        }

        [TestMethod]
        public void Given_UnknownCurrency_When_GetLatestInvoked_Then_ActionResponseWithErrorsRetrived()
        {
            var currency = "UKCUR";

            var actionResponse = _serviceUnderTest.GetLatest(currency);

            Assert.IsTrue(actionResponse.Errors.Any());
        }

        [TestMethod]
        public void Given_CurrencyAndDateRange_When_GetHistoricalInvoked_Then_ActionResponseWithHistoricalExchangeRatesRetrived()
        {
            var currency = "EUR";
            DateTime beginDate = new DateTime(2017, 1, 1), endDate = new DateTime(2017, 12, 31);

            var actionResult = _serviceUnderTest.GetHistorical(currency, beginDate, endDate);

            Assert.IsNotNull(actionResult);
            Assert.IsTrue(actionResult.Data.All(x => x.To == currency));
            Assert.IsTrue(actionResult.Data.All(x => x.Date >= beginDate && x.Date <= endDate));
        }

        [TestMethod]
        public void Given_UnexistingCurrencyAndDateRange_When_GetHistoricalInvoked_Then_ActionResponseWithErrorsRetrived()
        {
            var currency = "UKCUR";
            DateTime beginDate = new DateTime(2017, 1, 1), endDate = new DateTime(2017, 12, 31);

            var actionResponse = _serviceUnderTest.GetHistorical(currency, beginDate, endDate);

            Assert.IsTrue(actionResponse.Errors.Any());
        }

        [TestMethod]
        public void Given_ConfiguredCurrencies_When_GetConfiguredLatestInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            var actionResponse = _serviceUnderTest.GetConfiguredLatest();

            Assert.IsNotNull(actionResponse);
            Assert.IsTrue(actionResponse.Data.All(x => x.To == ExchangeRateTestHelper.GetMockedConfiguredCurrencyBase()));

            foreach (var configCurrency in ExchangeRateTestHelper.GetMockedConfiguredCurrencies())
                Assert.IsTrue(actionResponse.Data.Any(x => x.From == configCurrency));
        }

        [TestMethod]
        public void Given_OneUnknownConfiguredCurrencyWithMore_When_GetConfiguredLatestInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesAndErrorsRetrived()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockService(false);

            var actionResponse = _serviceUnderTest.GetConfiguredLatest();

            Assert.IsNotNull(actionResponse);
            Assert.IsTrue(actionResponse.Data.All(x => x.To == ExchangeRateTestHelper.GetMockedConfiguredCurrencyBase()));
            Assert.IsTrue(actionResponse.Errors.Any());
            Assert.AreEqual(actionResponse.Flag, Common.Enums.ActionResponseType.Warning);
        }

        [TestMethod]
        public void Given_ConfiguredCurrenciesAndDates_When_GetGetConfiguredHistoricalInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            DateTime beginDate = new DateTime(2018, 1, 1), endDate = new DateTime(2018, 1, 7);

            var actionResponse = _serviceUnderTest.GetConfiguredHistorical(beginDate, endDate);

            Assert.IsNotNull(actionResponse);
            foreach (var configCurrency in ExchangeRateTestHelper.GetMockedConfiguredCurrencies())
                Assert.IsTrue(actionResponse.Data.Any(x => x.From == configCurrency));
        }

        [TestMethod]
        public void Given_OneUnknownConfiguredCurrencyWithMoreAndDates_When_GetGetConfiguredHistoricalInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockService(false);
            DateTime beginDate = new DateTime(2018, 1, 1), endDate = new DateTime(2018, 1, 7);

            var actionResponse = _serviceUnderTest.GetConfiguredHistorical(beginDate, endDate);

            Assert.IsNotNull(actionResponse);
            Assert.IsTrue(actionResponse.Errors.Any());
            Assert.AreEqual(actionResponse.Flag, Common.Enums.ActionResponseType.Warning);
        }

        #endregion

    }
}

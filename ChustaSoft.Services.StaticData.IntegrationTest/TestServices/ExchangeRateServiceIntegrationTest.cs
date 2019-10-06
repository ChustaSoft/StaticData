using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.Helpers;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ChustaSoft.Services.StaticIntegrationTest.TestServices
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
        public void Given_CurrenciesAndDate_When_GetInvoked_Then_resultWithExchangeRateRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            var date = DateTime.Today;

            var result = _serviceUnderTest.Get(currencyFrom, currencyTo, date);

            Assert.IsNotNull(result);
            Assert.AreEqual(currencyFrom, result.From);
            Assert.AreEqual(currencyTo, result.To);
            Assert.AreNotEqual(result.Rate, 0);
            Assert.AreEqual(date, result.Date);
        }

        [TestMethod]
        public void Given_CurrenciesAndDate_When_GetAsyncInvoked_Then_resultWithExchangeRateRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            var date = DateTime.Today;

            var result = _serviceUnderTest.GetAsync(currencyFrom, currencyTo, date).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(currencyFrom, result.From);
            Assert.AreEqual(currencyTo, result.To);
            Assert.AreNotEqual(result.Rate, 0);
            Assert.AreEqual(date, result.Date);
        }

        [TestMethod]
        public void Given_Currencies_When_GetInvoked_Then_resultWithCurrentExchangeRateRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";

            var result = _serviceUnderTest.Get(currencyFrom, currencyTo);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Date);
            Assert.AreEqual(currencyFrom, result.From);
            Assert.AreEqual(currencyTo, result.To);
            Assert.AreNotEqual(result.Rate, 0);
        }

        [TestMethod]
        public void Given_Currencies_When_GetAsyncInvoked_Then_resultWithCurrentExchangeRateRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";

            var result = _serviceUnderTest.GetAsync(currencyFrom, currencyTo).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Date);
            Assert.AreEqual(currencyFrom, result.From);
            Assert.AreEqual(currencyTo, result.To);
            Assert.AreNotEqual(result.Rate, 0);
        }

        [TestMethod]
        public void Given_UnexistingCurrencyAndDate_When_GetInvoked_Then_ExceptionThrown()
        {
            string currencyFrom = "INVCUR", currencyTo = "EUR";
            var date = DateTime.Today;

            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get(currencyFrom, currencyTo, date));
        }


        [TestMethod]
        public void Given_UnexistingCurrencyAndDate_When_GetAsyncInvoked_Then_ExceptionThrown()
        {
            string currencyFrom = "INVCUR", currencyTo = "EUR";
            var date = DateTime.Today;

            Assert.ThrowsExceptionAsync<AggregateException>(() => _serviceUnderTest.GetAsync(currencyFrom, currencyTo, date));
        }

        [TestMethod]
        public void Given_CurrenciesAndDate_When_GetBidirectionalInvoked_Then_resultWithExchangeRatesBothRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            var date = DateTime.Today;

            var result = _serviceUnderTest.GetBidirectional(currencyFrom, currencyTo, date);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.Any(x => x.From == currencyFrom));
            Assert.IsTrue(result.Any(x => x.From == currencyTo));
            Assert.IsTrue(result.Any(x => x.To == currencyTo));
            Assert.IsTrue(result.Any(x => x.To == currencyFrom));
            Assert.IsTrue(result.All(x => x.Rate > 0));
            Assert.IsTrue(result.All(x => x.Date == date));
        }

        [TestMethod]
        public void Given_CurrenciesAndDate_When_GetBidirectionalAsyncInvoked_Then_resultWithExchangeRatesBothRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            var date = DateTime.Today;

            var result = _serviceUnderTest.GetBidirectionalAsync(currencyFrom, currencyTo, date).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.Any(x => x.From == currencyFrom));
            Assert.IsTrue(result.Any(x => x.From == currencyTo));
            Assert.IsTrue(result.Any(x => x.To == currencyTo));
            Assert.IsTrue(result.Any(x => x.To == currencyFrom));
            Assert.IsTrue(result.All(x => x.Rate > 0));
            Assert.IsTrue(result.All(x => x.Date == date));
        }

        [TestMethod]
        public void Given_Currency_When_GetLatestInvoked_Then_resultWithExchangeRatesCollectionRetrived()
        {
            var currency = "USD";

            var result = _serviceUnderTest.GetLatest(currency);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Given_Currency_When_GetLatestAsyncInvoked_Then_resultWithExchangeRatesCollectionRetrived()
        {
            var currency = "USD";

            var result = _serviceUnderTest.GetLatestAsync(currency).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Given_UnknownCurrency_When_GetLatestInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.GetLatest("UKCUR"));
        }

        [TestMethod]
        public void Given_UnknownCurrency_When_GetLatestAsyncInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsExceptionAsync<AggregateException>(() => _serviceUnderTest.GetLatestAsync("UKCUR"));
        }

        [TestMethod]
        public void Given_CurrencyAndDateRange_When_GetHistoricalInvoked_Then_resultWithHistoricalExchangeRatesRetrived()
        {
            var currency = "EUR";
            DateTime beginDate = new DateTime(2017, 1, 1), endDate = new DateTime(2017, 12, 31);

            var actionResult = _serviceUnderTest.GetHistorical(currency, beginDate, endDate);

            Assert.IsNotNull(actionResult);
            Assert.IsTrue(actionResult.All(x => x.To == currency));
            Assert.IsTrue(actionResult.All(x => x.Date >= beginDate && x.Date <= endDate));
        }

        [TestMethod]
        public void Given_CurrencyAndDateRange_When_GetHistoricalAsyncInvoked_Then_resultWithHistoricalExchangeRatesRetrived()
        {
            var currency = "EUR";
            DateTime beginDate = new DateTime(2017, 1, 1), endDate = new DateTime(2017, 12, 31);

            var actionResult = _serviceUnderTest.GetHistoricalAsync(currency, beginDate, endDate).Result;

            Assert.IsNotNull(actionResult);
            Assert.IsTrue(actionResult.All(x => x.To == currency));
            Assert.IsTrue(actionResult.All(x => x.Date >= beginDate && x.Date <= endDate));
        }

        [TestMethod]
        public void Given_UnexistingCurrencyAndDateRange_When_GetHistoricalInvoked_Then_resultWithErrorsRetrived()
        {
            var currency = "UKCUR";
            DateTime beginDate = new DateTime(2017, 1, 1), endDate = new DateTime(2017, 12, 31);

            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.GetHistorical(currency, beginDate, endDate));
        }

        [TestMethod]
        public void Given_UnexistingCurrencyAndDateRange_When_GetHistoricalAsyncInvoked_Then_resultWithErrorsRetrived()
        {
            var currency = "UKCUR";
            DateTime beginDate = new DateTime(2017, 1, 1), endDate = new DateTime(2017, 12, 31);

            Assert.ThrowsExceptionAsync<AggregateException>(() => _serviceUnderTest.GetHistoricalAsync(currency, beginDate, endDate));
        }

        [TestMethod]
        public void Given_ConfiguredCurrencies_When_GetConfiguredLatestInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            var result = _serviceUnderTest.GetConfiguredLatest();
            var exchangeRatesRetrived = result.SelectMany(x => x.Value.ExchangeRates);

            Assert.IsNotNull(result);
            Assert.IsTrue(exchangeRatesRetrived.All(x => x.To == ExchangeRateTestHelper.GetMockedConfiguredCurrencyBase()));

            foreach (var configCurrency in ExchangeRateTestHelper.GetMockedConfiguredCurrencies())
                Assert.IsTrue(exchangeRatesRetrived.Any(x => x.From == configCurrency));
        }


        [TestMethod]
        public void Given_ConfiguredCurrencies_When_GetConfiguredLatestAsyncInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            var result = _serviceUnderTest.GetConfiguredLatestAsync().Result;
            var exchangeRatesRetrived = result.SelectMany(x => x.Value.ExchangeRates);

            Assert.IsNotNull(result);
            Assert.IsTrue(exchangeRatesRetrived.All(x => x.To == ExchangeRateTestHelper.GetMockedConfiguredCurrencyBase()));

            foreach (var configCurrency in ExchangeRateTestHelper.GetMockedConfiguredCurrencies())
                Assert.IsTrue(exchangeRatesRetrived.Any(x => x.From == configCurrency));
        }

        [TestMethod]
        public void Given_OneUnknownConfiguredCurrencyWithMore_When_GetConfiguredLatestInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesAndErrorsRetrived()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockService(false);

            var result = _serviceUnderTest.GetConfiguredLatest();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SelectMany(x => x.Value.ExchangeRates).All(x => x.To == ExchangeRateTestHelper.GetMockedConfiguredCurrencyBase()));
        }

        [TestMethod]
        public void Given_OneUnknownConfiguredCurrencyWithMore_When_GetConfiguredLatestAsyncInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesAndErrorsRetrived()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockService(false);

            var result = _serviceUnderTest.GetConfiguredLatestAsync().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SelectMany(x => x.Value.ExchangeRates).All(x => x.To == ExchangeRateTestHelper.GetMockedConfiguredCurrencyBase()));
        }

        [TestMethod]
        public void Given_ConfiguredCurrenciesAndDates_When_GetGetConfiguredHistoricalInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            DateTime beginDate = DateTime.Now.AddMonths(-1), endDate = DateTime.Now.AddMonths(-1).AddDays(7);

            var result = _serviceUnderTest.GetConfiguredHistorical(beginDate, endDate);
            var exchangeRatesRetrived = result.SelectMany(x => x.Value.ExchangeRates);

            Assert.IsNotNull(result);
            foreach (var configCurrency in ExchangeRateTestHelper.GetMockedConfiguredCurrencies())
                Assert.IsTrue(exchangeRatesRetrived.Any(x => x.From == configCurrency));
        }

        [TestMethod]
        public void Given_ConfiguredCurrenciesAndDates_When_GetGetConfiguredHistoricalAsyncInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            DateTime beginDate = DateTime.Now.AddMonths(-1), endDate = DateTime.Now.AddMonths(-1).AddDays(7);

            var result = _serviceUnderTest.GetConfiguredHistoricalAsync(beginDate, endDate).Result;
            var exchangeRatesRetrived = result.SelectMany(x => x.Value.ExchangeRates);

            Assert.IsNotNull(result);
            foreach (var configCurrency in ExchangeRateTestHelper.GetMockedConfiguredCurrencies())
                Assert.IsTrue(exchangeRatesRetrived.Any(x => x.From == configCurrency));
        }

        [TestMethod]
        public void Given_OneUnknownConfiguredCurrencyWithMoreAndDates_When_GetGetConfiguredHistoricalInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockService(false);
            DateTime beginDate = new DateTime(2018, 1, 1), endDate = new DateTime(2018, 1, 7);

            var result = _serviceUnderTest.GetConfiguredHistorical(beginDate, endDate);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Given_OneUnknownConfiguredCurrencyWithMoreAndDates_When_GetGetConfiguredHistoricalAsyncInvoked_Then_ActionResponseWithConfiguredAndBaseExchangeRatesRetrived()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockService(false);
            DateTime beginDate = new DateTime(2018, 1, 1), endDate = new DateTime(2018, 1, 7);

            var result = _serviceUnderTest.GetConfiguredHistoricalAsync(beginDate, endDate).Result;

            Assert.IsNotNull(result);
        }

        #endregion

    }
}

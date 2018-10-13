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
    public class ExchangeRateQueryableExternalServiceIntegrationTest
    {

        #region Test Fields

        private IExchangeRateQueryableRepository _serviceUnderTest;

        #endregion


        #region Test Workflow

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockQueryableRepository();
        }

        #endregion


        #region Test Cases

        [TestMethod]
        public void Given_CurrenciesAndDate_When_GetInvoked_Then_ExchangeRateRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            var date = DateTime.Today;

            var data = _serviceUnderTest.Get(currencyFrom, currencyTo, date).Result;

            Assert.IsNotNull(data);
            Assert.AreEqual(currencyFrom, data.From);
            Assert.AreEqual(currencyTo, data.To);
            Assert.AreNotEqual(data.Rate, 0);
            Assert.AreEqual(date, data.Date);
        }

        [TestMethod]
        public void Given_Currencies_When_GetInvoked_Then_CurrentExchangeRateRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            
            var data = _serviceUnderTest.Get(currencyFrom, currencyTo).Result;

            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Date);
            Assert.AreEqual(currencyFrom, data.From);
            Assert.AreEqual(currencyTo, data.To);
            Assert.AreNotEqual(data.Rate, 0);
        }

        [TestMethod]
        public void Given_UnexistingCurrencyAndDate_When_GetInvoked_Then_CurrencyNotFoundExceptionThrowed()
        {
            string currencyFrom = "INVCUR", currencyTo = "EUR";
            var date = DateTime.Today;

            Assert.ThrowsExceptionAsync<CurrencyNotFoundException>(() => _serviceUnderTest.Get(currencyFrom, currencyTo, date));
        }

        [TestMethod]
        public void Given_CurrenciesAndDate_When_GetBidirectionalInvoked_Then_ExchangeRatesBothRetrived()
        {
            string currencyFrom = "USD", currencyTo = "EUR";
            var date = DateTime.Today;

            var data = _serviceUnderTest.GetBidirectional(currencyFrom, currencyTo, date).Result;

            Assert.IsNotNull(data);
            Assert.AreEqual(data.Count(), 2);
            Assert.IsTrue(data.Any(x => x.From == currencyFrom));
            Assert.IsTrue(data.Any(x => x.From == currencyTo));
            Assert.IsTrue(data.Any(x => x.To == currencyTo));
            Assert.IsTrue(data.Any(x => x.To == currencyFrom));
            Assert.IsTrue(data.All(x => x.Rate > 0));
            Assert.IsTrue(data.All(x => x.Date == date));
        }

        #endregion

    }
}

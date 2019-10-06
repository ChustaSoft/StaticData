using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.Helpers;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ChustaSoft.Services.StaticData.IntegrationTest.TestServices
{
    [TestClass]
    [TestCategory(TestCategories.CurrencyTestCategory)]
    public class CurrencyServiceIntegrationTest
    {

        #region Test Fields

        private ICurrencyService _serviceUnderTest;

        #endregion


        #region Test Workflow

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceUnderTest = CurrencyTestHelper.CreateMockService();
        }

        #endregion


        #region Test Cases

        [TestMethod]
        public void Given_Nothing_When_GetAllInvoked_Then_CurrenciesRetrived()
        {
            var result = _serviceUnderTest.GetAll();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Given_Nothing_When_GetAllAsyncInvoked_Then_CurrenciesRetrived()
        {
            var result = _serviceUnderTest.GetAllAsync().Result;

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Given_CurrencyCode_When_GetInvoked_Then_CurrencyRetrived()
        {
            var currencySymbol = "EUR";

            var result = _serviceUnderTest.Get(currencySymbol);

            Assert.IsNotNull(result);
            Assert.AreEqual(currencySymbol, result.Id);
        }

        [TestMethod]
        public void Given_CurrencyCode_When_GetAsyncInvoked_Then_CurrencyRetrived()
        {
            var currencySymbol = "EUR";

            var result = _serviceUnderTest.GetAsync(currencySymbol).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(currencySymbol, result.Id);
        }

        [TestMethod]
        public void Given_UnexistingCurrencyCode_When_GetInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get("TESTBAD"));
        }

        [TestMethod]
        public void Given_UnexistingCurrencyCode_When_GetAsyncInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsExceptionAsync<AggregateException>(() => _serviceUnderTest.GetAsync("TESTBAD"));
        }

        #endregion

    }
}

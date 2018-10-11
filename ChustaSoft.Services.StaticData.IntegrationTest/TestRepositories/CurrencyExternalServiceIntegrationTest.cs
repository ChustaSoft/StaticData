using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestRepositories
{
    [TestClass]
    [TestCategory(TestCategories.CurrencyTestCategory)]
    public class CurrencyExternalServiceIntegrationTest
    {

        #region Test Fields

        private ICurrencyRepository _serviceUnderTest;

        #endregion


        #region Test Workflow

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceUnderTest = CurrencyTestHelper.CreateMockRepository();
        }

        #endregion


        #region Test Cases

        [TestMethod]
        public void Given_Nothing_When_GetAllInvoked_Then_CurrenciesRetrived()
        {
            var data = _serviceUnderTest.GetAll().Result;

            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void Given_CurrencyCode_When_GetInvoked_Then_CurrencyRetrived()
        {
            var currencySymbol = "EUR";

            var data = _serviceUnderTest.Get(currencySymbol).Result;

            Assert.IsNotNull(data);
            Assert.AreEqual(currencySymbol, data.Id);
        }

        [TestMethod]
        public void Given_UnexistingCurrencyCode_When_GetInvoked_Then_ExceptionThrown()
        {
            var currencySymbol = "TESTBAD";

            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get(currencySymbol).Result);
        }

        #endregion

    }
}

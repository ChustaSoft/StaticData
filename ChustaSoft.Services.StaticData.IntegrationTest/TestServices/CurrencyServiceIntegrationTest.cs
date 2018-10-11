using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var actionResult = _serviceUnderTest.GetAll();

            Assert.IsTrue(actionResult.Data.Any());
        }

        [TestMethod]
        public void Given_CurrencyCode_When_GetInvoked_Then_CurrencyRetrived()
        {
            var currencySymbol = "EUR";

            var actionResult = _serviceUnderTest.Get(currencySymbol);

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(currencySymbol, actionResult.Data.Id);
        }

        [TestMethod]
        public void Given_UnexistingCurrencyCode_When_GetInvoked_Then_ActionResultWithErrorsRetrived()
        {
            var currencySymbol = "TESTBAD";

            var actionResult = _serviceUnderTest.Get(currencySymbol);

            Assert.IsNotNull(actionResult);
            Assert.IsTrue(actionResult.Errors.Any());
        }

        #endregion

    }
}

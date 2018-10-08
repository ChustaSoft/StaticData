using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestServices
{
    [TestClass]
    [TestCategory(TestCategories.CountryTestCategory)]
    public class CountryServiceToLocalIntegrationTest
    {

        #region Test Fields

        private ICountryService _serviceUnderTest;

        #endregion


        #region Test Workflow Control

        [TestInitialize]
        public void InitializeTest()
        {
            _serviceUnderTest = CountryTestHelper.CreateMockService(typeof(CountryLocalRepository));
        }

        #endregion


        #region Test Cases

        [TestMethod]
        public void Given_Nothing_When_GetAllInvoked_Then_CountriesListRetrived()
        {
            var actionResponse = _serviceUnderTest.GetAll();

            Assert.IsTrue(actionResponse.Data.Any());
        }

        [TestMethod]
        public void Given_Alpha2Code_When_GetInvoked_Then_CountryRetrived()
        {
            var actionResponse = _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha2, "CR");

            Assert.IsNotNull(actionResponse.Data);
        }

        [TestMethod]
        public void Given_Alpha3Code_When_GetInvoked_Then_CountryRetrived()
        {
            var actionResponse = _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha3, "CRI");

            Assert.IsNotNull(actionResponse.Data);
        }

        [TestMethod]
        public void Given_CountryName_When_GetInvoked_Then_CountryRetrived()
        {
            var actionResponse = _serviceUnderTest.Get("Burkina Faso");

            Assert.IsNotNull(actionResponse.Data);
        }

        [TestMethod]
        public void Given_UnexistingName_When_GetInvoked_Then_ExceptionThrown()
        {
            var actionResponse = _serviceUnderTest.Get("Solar system");

            Assert.IsTrue(actionResponse.Errors.Any());
        }

        [TestMethod]
        public void Given_UnexistingAlpha2Code_When_GetInvoked_Then_ExceptionThrown()
        {
            var actionResponse = _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha2, "Solar system");

            Assert.IsTrue(actionResponse.Errors.Any());
        }

        [TestMethod]
        public void Given_UnexistingAlpha3Code_When_GetInvoked_Then_ExceptionThrown()
        {
            var actionResponse = _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha3, "Solar system");

            Assert.IsTrue(actionResponse.Errors.Any());
        }

        #endregion

    }
}

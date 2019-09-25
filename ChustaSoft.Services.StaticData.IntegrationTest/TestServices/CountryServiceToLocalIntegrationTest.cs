using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            var result = _serviceUnderTest.GetAll();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Given_Alpha2Code_When_GetInvoked_Then_CountryRetrived()
        {
            var result = _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha2, "CR");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Given_Alpha3Code_When_GetInvoked_Then_CountryRetrived()
        {
            var result = _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha3, "CRI");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Given_CountryName_When_GetInvoked_Then_CountryRetrived()
        {
            var result = _serviceUnderTest.Get("Burkina Faso");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Given_UnexistingName_When_GetInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get("Solar system"));
        }

        [TestMethod]
        public void Given_UnexistingAlpha2Code_When_GetInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha2, "Solar system"));
        }

        [TestMethod]
        public void Given_UnexistingAlpha3Code_When_GetInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha3, "Solar system"));
        }

        #endregion

    }
}

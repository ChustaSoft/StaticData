using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestServices
{
    [TestClass]
    [TestCategory(TestCategories.CountryTestCategory)]
    public class CountryLocalRepositoryIntegrationTest
    {

        #region Test Fields

        private ICountryRepository _serviceUnderTest;

        #endregion


        #region Test Workflow

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceUnderTest = CountryTestHelper.CreateMockRepository(typeof(CountryLocalRepository));
        }

        #endregion


        #region Test Cases

        [TestMethod]
        public void Given_Nothing_When_GetAllInvoked_Then_CountriesListRetrived()
        {
            var data = _serviceUnderTest.GetAll().Result;

            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void Given_Alpha2Code_When_GetInvoked_Then_CountryRetrived()
        {
            var data = _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha2, "CR").Result;

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void Given_Alpha3Code_When_GetInvoked_Then_CountryRetrived()
        {
            var data = _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha3, "CRI").Result;

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void Given_CountryName_When_GetInvoked_Then_CountryRetrived()
        {
            var data = _serviceUnderTest.Get("Burkina Faso").Result;

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void Given_UnexistingName_When_GetInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get("Solar system").Result);
        }

        [TestMethod]
        public void Given_UnexistingAlpha2Code_When_GetInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha2, "Solar system").Result);
        }

        [TestMethod]
        public void Given_UnexistingAlpha3Code_When_GetInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<AggregateException>(() => _serviceUnderTest.Get(Enums.AlphaCodeType.Alpha3, "Solar system").Result);
        }

        #endregion

    }
}

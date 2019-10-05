using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestServices
{
    [TestClass]
    [TestCategory(TestCategories.CityTestCategory)]
    public class CityLocalRepositoryIntegrationTest
    {

        #region Fields

        private ICityRepository _serviceUnderTest;

        #endregion

        #region Test Workflow

        [TestInitialize]
        public void TestInitialization()
        {
            _serviceUnderTest = CityTestHelper.CreateMockRepository();
        }

        #endregion


        #region Test methods

        [TestMethod]
        public void Given_CountryStr_When_GetAsyncInvoked_Then_CitiesRetrived()
        {
            var country = "Burkina Faso";

            var cities = _serviceUnderTest.GetAsync(country).Result;

            Assert.IsTrue(cities.Any());
        }

        [TestMethod]
        public void Given_UnexistingCountryStr_When_GetAsyncInvoked_Then_ExceptionThrown()
        {
            var country = "Solar system";

            Assert.ThrowsExceptionAsync<AggregateException>(() => _serviceUnderTest.GetAsync(country));
        }

        #endregion

    }
}

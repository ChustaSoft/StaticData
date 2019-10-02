using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ChustaSoft.Services.StaticData.IntegrationTest.TestServices
{
    [TestClass]
    [TestCategory(TestCategories.CityTestCategory)]
    public class CityServiceIntegrationTest
    {

        #region Test Fields

        private ICityService _serviceUnderTest;

        #endregion


        #region Test Workflow Control

        [TestInitialize]
        public void InitializeTest()
        {
            _serviceUnderTest = CityTestHelper.CreateMockService();
        }

        #endregion


        #region Test Cases

        [TestMethod]
        public void Given_CountryStr_When_GetInvoked_Then_CitiesRetrived()
        {
            var country = "Burkina Faso";

            var result = _serviceUnderTest.Get(country);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Given_CountryStr_When_GetAsyncInvoked_Then_CitiesRetrived()
        {
            var country = "Burkina Faso";

            var result = _serviceUnderTest.GetAsync(country).Result;

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Given_UnexistingCountryStr_When_GetInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsException<CountryNotFoundException>(() => _serviceUnderTest.Get("Solar system"));
        }

        [TestMethod]
        public void Given_UnexistingCountryStr_When_GetAsyncInvoked_Then_ExceptionThrown()
        {
            Assert.ThrowsExceptionAsync<CountryNotFoundException>(() => _serviceUnderTest.GetAsync("Solar system"));
        }

        [TestMethod]
        public void Given_CountryStrList_When_GetInvoked_Then_CitiesRetrived()
        {
            string country1 = "Burkina Faso", country2 = "Angola", country3 = "Ghana";
            var countries = new List<string> { country1, country2, country3 };

            var result = _serviceUnderTest.Get(countries);

            Assert.IsTrue(result[country1].Found);
            Assert.IsTrue(result[country2].Found);
            Assert.IsTrue(result[country3].Found);
            Assert.IsTrue(result[country1].Cities.All(x => x.CountryAlpha2Code == "BF"));
            Assert.IsTrue(result[country2].Cities.All(x => x.CountryAlpha2Code == "AO"));
            Assert.IsTrue(result[country3].Cities.All(x => x.CountryAlpha2Code == "GH"));
        }

        [TestMethod]
        public void Given_CountryStrList_When_GetAsyncInvoked_Then_CitiesRetrived()
        {
            string country1 = "Burkina Faso", country2 = "Angola", country3 = "Ghana";
            var countries = new List<string> { country1, country2, country3 };

            var result = _serviceUnderTest.GetAsync(countries).Result;

            Assert.IsTrue(result[country1].Found);
            Assert.IsTrue(result[country2].Found);
            Assert.IsTrue(result[country3].Found);
            Assert.IsTrue(result[country1].Cities.All(x => x.CountryAlpha2Code == "BF"));
            Assert.IsTrue(result[country2].Cities.All(x => x.CountryAlpha2Code == "AO"));
            Assert.IsTrue(result[country3].Cities.All(x => x.CountryAlpha2Code == "GH"));
        }

        [TestMethod]
        public void Given_ExistingAndUnexistingCountryStrList_When_GetInvoked_Then_CitiesAndErrorsRetrived()
        {
            string country1 = "Burkina Faso", country2 = "Angola", unexistingCountry = "Solar System";
            var countries = new List<string> { country1, country2, unexistingCountry };

            var result = _serviceUnderTest.Get(countries);

            Assert.IsTrue(result[country1].Found);
            Assert.IsTrue(result[country2].Found);
            Assert.IsFalse(result[unexistingCountry].Found);
            Assert.IsTrue(result[country1].Cities.All(x => x.CountryAlpha2Code == "BF"));
            Assert.IsTrue(result[country2].Cities.All(x => x.CountryAlpha2Code == "AO"));
            Assert.IsFalse(result[unexistingCountry].Cities.Any());
        }

        [TestMethod]
        public void Given_ExistingAndUnexistingCountryStrList_When_GetAsyncInvoked_Then_CitiesAndErrorsRetrived()
        {
            string country1 = "Burkina Faso", country2 = "Angola", unexistingCountry = "Solar System";
            var countries = new List<string> { country1, country2, unexistingCountry };

            var result = _serviceUnderTest.GetAsync(countries).Result;

            Assert.IsTrue(result[country1].Found);
            Assert.IsTrue(result[country2].Found);
            Assert.IsFalse(result[unexistingCountry].Found);
            Assert.IsTrue(result[country1].Cities.All(x => x.CountryAlpha2Code == "BF"));
            Assert.IsTrue(result[country2].Cities.All(x => x.CountryAlpha2Code == "AO"));
            Assert.IsFalse(result[unexistingCountry].Cities.Any());
        }

        #endregion

    }
}

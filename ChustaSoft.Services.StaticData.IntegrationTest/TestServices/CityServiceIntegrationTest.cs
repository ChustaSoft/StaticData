using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Repositories;
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

            var actionResponse = _serviceUnderTest.Get(country);

            Assert.IsTrue(actionResponse.Data.Any());
        }

        [TestMethod]
        public void Given_UnexistingCountryStr_When_GetInvoked_Then_ErrorsRetrived()
        {
            var country = "Solar system";

            var actionResponse = _serviceUnderTest.Get(country);

            Assert.IsTrue(actionResponse.Errors.Any());
        }

        [TestMethod]
        public void Given_CountryStrList_When_GetInvoked_Then_CitiesRetrived()
        {
            var countries = new List<string> { "Burkina Faso", "Angola", "Ghana" };

            var actionResponse = _serviceUnderTest.Get(countries);

            Assert.IsTrue(actionResponse.Data.Any());
            Assert.IsTrue(actionResponse.Data.Any(x => x.CountryAlpha2Code == "BF"));
            Assert.IsTrue(actionResponse.Data.Any(x => x.CountryAlpha2Code == "AO"));
            Assert.IsTrue(actionResponse.Data.Any(x => x.CountryAlpha2Code == "GH"));
        }

        [TestMethod]
        public void Given_ExistingAndUnexistingCountryStrList_When_GetInvoked_Then_CitiesAndErrorsRetrived()
        {
            var unexistingCountry = "Solar System";
            var countries = new List<string> { "Burkina Faso", "Ghana", unexistingCountry };

            var actionResponse = _serviceUnderTest.Get(countries);

            Assert.IsTrue(actionResponse.Data.Any());
            Assert.IsTrue(actionResponse.Data.Any(x => x.CountryAlpha2Code == "BF"));
            Assert.IsTrue(actionResponse.Data.Any(x => x.CountryAlpha2Code == "GH"));
            Assert.IsTrue(actionResponse.Errors.Any(x => x.Text.Contains(unexistingCountry)));
        }

        #endregion

    }
}

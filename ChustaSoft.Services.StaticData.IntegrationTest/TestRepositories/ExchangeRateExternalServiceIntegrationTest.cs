using ChustaSoft.Services.StaticData.IntegrationTest.TestConstants;
using ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers;
using ChustaSoft.Services.StaticData.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChustaSoft.Services.StaticData.IntegrationTest.TestRepositories
{
    [TestClass]
    [TestCategory(TestCategories.ExchangeRateTestCategory)]
    public class ExchangeRateExternalServiceIntegrationTest
    {

        #region Test Fields

        private IExchangeRateRepository _serviceUnderTest;

        #endregion


        #region Test Workflow

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceUnderTest = ExchangeRateTestHelper.CreateMockRepository();
        }

        #endregion


        #region Test Cases

       

        #endregion

    }
}

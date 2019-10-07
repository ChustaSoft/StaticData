using ChustaSoft.Services.StaticData.Configuration;
using ChustaSoft.Services.StaticData.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ChustaSoft.Services.StaticData.IntegrationTest.TestConfiguration
{
    [TestClass]
    public class StaticDataConfigurationBuilderUnitTest
    {

        #region Test methods

        [TestMethod]
        public void Given_Nothing_When_GenerateAndBuild_Then_DefaultConfigRetrived()
        {
            var testConfig = StaticDataConfigurationBuilder.Generate().Build();

            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsFalse(testConfig.ConfiguredCurrencies.Any());
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_ApiPrerably_When_GenerateAndSetApiPreferablyAndBuild_Then_ConfigRetrivedWithSamePreferably()
        {
            var preferability = false;
            var testConfig = StaticDataConfigurationBuilder.Generate()
                .SetApiPreferably(preferability)
                .Build();

            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsFalse(testConfig.ConfiguredCurrencies.Any());
            Assert.AreEqual(testConfig.ApiDataPeferably, preferability);
        }

        [TestMethod]
        public void Given_DefaultCurrency_When_GenerateAndSetBaseCurrencyAndBuild_Then_ConfigRetrivedWithSameDefaultCurrency()
        {
            var defaultCurrency = "EUR";
            var testConfig = StaticDataConfigurationBuilder.Generate()
                .SetBaseCurrency(defaultCurrency)
                .Build();

            Assert.AreEqual(testConfig.BaseCurrency, defaultCurrency);
            Assert.IsFalse(testConfig.ConfiguredCurrencies.Any());
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_ConfiguredCurrency_When_GenerateAddConfiguredCurrencyAndBuild_Then_ConfigRetrivedWithSpecifiedConfigCurrency()
        {
            var configCurrency = "EUR";
            var testConfig = StaticDataConfigurationBuilder.Generate()
                .AddConfiguredCurrency(configCurrency)
                .Build();

            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => x.Equals(configCurrency)));
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_TwoConfiguredCurrencies_When_GenerateAddConfiguredCurrencyAndBuild_Then_ConfigRetrivedWithSpecifiedConfigCurrencies()
        {
            string configCurrency1 = "EUR", configCurrency2 = "GBP";
            var testConfig = StaticDataConfigurationBuilder.Generate()
                .AddConfiguredCurrency(configCurrency1)
                .AddConfiguredCurrency(configCurrency2)
                .Build();

            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => x.Equals(configCurrency1)));
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => x.Equals(configCurrency2)));
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_ConfiguredCurrenciesCollection_When_GenerateAddConfiguredCurrenciesAndBuild_Then_ConfigRetrivedWithSpecifiedConfigCurrencies()
        {
            var configCurrencies = new List<string> { "EUR", "GBP" };
            var testConfig = StaticDataConfigurationBuilder.Generate()
                .AddConfiguredCurrencies(configCurrencies)
                .Build();

            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => configCurrencies.Contains(x)));
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_ConfiguredCurrenciesCollectionAndSingleOne_When_GenerateAddConfiguredCurrenciesAndBuild_Then_ConfigRetrivedWithSpecifiedConfigCurrencies()
        {
            var configCurrency = "CHF";
            var configCurrencies = new List<string> { "EUR", "GBP" };
            var testConfig = StaticDataConfigurationBuilder.Generate()
                .AddConfiguredCurrencies(configCurrencies)
                .AddConfiguredCurrency(configCurrency)
                .Build();

            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => x.Equals(configCurrency)));
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => configCurrencies.Contains(x)));
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_ConfiguredCurrenciesCollectionAndSingleOneRepeated_When_GenerateAddConfiguredCurrenciesAndBuild_Then_ConfigRetrivedWithSpecifiedConfigCurrenciesWithErrors()
        {
            var configCurrencies = new List<string> { "EUR", "GBP" };
            var configCurrency = "EUR";
            var testConfigBuilder = StaticDataConfigurationBuilder.Generate()
                .AddConfiguredCurrencies(configCurrencies)
                .AddConfiguredCurrency(configCurrency);
            var testConfig = testConfigBuilder.Build();

            Assert.IsTrue(testConfigBuilder.Errors.Any());
            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => x.Equals(configCurrency)));
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => configCurrencies.Contains(x)));
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_ConfiguredCurrencyAndConfiguredCurrenciesCollectionWithDuplicated_When_GenerateAddConfiguredCurrenciesAndBuild_Then_ConfigRetrivedWithSpecifiedConfigCurrenciesWithErrors()
        {
            var configCurrency = "EUR";
            var configCurrencies = new List<string> { "EUR", "GBP" };
            var testConfigBuilder = StaticDataConfigurationBuilder.Generate()
                .AddConfiguredCurrency(configCurrency)
                .AddConfiguredCurrencies(configCurrencies);
            var testConfig = testConfigBuilder.Build();

            Assert.IsTrue(testConfigBuilder.Errors.Any());
            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => x.Equals(configCurrency)));
            Assert.IsTrue(testConfig.ConfiguredCurrencies.Any(x => configCurrencies.Contains(x)));
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_CurrencyConverterApiKey_When_GenerateAndAddCurrencyConverterApiKeyAndBuild_Then_DefaultConfigRetrived()
        {
            var testKey = "TEST_KEY_STATICDATA";
            var testConfig = StaticDataConfigurationBuilder.Generate()
                .AddApiKey(ApiType.CurrencyConverter, testKey)
                .Build();

            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsFalse(testConfig.ConfiguredCurrencies.Any());
            Assert.AreEqual(testConfig.CurrencyConversionApiKey, testKey);
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        [TestMethod]
        public void Given_EmptyCurrencyConverterApiKey_When_GenerateAndAddCurrencyConverterApiKeyAndBuild_Then_ErrorRetrived()
        {
            var testConfigBuilder = StaticDataConfigurationBuilder.Generate()
                .AddApiKey(ApiType.CurrencyConverter, string.Empty);
            var testConfig = testConfigBuilder.Build();

            Assert.IsTrue(testConfigBuilder.Errors.Any());
            Assert.AreEqual(testConfig.BaseCurrency, StaticDataConfigurationBuilder.DEFAULT_BASE_CURRENCY);
            Assert.IsFalse(testConfig.ConfiguredCurrencies.Any());
            Assert.AreEqual(testConfig.CurrencyConversionApiKey, string.Empty);
            Assert.IsTrue(testConfig.ApiDataPeferably);
        }

        #endregion
    }
}

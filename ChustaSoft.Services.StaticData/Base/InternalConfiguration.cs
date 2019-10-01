using ChustaSoft.Services.StaticData.Constants;
using ChustaSoft.Services.StaticData.Helpers;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Base
{
    /// <summary>
    /// Base Configuration with default data for configuring StaticData use
    /// </summary>
    public class InternalConfiguration
    {

        #region Properties

        internal string CurrencyConverterApiKey { get; private set; }
        internal string CountriesApiUrl { get; private set; }
        internal string CurrenciesApiUrl { get; private set; }
        internal string ExchangeRatesApiUrl { get; private set; }
        internal string ExchangeRatesQueryableApiUrl { get; private set; }
        internal bool CountriesFromApi { get; private set; }
        internal string ConfiguredBaseCurrency { get; private set; }
        internal IEnumerable<string> ConfiguredCurrencies { get; private set; }

        #endregion


        #region Constructor

        public InternalConfiguration(StaticDataConfiguration configuredParams)
        {
            SetupConfiguredParams(configuredParams);
            SetupConstantUrls();
        }

        public InternalConfiguration(IStaticDataConfigurationBuilder builder)
        {
            var configuredParams = builder.Build();

            SetupConfiguredParams(configuredParams);
            SetupConstantUrls();
        }

        #endregion


        #region Private methods

        private void SetupConfiguredParams(StaticDataConfiguration configuredParams)
        {
            CountriesFromApi = configuredParams.ApiDataPeferably;
            ConfiguredBaseCurrency = configuredParams.BaseCurrency;
            ConfiguredCurrencies = configuredParams.ConfiguredCurrencies;
            CurrencyConverterApiKey = configuredParams.CurrencyConversionApiKey;
        }

        private void SetupConstantUrls()
        {
            CountriesApiUrl = ApiConnection.CountriesApiUrl;
            CurrenciesApiUrl = ApiConnection.CurrenciesApiUrl;
            ExchangeRatesApiUrl = ApiConnection.ExchangeRatesApiUrl;
            ExchangeRatesQueryableApiUrl = ApiConnection.ExchangeRatesQueryableApiUrl;
        }

        #endregion

    }
}

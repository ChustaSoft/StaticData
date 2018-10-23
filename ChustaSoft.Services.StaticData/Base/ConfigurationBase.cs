using ChustaSoft.Services.StaticData.Constants;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Base
{
    /// <summary>
    /// Base Configuration with default data for configuring StaticData use
    /// </summary>
    public class ConfigurationBase
    {

        #region Fields

        private bool _dataFromApi = false;

        #endregion


        #region Properties

        internal bool CountriesFromApi => _dataFromApi;
        
        internal string CountriesApiUrl { get; set; }

        internal string CurrenciesApiUrl { get; set; }

        internal string ExchangeRatesApiUrl { get; set; }

        internal string ExchangeRatesQueryableApiUrl { get; set; }

        internal string ConfiguredBaseCurrency { get; set; }

        internal IEnumerable<string> ConfiguredCurrencies { get; set; }

        #endregion


        #region Constructor

        public ConfigurationBase()
        {
            CountriesApiUrl = ApiConnection.CountriesApiUrl;
            CurrenciesApiUrl = ApiConnection.CurrenciesApiUrl;
            ExchangeRatesApiUrl = ApiConnection.ExchangeRatesApiUrl;
            ExchangeRatesQueryableApiUrl = ApiConnection.ExchangeRatesQueryableApiUrl;
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Use this to configure services that can provide data from local repositories or from API external service
        /// </summary>
        /// <param name="dataFromApi">Set if data should be retrived by API or not</param>
        public void SetCountriesFromApi(bool dataFromApi)
        {
            _dataFromApi = dataFromApi;
        }

        /// <summary>
        /// Use this method to configure a currency different than USD as base currency to compare requested exchangeRates
        /// </summary>
        /// <param name="configuredBaseCurrency">Three digits currency code to configure</param>
        public void SetBaseCurency(string configuredBaseCurrency)
        {
            ConfiguredBaseCurrency = configuredBaseCurrency;
        }

        /// <summary>
        /// Use this method to configure different currencies to map data in services
        /// </summary>
        /// <param name="configuredCurrencies">Collection of three digit currency codes</param>
        public void SetConfiguredCurrencies(IEnumerable<string> configuredCurrencies)
        {
            ConfiguredCurrencies = configuredCurrencies;
        }

        #endregion

    }
}

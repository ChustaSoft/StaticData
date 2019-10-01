using ChustaSoft.Common.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ChustaSoft.Services.StaticData.Helpers
{
    public class StaticDataConfigurationBuilder : IStaticDataConfigurationBuilder
    {

        #region Constants, Fields & Properties

        private const string DEFAULT_BASE_CURRENCY = "USD";
        private const bool DEFAULT_API_PREFERABILITY = true;


        private StaticDataConfiguration _staticDataConfiguration;

        public ICollection<ErrorMessage> Errors { get; set; }

        #endregion


        #region Constructor

        internal StaticDataConfigurationBuilder()
        {
            _staticDataConfiguration = new StaticDataConfiguration(DEFAULT_API_PREFERABILITY, DEFAULT_BASE_CURRENCY);
            Errors = Enumerable.Empty<ErrorMessage>().ToList();
        }


        public static StaticDataConfigurationBuilder Configure() => new StaticDataConfigurationBuilder();

        #endregion


        #region Public methods

        public StaticDataConfigurationBuilder SetApiPreferably(bool apiPreferably)
        {
            _staticDataConfiguration.ApiDataPeferably = apiPreferably;

            return this;
        }

        public StaticDataConfigurationBuilder SetBaseCurrency(string currencyBase)
        {
            _staticDataConfiguration.BaseCurrency = currencyBase;

            return this;
        }

        public StaticDataConfigurationBuilder AddConfiguredCurrency(string configuredCurrency)
        {
            TryAddCurrency(configuredCurrency);

            return this;
        }

        public StaticDataConfigurationBuilder AddConfiguredCurrencies(IEnumerable<string> configuredCurrencies)
        {
            if (!configuredCurrencies.Any())
                Errors.Add(new ErrorMessage(Common.Enums.ErrorType.Invalid, $"Currencies are empty"));

            foreach (var configuredCurrency in configuredCurrencies)
                TryAddCurrency(configuredCurrency);

            return this;
        }

        public StaticDataConfigurationBuilder AddCurrencyConverterApiKey(string apiKey)
        {
            _staticDataConfiguration.CurrencyConversionApiKey = apiKey;

            return this;
        }

        public StaticDataConfiguration Build() => _staticDataConfiguration;

        #endregion


        #region Private methods

        private void TryAddCurrency(string configuredCurrency)
        {
            if (_staticDataConfiguration.ConfiguredCurrencies.Contains(configuredCurrency))
                Errors.Add(new ErrorMessage(Common.Enums.ErrorType.Invalid, $"Currency: {configuredCurrency} already configured"));
            else
                _staticDataConfiguration.ConfiguredCurrencies.Add(configuredCurrency);
        }

        #endregion

    }
}

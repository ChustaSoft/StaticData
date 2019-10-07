using ChustaSoft.Common.Contracts;
using ChustaSoft.Services.StaticData.Enums;
using System.Collections.Generic;

namespace ChustaSoft.Services.StaticData.Configuration
{

    /// <summary>
    /// Builder designed to provide the application configuration to the StaticData tool
    /// </summary>
    public interface IStaticDataConfigurationBuilder : IBuilder<StaticDataConfiguration>
    {
        /// <summary>
        /// Use this specific method for specify a default currency for the tool as reference currency.
        /// By default USD will be configured if nothing is specified
        /// </summary>
        /// <param name="currencyBase">Configured currency as default/reference currency</param>
        /// <returns>Builder itself</returns>
        StaticDataConfigurationBuilder SetBaseCurrency(string currencyBase);

        /// <summary>
        /// In order to have specific conversions, it is possible to configure most useful currencies
        /// </summary>
        /// <param name="configuredCurrency">Frequently used currency</param>
        /// <returns>Builder itself</returns>
        StaticDataConfigurationBuilder AddConfiguredCurrency(string configuredCurrency);

        /// <summary>
        /// In order to have specific conversions, it is possible to configure most useful currencies
        /// </summary>
        /// <param name="configuredCurrencies">Frequently used currencies</param>
        /// <returns>Builder itself</returns>
        StaticDataConfigurationBuilder AddConfiguredCurrencies(IEnumerable<string> configuredCurrencies);

        /// <summary>
        /// Used to specify the custom API key for CurrencyConverter API
        /// <see href="https://free.currencyconverterapi.com/">HERE</see>
        /// </summary>
        /// <param name="apiKey">Custom obtained API key</param>
        /// <returns>Builder itself</returns>
        StaticDataConfigurationBuilder AddApiKey(ApiType apiType, string apiKey);

        /// <summary>
        /// If static data as Cities or Countries are preferred to be retrived from configured APIs
        /// True will be specified by default
        /// </summary>
        /// <param name="apiPreferably">True if is preferably to take by API, false for internal JSON DB</param>
        /// <returns>Builder itself</returns>
        StaticDataConfigurationBuilder SetApiPreferably(bool apiPreferably);

    }
}
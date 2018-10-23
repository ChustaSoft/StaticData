using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;


namespace ChustaSoft.Services.StaticData.AspNet.Configuration
{
    public static class ConfigurationHelper
    {

        #region Fields

        private const string STATICDATA_CONFIGURATION_CONFIG_PARAM = "StaticDataConfiguration";
        private const string DEFAULT_BASE_CURRENCY = "USD";
        private const bool DEFAULT_API_CONFIGURATION = false;

        #endregion


        #region Public methods

        /// <summary>
        /// Extension method availaible for configuring StaticData services
        /// Could be used for default configuration, or appsettings configurations inside Startup
        /// </summary>
        /// <param name="services">DI container</param>
        /// <param name="configuration">ASPNET Core configuration</param>
        public static void RegisterStaticDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(STATICDATA_CONFIGURATION_CONFIG_PARAM).Get<StaticDataConfiguration>();
            var configurationBase = GetConfiguration(configurationSection);

            RegisterServicesSingleton(services, configurationBase);
        }

        /// <summary>
        /// Extension method availaible for configuring StaticData services
        /// Could be used for manual configuring inside Startup
        /// </summary>
        /// <param name="services"></param>
        /// <param name="apiDataPeferably"></param>
        /// <param name="baseCurrency"></param>
        /// <param name="configuredCurrencies"></param>
        public static void RegisterStaticDataServices(this IServiceCollection services, bool apiDataPeferably, string baseCurrency, IEnumerable<string> configuredCurrencies)
        {
            var configurationBase = GetConfiguration(apiDataPeferably, baseCurrency, configuredCurrencies);

            RegisterServicesSingleton(services, configurationBase);
        }

        #endregion


        #region Private methods

        private static ConfigurationBase GetConfiguration(StaticDataConfiguration staticDataConfiguration)
        {
            if (staticDataConfiguration != null)
                return ConstructConfiguration(staticDataConfiguration.ApiDataPeferably, staticDataConfiguration.BaseCurrency, staticDataConfiguration.ConfiguredCurrencies);
            else
                return ConstructConfiguration(DEFAULT_API_CONFIGURATION, DEFAULT_BASE_CURRENCY, Enumerable.Empty<string>());
        }

        private static ConfigurationBase GetConfiguration(bool apiDataPeferably, string baseCurrency, IEnumerable<string> configuredCurrencies)
        {
            return ConstructConfiguration(apiDataPeferably, baseCurrency, configuredCurrencies);
        }

        private static ConfigurationBase ConstructConfiguration(bool apiDataPeferably, string baseCurrency, IEnumerable<string> configuredCurrencies)
        {
            var configuration = new ConfigurationBase();

            configuration.SetCountriesFromApi(apiDataPeferably);
            configuration.SetBaseCurency(string.IsNullOrEmpty(baseCurrency) ? DEFAULT_BASE_CURRENCY : baseCurrency);
            configuration.SetConfiguredCurrencies(configuredCurrencies);

            return configuration;
        }

        private static void RegisterServicesSingleton(IServiceCollection services, ConfigurationBase configurationBase)
        {
            services.AddSingleton(configurationBase);

            services.AddSingleton(StaticDataServiceFactory.GetCityService(configurationBase));
            services.AddSingleton(StaticDataServiceFactory.GetCountryService(configurationBase));
            services.AddSingleton(StaticDataServiceFactory.GetCurrencyService(configurationBase));
            services.AddSingleton(StaticDataServiceFactory.GetExchangeRateService(configurationBase));
        }

        #endregion

    }
}

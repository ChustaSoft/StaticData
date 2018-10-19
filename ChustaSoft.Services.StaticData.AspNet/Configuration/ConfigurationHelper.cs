using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ChustaSoft.Services.StaticData.AspNet.Configuration
{
    public static class ConfigurationHelper
    {

        #region Fields

        private const string STATICDATA_CONFIGURATION_CONFIG_PARAM = "StaticDataConfiguration";

        #endregion


        #region Public methods

        public static void RegisterStaticDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(STATICDATA_CONFIGURATION_CONFIG_PARAM).Get<StaticDataConfiguration>();
            var configurationBase = GetConfiguration(configurationSection);

            services.AddSingleton(configurationBase);

            RegisterRepositories(services, configurationSection.ApiDataPeferably);
            RegisterServices(services);
        }

        #endregion


        #region Private methods

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICurrencyService, CurrencyService>();
            services.AddTransient<IExchangeRateService, ExchangeRateService>();
        }

        private static void RegisterRepositories(IServiceCollection services, bool apiDataPeferably)
        {
            if (apiDataPeferably)
                services.AddTransient<ICountryRepository, CountryExternalService>();
            else
                services.AddTransient<ICountryRepository, CountryLocalRepository>();

            services.AddTransient<ICityRepository, CityLocalRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyExternalService>();
            services.AddTransient<IExchangeRateSingleRepository, ExchangeRateSingleExternalService>();
            services.AddTransient<IExchangeRateMultipleRepository, ExchangeRateMultipleExternalService>();
        }

        private static ConfigurationBase GetConfiguration(StaticDataConfiguration staticDataConfiguration)
        {
            var configuration = new ConfigurationBase();

            configuration.SetCountriesFromApi(staticDataConfiguration.ApiDataPeferably);
            configuration.SetBaseCurency(staticDataConfiguration.BaseCurrency);
            configuration.SetConfiguredCurrencies(staticDataConfiguration.ConfiguredCurrencies);

            return configuration;
        }

        #endregion

    }
}

using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChustaSoft.Services.StaticData.Configuration
{
    public static class ConfigurationHelper
    {

        #region Fields

        private const string STATICDATA_CONFIGURATION_CONFIG_PARAM = "StaticDataConfiguration";

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
            var internalConfiguration = GetConfiguration(configurationSection);

            RegisterServicesSingleton(services, internalConfiguration);
        }


        /// <summary>
        /// Extension method availaible for configuring StaticData services
        /// Could be used for manual configuring inside Startup
        /// </summary>
        /// <param name="services">DI container</param>
        /// <param name="configurationBuilder">Builder with configured properties</param>
        public static void RegisterStaticDataServices(this IServiceCollection services, IStaticDataConfigurationBuilder configurationBuilder)
        {
            var configurationBase = ConstructConfiguration(configurationBuilder);

            RegisterServicesSingleton(services, configurationBase);
        }

        #endregion


        #region Private methods

        private static InternalConfiguration GetConfiguration(StaticDataConfiguration staticDataConfiguration)
        {
            if (staticDataConfiguration != null)
                return new InternalConfiguration(staticDataConfiguration);
            else
                return new InternalConfiguration(StaticDataConfigurationBuilder.Configure());
        }

        private static InternalConfiguration ConstructConfiguration(IStaticDataConfigurationBuilder configurationBuilder)
        {
            var configuration = new InternalConfiguration(configurationBuilder);

            return configuration;
        }

        private static void RegisterServicesSingleton(IServiceCollection services, InternalConfiguration configurationBase)
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

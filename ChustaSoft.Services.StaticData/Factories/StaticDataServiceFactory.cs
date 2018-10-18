using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;


namespace ChustaSoft.Services.StaticData.Factories
{


    public class StaticDataServiceFactory
    {

        #region Public methods

        public static IExchangeRateService GetExchangeRateService(ConfigurationBase configuration)
        {
            var exchangeRateMultipleRepository = new ExchangeRateMultipleExternalService(configuration);
            var exchangeRateSingleRepository = new ExchangeRateSingleExternalService(configuration);

            return new ExchangeRateService(configuration, exchangeRateSingleRepository, exchangeRateMultipleRepository);
        }

        public static ICurrencyService GetCurrencyService(ConfigurationBase configuration)
        {
            var currencyRepository = new CurrencyExternalService(configuration);

            return new CurrencyService(currencyRepository);
        }

        public static ICountryService GetCountryService(ConfigurationBase configuration)
        {
            var countryRepository = GetConfiguredRepository(configuration);

            return new CountryService(countryRepository);
        }

        public static ICityService GetCityService(ConfigurationBase configuration)
        {
            var cityRepository = new CityLocalRepository();

            return new CityService(cityRepository);
        }

        #endregion


        #region Private methods

        private static ICountryRepository GetConfiguredRepository(ConfigurationBase configuration)
        {
            if (configuration.CountriesFromApi)
                return new CountryExternalService(configuration);
            else
                return new CountryLocalRepository();
        }

        #endregion

    }
}

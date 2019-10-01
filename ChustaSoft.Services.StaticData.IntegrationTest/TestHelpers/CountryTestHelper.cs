using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Helpers;
using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;
using System;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestHelpers
{
    internal static class CountryTestHelper
    {
        
        #region Fields

        private const string CountriesApiUrl = "http://countryapi.gear.host/v1/Country/getCountries";

        #endregion


        #region Internal methods

        internal static ICountryRepository CreateMockRepository(Type type)
        {
            switch (type.Name)
            {
                case nameof(CountryExternalService):
                    return GetCountryExternalService();
                case nameof(CountryLocalRepository):
                default:
                    return GetCountryLocalRepository();
            }
        }

        internal static ICountryService CreateMockService(Type type)
        {
            var repository = CreateMockRepository(type);

            return new CountryService(repository);
        }

        #endregion
        
        
        #region Private methods

        private static ICountryRepository GetCountryLocalRepository()
        {
            return new CountryLocalRepository();
        }

        private static ICountryRepository GetCountryExternalService()
        {
            var mockedConfiguration = new InternalConfiguration(StaticDataConfigurationBuilder.Configure().SetApiPreferably(false));

            return new CountryExternalService(mockedConfiguration);
        }

        #endregion

    }
}

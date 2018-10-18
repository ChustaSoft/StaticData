using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    internal class CountryLocalRepository : LocalRepositoryBase, ICountryRepository
    {
        
        #region Fields

        private static string COUNTRIES_FILE_NAME = @"countries";

        #endregion


        #region Public methods

        public async Task<Country> Get(string countryName)
        {
            var allCountries = GetParsedCollection<CountryLocal>(COUNTRIES_FILE_NAME);

            return allCountries.First(x => x.Name == countryName);
        }

        public async Task<Country> Get(AlphaCodeType alphaType, string alphaCode)
        {
            var allCountries = GetParsedCollection<CountryLocal>(COUNTRIES_FILE_NAME);

            switch (alphaType)
            {
                case AlphaCodeType.Alpha2:
                    return allCountries.First(x => x.Alpha2Code == alphaCode);

                case AlphaCodeType.Alpha3:
                    return allCountries.First(x => x.Alpha3Code == alphaCode);

                default:
                    throw new InvalidEnumArgumentException(nameof(alphaType), (int)alphaType, typeof(AlphaCodeType));
            }
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return GetParsedCollection<CountryLocal>(COUNTRIES_FILE_NAME);
        }

        #endregion

    }
}

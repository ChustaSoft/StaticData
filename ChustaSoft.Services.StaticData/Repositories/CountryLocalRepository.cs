using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public class CountryLocalRepository : LocalRepositoryBase, ICountryRepository
    {
        
        #region Fields

        private static string COUNTRIES_JSON_REPOSITORY_FILE_PATH = @"Data\countries.json";

        #endregion


        public async Task<Country> Get(string countryName)
        {
            var allCountries = GetAllFileData<CountryLocal>(COUNTRIES_JSON_REPOSITORY_FILE_PATH);

            return allCountries.First(x => x.Name == countryName);
        }

        public async Task<Country> Get(AlphaCodeType alphaType, string alphaCode)
        {
            var allCountries = GetAllFileData<CountryLocal>(COUNTRIES_JSON_REPOSITORY_FILE_PATH);

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
            return GetAllFileData<CountryLocal>(COUNTRIES_JSON_REPOSITORY_FILE_PATH);
        }

    }
}

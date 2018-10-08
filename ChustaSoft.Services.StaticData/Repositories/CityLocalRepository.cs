using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.IO;

namespace ChustaSoft.Services.StaticData.Repositories
{
    public class CityLocalRepository : LocalRepositoryBase, ICityRepository
    {

        #region Fields

        private static string COUNTRY_JSON_REPOSITORY_FILE_PATH = @"Data\{0}.json";

        #endregion


        #region Public methods

        public IEnumerable<City> Get(string country)
        {
            try
            {
                var countryCities = GetParsedData<CountryLocalParsed>(string.Format(COUNTRY_JSON_REPOSITORY_FILE_PATH, country));

                return countryCities.Cities;
            }
            catch (FileNotFoundException ex)
            {
                throw new CountryNotFoundException(country, ex);
            }
        }

        #endregion

    }
}

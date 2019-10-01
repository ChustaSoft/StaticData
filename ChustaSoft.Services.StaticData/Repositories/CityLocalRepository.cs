using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.IO;

namespace ChustaSoft.Services.StaticData.Repositories
{
    internal class CityLocalRepository : LocalRepositoryBase, ICityRepository
    {

        #region Public methods

        public IEnumerable<City> Get(string country)
        {
            try
            {
                var countryCities = GetParsedElement<CountryLocalParsed>(country);

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

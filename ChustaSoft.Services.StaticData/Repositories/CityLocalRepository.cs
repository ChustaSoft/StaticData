using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Repositories
{
    internal class CityLocalRepository : LocalRepositoryBase, ICityRepository
    {

        #region Public methods

        public IEnumerable<City> Get(string country)
        {
            var countryCities = GetParsedElement<CountryLocalParsed>(country);

            return countryCities.Cities;
        }

        #endregion

    }
}

using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public class CityLocalRepository : LocalRepositoryBase, ICityRepository
    {

        #region Public methods

        public IEnumerable<City> Get(string country)
        {
            try
            {
                var countryCities = GetParsedElement<CountryLocalParsed>(country);

                return countryCities.Cities;
            }
            catch (Exception ex)
            {
                throw new CountryNotFoundException(country, ex);
            }
        }

        #endregion

    }
}

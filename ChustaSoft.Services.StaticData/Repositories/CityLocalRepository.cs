using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ChustaSoft.Services.StaticData.Repositories
{
    internal class CityLocalRepository : LocalRepositoryBase, ICityRepository
    {

        #region Public methods

        public async Task<IEnumerable<City>> GetAsync(string country)
        {
            try
            {
                return await Task.Run(() => 
                {
                    var countryCities = GetParsedElement<CountryLocalParsed>(country);

                    return countryCities.Cities;
                });
            }
            catch (FileNotFoundException ex) 
            {
                throw new CountryNotFoundException(country, ex);
            }
        }

        #endregion

    }
}

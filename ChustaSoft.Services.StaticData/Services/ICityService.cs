using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    /// <summary>
    /// StaticData City Service
    /// </summary>
    public interface ICityService
    {

        /// <summary>
        /// Gets cities of a country
        /// </summary>
        /// <param name="country">Requested country</param>
        /// <returns>Retrived cities</returns>
        IEnumerable<City> Get(string country);

        /// <summary>
        /// Get cities from the requested countries
        /// </summary>
        /// <param name="countries">Requested countries</param>
        /// <returns>Retrived cities result for each country</returns>
        IDictionary<string, (bool Found, IEnumerable<City> Cities)> Get(IEnumerable<string> countries);

    }
}

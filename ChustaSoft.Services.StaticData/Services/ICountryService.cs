using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    /// <summary>
    /// StaticData Country Service
    /// </summary>
    public interface ICountryService
    {

        /// <summary>
        /// Gets all countries
        /// </summary>
        /// <returns>ActionResponse with countries retrived</returns>
        ActionResponse<IEnumerable<Country>> GetAll();

        /// <summary>
        /// Get Country Information by it's name
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>ActionResponse with retrived country</returns>
        ActionResponse<Country> Get(string countryName);

        /// <summary>
        /// Get Country by Alpha Code
        /// </summary>
        /// <param name="alphaType">Alpha Code type for filtering</param>
        /// <param name="alphaCode">Aplha code itself</param>
        /// <returns>ActionResponse with retrived country</returns>
        ActionResponse<Country> Get(AlphaCodeType alphaType, string alphaCode);

    }
}

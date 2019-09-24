using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    /// <summary>
    /// StaticData City Service
    /// </summary>
    public interface ICityService
    {

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Gets cities of a country
        /// </summary>
        /// <param name="country">Requested country</param>
        /// <returns>ActionResponse with retrived cities</returns>
        ActionResponse<IEnumerable<City>> Get(string country);

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Get cities from the requested countries
        /// </summary>
        /// <param name="countries">Requested countries</param>
        /// <returns>ActionResponse with retrived cities</returns>
        ActionResponse<IEnumerable<City>> Get(List<string> countries);

    }
}

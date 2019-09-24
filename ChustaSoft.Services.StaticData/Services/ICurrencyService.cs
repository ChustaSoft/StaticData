using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    /// <summary>
    /// StaticData Currency Service
    /// </summary>
    public interface ICurrencyService
    {

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Gets all currencies
        /// </summary>
        /// <returns>ActionResponse with retrived currencies</returns>
        ActionResponse<IEnumerable<Currency>> GetAll();

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Get a currency info by it's code
        /// </summary>
        /// <param name="currencySymbol"></param>
        /// <returns>ActionResponse with currency info</returns>
        ActionResponse<Currency> Get(string currencySymbol);

    }
}

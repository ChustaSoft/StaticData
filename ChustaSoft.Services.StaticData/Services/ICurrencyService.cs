using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    /// <summary>
    /// StaticData Currency Service
    /// </summary>
    public interface ICurrencyService
    {

        /// <summary>
        /// Gets all currencies
        /// </summary>
        /// <returns>ActionResponse with retrived currencies</returns>
        ActionResponse<IEnumerable<Currency>> GetAll();

        /// <summary>
        /// Get a currency info by it's code
        /// </summary>
        /// <param name="currencySymbol"></param>
        /// <returns>ActionResponse with currency info</returns>
        ActionResponse<Currency> Get(string currencySymbol);

    }
}

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
        /// <returns>Retrived currencies</returns>
        IEnumerable<Currency> GetAll();

        /// <summary>
        /// Get a currency info by it's code
        /// </summary>
        /// <param name="currencySymbol"></param>
        /// <returns>Retrived currency info</returns>
        Currency Get(string currencySymbol);

    }
}

using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<IEnumerable<Currency>> GetAllAsync();

        /// <summary>
        /// Get a currency info by it's code
        /// </summary>
        /// <param name="currencySymbol"></param>
        /// <returns>Retrived currency info</returns>
        Currency Get(string currencySymbol);
        Task<Currency> GetAsync(string currencySymbol);

    }
}

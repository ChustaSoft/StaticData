using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <returns>Countries retrived</returns>
        IEnumerable<Country> GetAll();
        Task<IEnumerable<Country>> GetAllAsync();

        /// <summary>
        /// Get Country Information by it's name
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>Retrived country</returns>
        Country Get(string countryName);
        Task<Country> GetAsync(string countryName);

        /// <summary>
        /// Get Country by Alpha Code
        /// </summary>
        /// <param name="alphaType">Alpha Code type for filtering</param>
        /// <param name="alphaCode">Aplha code itself</param>
        /// <returns>Retrived country</returns>
        Country Get(AlphaCodeType alphaType, string alphaCode);
        Task<Country> GetAsync(AlphaCodeType alphaType, string alphaCode);

    }
}

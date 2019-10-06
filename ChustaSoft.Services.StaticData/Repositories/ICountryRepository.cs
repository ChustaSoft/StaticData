using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    internal interface ICountryRepository
    {

        Task<IEnumerable<Country>> GetAllAsync();

        Task<Country> GetAsync(string countryName);

        Task<Country> GetAsync(AlphaCodeType alphaType, string alphaCode);

    }
}

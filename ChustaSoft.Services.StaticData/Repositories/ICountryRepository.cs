using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public interface ICountryRepository
    {

        Task<IEnumerable<Country>> GetAll();

        Task<Country> Get(string countryName);

        Task<Country> Get(AlphaCodeType alphaType, string alphaCode);

    }
}

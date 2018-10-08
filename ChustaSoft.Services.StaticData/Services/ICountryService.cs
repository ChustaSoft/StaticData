using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    public interface ICountryService
    {

        ActionResponse<IEnumerable<Country>> GetAll();

        ActionResponse<Country> Get(string countryName);

        ActionResponse<Country> Get(AlphaCodeType alphaType, string alphaCode);

    }
}

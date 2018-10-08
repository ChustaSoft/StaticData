using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    public interface ICityService
    {

        ActionResponse<IEnumerable<City>> Get(string country);

        ActionResponse<IEnumerable<City>> Get(List<string> countries);

    }
}

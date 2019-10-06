using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChustaSoft.Services.StaticData.Repositories
{
    internal interface ICityRepository
    {

        Task<IEnumerable<City>> GetAsync(string country);

    }
}

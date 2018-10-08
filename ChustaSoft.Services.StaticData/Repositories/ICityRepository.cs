using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public interface ICityRepository
    {

        IEnumerable<City> Get(string country);

    }
}

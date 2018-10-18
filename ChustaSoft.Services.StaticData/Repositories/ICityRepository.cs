using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Repositories
{
    internal interface ICityRepository
    {

        IEnumerable<City> Get(string country);

    }
}

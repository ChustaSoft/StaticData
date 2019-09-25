using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChustaSoft.Services.StaticData.Services
{
    internal class CityService : ICityService
    {
        
        #region Fields

        private ICityRepository _cityRepository;

        #endregion


        #region Constructor

        internal CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        #endregion


        #region Public methods

        public IEnumerable<City> Get(string country)
        {
            return _cityRepository.Get(country);
        }

        public IDictionary<string, (bool Found, IEnumerable<City> Cities)> Get(IEnumerable<string> countries)
        {
            var citiesResult = new Dictionary<string, (bool, IEnumerable<City>)>();

            foreach (var country in countries)
            {
                try
                {
                    citiesResult.Add(country, (true, _cityRepository.Get(country)));
                }
                catch (FileNotFoundException)
                {
                    citiesResult.Add(country, (false, Enumerable.Empty<City>()));
                }
            }

            return citiesResult;
        }

        #endregion

    }
}

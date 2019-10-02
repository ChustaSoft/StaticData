using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            try
            {
                return _cityRepository.Get(country);
            }
            catch (FileNotFoundException ex)
            {
                throw new CountryNotFoundException(country, ex);
            }
        }

        public Task<IEnumerable<City>> GetAsync(string country)
        {
            var task = new Task<IEnumerable<City>>(() => Get(country));
            task.Start();

            return task;
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
                catch (CountryNotFoundException)
                {
                    citiesResult.Add(country, (false, Enumerable.Empty<City>()));
                }
            }

            return citiesResult;
        }

        public Task<IDictionary<string, (bool Found, IEnumerable<City> Cities)>> GetAsync(IEnumerable<string> countries)
        {
            var task = new Task<IDictionary<string, (bool Found, IEnumerable<City> Cities)>>(() => Get(countries));
            task.Start();

            return task;
        }

        #endregion

    }
}

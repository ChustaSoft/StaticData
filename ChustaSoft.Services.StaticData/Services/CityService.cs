using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System;
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
                return GetAsync(country).Result;
            }
            catch (AggregateException ie) when (ie.InnerException is FileNotFoundException fnfe)
            {
                throw new CountryNotFoundException(country, fnfe);
            }
        }

        public async Task<IEnumerable<City>> GetAsync(string country)
        {
            return await _cityRepository.GetAsync(country);
        }

        public IDictionary<string, (bool Found, IEnumerable<City> Cities)> Get(IEnumerable<string> countries)
        {
            var citiesResult = new Dictionary<string, (bool, IEnumerable<City>)>();

            foreach (var country in countries)
            {
                try
                {
                    citiesResult.Add(country, (true, _cityRepository.GetAsync(country).Result));
                }
                catch (AggregateException ie) when (ie.InnerException is CountryNotFoundException cie)
                {
                    citiesResult.Add(country, (false, Enumerable.Empty<City>()));
                }
            }

            return citiesResult;
        }

        public async Task<IDictionary<string, (bool Found, IEnumerable<City> Cities)>> GetAsync(IEnumerable<string> countries)
        {
            var task = new Task<IDictionary<string, (bool Found, IEnumerable<City> Cities)>>(() => Get(countries));
            task.Start();

            return await task;
        }

        #endregion

    }
}

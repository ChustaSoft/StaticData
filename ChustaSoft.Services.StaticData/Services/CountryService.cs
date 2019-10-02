using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChustaSoft.Services.StaticData.Services
{
    internal class CountryService : ICountryService
    {

        #region Fields

        private readonly ICountryRepository _countryRepository;

        #endregion


        #region Constructor

        internal CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        #endregion


        #region Public methods

        public IEnumerable<Country> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _countryRepository.GetAll();
        }

        public Country Get(string countryName)
        {
            return GetAsync(countryName).Result;
        }

        public async Task<Country> GetAsync(string countryName)
        {
            return await _countryRepository.Get(countryName);
        }

        public Country Get(AlphaCodeType alphaType, string alphaCode)
        {
            return GetAsync(alphaType, alphaCode).Result;
        }

        public async Task<Country> GetAsync(AlphaCodeType alphaType, string alphaCode)
        {
            return await _countryRepository.Get(alphaType, alphaCode);
        }
        #endregion

    }
}

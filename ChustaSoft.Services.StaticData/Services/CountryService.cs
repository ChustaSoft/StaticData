using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System.Collections.Generic;


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

        public Country Get(string countryName)
        {
            return _countryRepository.Get(countryName).Result;
        }

        public Country Get(AlphaCodeType alphaType, string alphaCode)
        {
            return _countryRepository.Get(alphaType, alphaCode).Result;
        }

        public IEnumerable<Country> GetAll()
        {
            return _countryRepository.GetAll().Result;
        }

        #endregion

    }
}

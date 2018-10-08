using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    public class CountryService : ICountryService
    {

        #region Fields

        private readonly ICountryRepository _countryRepository;

        #endregion

        
        #region Constructor

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        #endregion


        #region Public methods

        public ActionResponse<Country> Get(string countryName)
        {
            try
            {
                var country = _countryRepository.Get(countryName).Result;

                return new ActionResponse<Country>(country);
            }
            catch (System.Exception ex)
            {
                return new ActionResponse<Country>(new List<ErrorMessage> { new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message) });
            }
        }

        public ActionResponse<Country> Get(AlphaCodeType alphaType, string alphaCode)
        {
            try
            {
                var country = _countryRepository.Get(alphaType, alphaCode).Result;

                return new ActionResponse<Country>(country);
            }
            catch (System.Exception ex)
            {
                return new ActionResponse<Country>(new List<ErrorMessage> { new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message) });
            }
        }

        public ActionResponse<IEnumerable<Country>> GetAll()
        {
            try
            {
                var countries = _countryRepository.GetAll().Result;

                return new ActionResponse<IEnumerable<Country>>(countries);
            }
            catch (System.Exception ex)
            {
                return new ActionResponse<IEnumerable<Country>>(new List<ErrorMessage> { new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message) });
            }
        }

        #endregion

    }
}

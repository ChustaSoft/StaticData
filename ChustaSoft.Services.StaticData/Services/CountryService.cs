using ChustaSoft.Common.Helpers;
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
            var arBuilder = new ActionResponseBuilder<Country>();
            try
            {
                var country = _countryRepository.Get(countryName).Result;

                arBuilder.AddData(country);
            }
            catch (System.Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<Country> Get(AlphaCodeType alphaType, string alphaCode)
        {
            var arBuilder = new ActionResponseBuilder<Country>();
            try
            {
                var country = _countryRepository.Get(alphaType, alphaCode).Result;

                arBuilder.AddData(country);
            }
            catch (System.Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<IEnumerable<Country>> GetAll()
        {
            var arBuilder = new ActionResponseBuilder<IEnumerable<Country>>();
            try
            {
                var countries = _countryRepository.GetAll().Result;

                arBuilder.AddData(countries);
            }
            catch (System.Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        #endregion

    }
}

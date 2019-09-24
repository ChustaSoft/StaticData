using ChustaSoft.Common.Helpers;
using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System;
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

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        public ActionResponse<Country> Get(string countryName)
        {
            var arBuilder = new ActionResponseBuilder<Country>();
            try
            {
                var country = _countryRepository.Get(countryName).Result;

                arBuilder.SetData(country);
            }
            catch (System.Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        public ActionResponse<Country> Get(AlphaCodeType alphaType, string alphaCode)
        {
            var arBuilder = new ActionResponseBuilder<Country>();
            try
            {
                var country = _countryRepository.Get(alphaType, alphaCode).Result;

                arBuilder.SetData(country);
            }
            catch (System.Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        public ActionResponse<IEnumerable<Country>> GetAll()
        {
            var arBuilder = new ActionResponseBuilder<IEnumerable<Country>>();
            try
            {
                var countries = _countryRepository.GetAll().Result;

                arBuilder.SetData(countries);
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

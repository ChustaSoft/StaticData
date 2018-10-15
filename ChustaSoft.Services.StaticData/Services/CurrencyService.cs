using System.Collections.Generic;
using ChustaSoft.Common.Helpers;
using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;


namespace ChustaSoft.Services.StaticData.Services
{
    public class CurrencyService : ICurrencyService
    {
        
        #region Fields

        private ICurrencyRepository _currencyRepository;

        #endregion


        #region Constructor

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        #endregion


        #region Public methods

        public ActionResponse<Currency> Get(string currencySymbol)
        {
            var arBuilder = new ActionResponseBuilder<Currency>();
            try
            {
                var currency = _currencyRepository.Get(currencySymbol).Result;

                arBuilder.SetData(currency);
            }
            catch (System.Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<IEnumerable<Currency>> GetAll()
        {
            var arBuilder = new ActionResponseBuilder<IEnumerable<Currency>>();
            try
            {
                var currencies = _currencyRepository.GetAll().Result;

                arBuilder.SetData(currencies);
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

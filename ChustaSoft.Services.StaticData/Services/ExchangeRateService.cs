using System;
using System.Collections.Generic;
using System.Linq;
using ChustaSoft.Common.Helpers;
using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;


namespace ChustaSoft.Services.StaticData.Services
{
    public class ExchangeRateService : IExchangeRateService
    {

        #region Fields

        private readonly ConfigurationBase _configurationBase;

        private readonly IExchangeRateSingleRepository _exchangeRateSingleRepository;
        private readonly IExchangeRateMultipleRepository _exchangeRateMultipleRepository;

        #endregion


        #region Constructor

        public ExchangeRateService(ConfigurationBase configurationBase, IExchangeRateSingleRepository exchangeRateSingleRepository, IExchangeRateMultipleRepository exchangeRateMultipleRepository)
        {
            _configurationBase = configurationBase;

            _exchangeRateSingleRepository = exchangeRateSingleRepository;
            _exchangeRateMultipleRepository = exchangeRateMultipleRepository;
        }

        #endregion


        #region Public methods

        public ActionResponse<ExchangeRate> Get(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            var arBuilder = new ActionResponseBuilder<ExchangeRate>();
            try
            {
                var exchangeRate = _exchangeRateSingleRepository.Get(currencyFrom, currencyTo, date).Result;

                arBuilder.AddData(exchangeRate);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<IEnumerable<ExchangeRate>> GetBidirectional(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            var arBuilder = new ActionResponseBuilder<IEnumerable<ExchangeRate>>();
            try
            {
                var exchangeRate = _exchangeRateSingleRepository.GetBidirectional(currencyFrom, currencyTo, date).Result;

                arBuilder.AddData(exchangeRate);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<IEnumerable<ExchangeRate>> GetHistorical(string currency, DateTime beginDate, DateTime endDate)
        {
            var arBuilder = new ActionResponseBuilder<IEnumerable<ExchangeRate>>();
            try
            {
                var exchangeRate = _exchangeRateMultipleRepository.GetHistorical(currency, beginDate, endDate).Result;

                arBuilder.AddData(exchangeRate);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<IEnumerable<ExchangeRate>> GetLatest(string currency)
        {
            var arBuilder = new ActionResponseBuilder<IEnumerable<ExchangeRate>>();
            try
            {
                var exchangeRate = _exchangeRateMultipleRepository.GetLatest(currency).Result;

                arBuilder.AddData(exchangeRate);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<IEnumerable<ExchangeRate>> GetConfiguredLatest()
        {
            var arBuilder = new ActionResponseBuilder<IEnumerable<ExchangeRate>>();
            try
            {
                var exchangeRates = GetAllNeededExchangeRates();

                arBuilder.AddData(exchangeRates);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<IEnumerable<ExchangeRate>> GetConfiguredHistorical(DateTime beginDate, DateTime endDate)
        {
             var arBuilder = new ActionResponseBuilder<IEnumerable<ExchangeRate>>();
            try
            {
                var exchangeRates = GetAllNeededExchangeRates(beginDate, endDate);

                arBuilder.AddData(exchangeRates);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        #endregion


        #region Private methods

        private List<ExchangeRate> GetAllNeededExchangeRates()
        {
            var exchangeRates = new List<ExchangeRate>(_exchangeRateMultipleRepository.GetLatest(_configurationBase.ConfiguredBaseCurrency).Result);

            GetConfiguredExchangeRates(exchangeRates);

            return exchangeRates;
        }

        private List<ExchangeRate> GetAllNeededExchangeRates(DateTime beginDate, DateTime endDate)
        {
            var exchangeRates = new List<ExchangeRate>(_exchangeRateMultipleRepository.GetHistorical(_configurationBase.ConfiguredBaseCurrency, beginDate, endDate).Result);

            GetConfiguredExchangeRates(exchangeRates, beginDate, endDate);

            return exchangeRates;
        }

        private void GetConfiguredExchangeRates(List<ExchangeRate> exchangeRates)
        {
            foreach (var configCurrency in _configurationBase.ConfiguredCurrencies)
            {
                if (!exchangeRates.Any(er => er.From == configCurrency))
                {
                    var currencyLatestExchangeRate = _exchangeRateSingleRepository.Get(configCurrency, _configurationBase.ConfiguredBaseCurrency).Result;

                    exchangeRates.Add(currencyLatestExchangeRate);
                }
            }
        }

        private void GetConfiguredExchangeRates(List<ExchangeRate> exchangeRates, DateTime beginDate, DateTime endDate)
        {
            foreach (var configCurrency in _configurationBase.ConfiguredCurrencies)
            {
                if (!exchangeRates.Any(er => er.From == configCurrency))
                {
                    var currencyLatestExchangeRate = _exchangeRateSingleRepository.GetHistorical(configCurrency, _configurationBase.ConfiguredBaseCurrency, beginDate, endDate).Result;

                    exchangeRates.AddRange(currencyLatestExchangeRate);
                }
            }
        }

        #endregion

    }
}

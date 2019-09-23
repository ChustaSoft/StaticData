using ChustaSoft.Common.Builders;
using ChustaSoft.Common.Helpers;
using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


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

        internal ExchangeRateService(ConfigurationBase configurationBase, IExchangeRateSingleRepository exchangeRateSingleRepository, IExchangeRateMultipleRepository exchangeRateMultipleRepository)
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

                arBuilder.SetData(exchangeRate);
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

                arBuilder.SetData(exchangeRate);
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

                arBuilder.SetData(exchangeRate);
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

                arBuilder.SetData(exchangeRate);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<ICollection<ExchangeRate>> GetConfiguredLatest()
        {
            var arBuilder = new ActionResponseBuilder<ICollection<ExchangeRate>>();
            try
            {
                GetAllNeededExchangeRates(arBuilder);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        public ActionResponse<ICollection<ExchangeRate>> GetConfiguredHistorical(DateTime beginDate, DateTime endDate)
        {
            var arBuilder = new ActionResponseBuilder<ICollection<ExchangeRate>>();
            try
            {
                GetAllNeededExchangeRates(arBuilder, beginDate, endDate);
            }
            catch (Exception ex)
            {
                arBuilder.AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message));
            }
            return arBuilder.Build();
        }

        #endregion


        #region Private methods

        private void GetAllNeededExchangeRates(ActionResponseBuilder<ICollection<ExchangeRate>> arBuilder)
        {
            var exchangeRates = _exchangeRateMultipleRepository.GetLatest(_configurationBase.ConfiguredBaseCurrency)
                .Result.ToList();

            arBuilder.SetData(exchangeRates);

            GetConfiguredExchangeRates(arBuilder, exchangeRates);
        }

        private void GetAllNeededExchangeRates(ActionResponseBuilder<ICollection<ExchangeRate>> arBuilder, DateTime beginDate, DateTime endDate)
        {
            var exchangeRates = _exchangeRateMultipleRepository.GetHistorical(_configurationBase.ConfiguredBaseCurrency, beginDate, endDate)
                .Result.ToList();

            arBuilder.SetData(exchangeRates);

            GetConfiguredExchangeRates(arBuilder, exchangeRates, beginDate, endDate);
        }

        private void GetConfiguredExchangeRates(ActionResponseBuilder<ICollection<ExchangeRate>> arBuilder, List<ExchangeRate> exchangeRates)
        {
            foreach (var configCurrency in _configurationBase.ConfiguredCurrencies)
            {
                if (!exchangeRates.Any(er => er.From == configCurrency))
                {
                    try
                    {
                        var currencyLatestExchangeRate = _exchangeRateSingleRepository.Get(configCurrency, _configurationBase.ConfiguredBaseCurrency).Result;

                        arBuilder.AddElement(currencyLatestExchangeRate);
                    }
                    catch (Exception ex)
                    {
                        arBuilder
                            .AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message))
                            .SetStatus(Common.Enums.ActionResponseType.Warning);
                    }
                }
            }
        }

        private void GetConfiguredExchangeRates(ActionResponseBuilder<ICollection<ExchangeRate>> arBuilder, List<ExchangeRate> exchangeRates, DateTime beginDate, DateTime endDate)
        {
            foreach (var configCurrency in _configurationBase.ConfiguredCurrencies)
            {
                if (!exchangeRates.Any(er => er.From == configCurrency))
                {
                    try
                    {
                        var currencyLatestExchangeRate = _exchangeRateSingleRepository.GetHistorical(configCurrency, _configurationBase.ConfiguredBaseCurrency, beginDate, endDate)
                            .Result.ToList();

                        arBuilder.AddRange(currencyLatestExchangeRate);
                    }
                    catch (Exception ex)
                    {
                        arBuilder
                            .AddError(new ErrorMessage(Common.Enums.ErrorType.Invalid, ex.Message))
                            .SetStatus(Common.Enums.ActionResponseType.Warning);
                    }
                }
            }
        }

        #endregion

    }
}

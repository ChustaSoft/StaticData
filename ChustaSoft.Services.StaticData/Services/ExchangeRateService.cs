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

        private readonly InternalConfiguration _configurationBase;

        private readonly IExchangeRateSingleRepository _exchangeRateSingleRepository;
        private readonly IExchangeRateMultipleRepository _exchangeRateMultipleRepository;

        #endregion


        #region Constructor

        internal ExchangeRateService(InternalConfiguration configurationBase, IExchangeRateSingleRepository exchangeRateSingleRepository, IExchangeRateMultipleRepository exchangeRateMultipleRepository)
        {
            _configurationBase = configurationBase;

            _exchangeRateSingleRepository = exchangeRateSingleRepository;
            _exchangeRateMultipleRepository = exchangeRateMultipleRepository;
        }

        #endregion


        #region Public methods

        public ExchangeRate Get(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            return _exchangeRateSingleRepository.Get(currencyFrom, currencyTo, date).Result;
        }

        public IEnumerable<ExchangeRate> GetBidirectional(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            return _exchangeRateSingleRepository.GetBidirectional(currencyFrom, currencyTo, date).Result;
        }

        public IEnumerable<ExchangeRate> GetHistorical(string currency, DateTime beginDate, DateTime endDate)
        {
            return _exchangeRateMultipleRepository.GetHistorical(currency, beginDate, endDate).Result;
        }

        public IEnumerable<ExchangeRate> GetLatest(string currency)
        {
            return _exchangeRateMultipleRepository.GetLatest(currency).Result;
        }

        public IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> GetConfiguredLatest()
        {
            var finalData = new Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>();
            var exchangeRates = _exchangeRateMultipleRepository.GetLatest(_configurationBase.ConfiguredBaseCurrency)
                .Result.ToList();

            foreach (var configCurrency in _configurationBase.ConfiguredCurrencies)
            {
                if (!exchangeRates.Any(er => er.From == configCurrency))
                {
                    try
                    {
                        var currencyLatestExchangeRate = _exchangeRateSingleRepository.Get(configCurrency, _configurationBase.ConfiguredBaseCurrency).Result;
                        finalData.Add(configCurrency, (true, new List<ExchangeRate>{ currencyLatestExchangeRate } ));
                    }
                    catch (Exception)
                    {
                        finalData.Add(configCurrency, (false, Enumerable.Empty<ExchangeRate>()));
                    }
                }
            }

            return finalData;
        }

        public IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> GetConfiguredHistorical(DateTime beginDate, DateTime endDate)
        {
            var finalData = new Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>();
            var exchangeRates = _exchangeRateMultipleRepository.GetHistorical(_configurationBase.ConfiguredBaseCurrency, beginDate, endDate)
                .Result.ToList();

            foreach (var configCurrency in _configurationBase.ConfiguredCurrencies)
            {
                if (!exchangeRates.Any(er => er.From == configCurrency))
                {
                    try
                    {
                        var currencyLatestExchangeRate = _exchangeRateSingleRepository.Get(configCurrency, _configurationBase.ConfiguredBaseCurrency).Result;
                        finalData.Add(configCurrency, (true, new List<ExchangeRate> { currencyLatestExchangeRate }));
                    }
                    catch (Exception)
                    {
                        finalData.Add(configCurrency, (false, Enumerable.Empty<ExchangeRate>()));
                    }
                }
            }

            return finalData;
        }

        #endregion


        #region Private methods



        #endregion

    }
}

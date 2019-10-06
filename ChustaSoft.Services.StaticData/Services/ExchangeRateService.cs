using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return GetAsync(currencyFrom, currencyTo, date).Result;
        }

        public Task<ExchangeRate> GetAsync(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            return _exchangeRateSingleRepository.GetAsync(currencyFrom, currencyTo, date);
        }

        public IEnumerable<ExchangeRate> GetLatest(string currency)
        {
            return GetLatestAsync(currency).Result;
        }

        public Task<IEnumerable<ExchangeRate>> GetLatestAsync(string currency)
        {
            return _exchangeRateMultipleRepository.GetLatestAsync(currency);
        }

        public IEnumerable<ExchangeRate> GetHistorical(string currency, DateTime beginDate, DateTime endDate)
        {
            return GetHistoricalAsync(currency, beginDate, endDate).Result;
        }

        public Task<IEnumerable<ExchangeRate>> GetHistoricalAsync(string currency, DateTime beginDate, DateTime endDate)
        {
            return _exchangeRateMultipleRepository.GetHistoricalAsync(currency, beginDate, endDate);
        }

        public IEnumerable<ExchangeRate> GetBidirectional(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            return GetBidirectionalAsync(currencyFrom, currencyTo, date).Result;
        }

        public Task<IEnumerable<ExchangeRate>> GetBidirectionalAsync(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            return _exchangeRateSingleRepository.GetBidirectionalAsync(currencyFrom, currencyTo, date);
        }

        public IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> GetConfiguredLatest()
        {
            var finalData = new Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>();
            var exchangeRates = _exchangeRateMultipleRepository.GetLatestAsync(_configurationBase.ConfiguredBaseCurrency)
                .Result.ToList();

            IterateResults(finalData, exchangeRates);

            return finalData;
        }

        public async Task<IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>> GetConfiguredLatestAsync()
        {
            var finalData = new Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>();
            var exchangeRates = await _exchangeRateMultipleRepository.GetLatestAsync(_configurationBase.ConfiguredBaseCurrency);

            IterateResults(finalData, exchangeRates);

            return finalData;
        }

        public IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> GetConfiguredHistorical(DateTime beginDate, DateTime endDate)
        {
            var finalData = new Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>();
            var exchangeRates = _exchangeRateMultipleRepository.GetHistoricalAsync(_configurationBase.ConfiguredBaseCurrency, beginDate, endDate)
                .Result.ToList();

            IterateResults(finalData, exchangeRates);

            return finalData;
        }

        public async Task<IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>> GetConfiguredHistoricalAsync(DateTime beginDate, DateTime endDate)
        {
            var finalData = new Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>();
            var exchangeRates = await _exchangeRateMultipleRepository.GetHistoricalAsync(_configurationBase.ConfiguredBaseCurrency, beginDate, endDate);

            IterateResults(finalData, exchangeRates);

            return finalData;
        }

        #endregion


        #region Private methods

        private void IterateResults(Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> finalData, IEnumerable<ExchangeRate> exchangeRates)
        {
            foreach (var configCurrency in _configurationBase.ConfiguredCurrencies)
            {
                if (!HasExchangeRate(exchangeRates, configCurrency))
                    TryGetFromSingleRepository(finalData, configCurrency);
                else
                    AddFromRetrivedExchangeRates(finalData, exchangeRates, configCurrency);
            }
        }

        private static bool HasExchangeRate(IEnumerable<ExchangeRate> exchangeRates, string configCurrency)
        {
            return exchangeRates.Any(er => er.From == configCurrency);
        }

        private static void AddFromRetrivedExchangeRates(Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> finalData, IEnumerable<ExchangeRate> exchangeRates, string configCurrency)
        {
            finalData.Add(configCurrency, (true, new List<ExchangeRate> { exchangeRates.First(er => er.From == configCurrency) }));
        }

        private void TryGetFromSingleRepository(Dictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> finalData, string configCurrency)
        {
            try
            {
                var currencyLatestExchangeRate = _exchangeRateSingleRepository.GetAsync(configCurrency, _configurationBase.ConfiguredBaseCurrency).Result;
                finalData.Add(configCurrency, (true, new List<ExchangeRate> { currencyLatestExchangeRate }));
            }
            catch (Exception)
            {
                finalData.Add(configCurrency, (false, Enumerable.Empty<ExchangeRate>()));
            }
        }

        #endregion

    }
}

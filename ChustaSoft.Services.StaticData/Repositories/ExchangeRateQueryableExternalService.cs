using ChustaSoft.Common.Helpers;
using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChustaSoft.Services.StaticData.Constants;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public class ExchangeRateQueryableExternalService : ExternalServiceBase, IExchangeRateQueryableRepository
    {
        
        #region Fields

        private const char PARAM_PREFIX = 'q';

        private const string DATE_PARAM_NAME = "date";

        #endregion


        #region Constructor

        public ExchangeRateQueryableExternalService(ConfigurationBase configuration) : base(configuration) { }

        #endregion


        #region Protected methods

        protected override UriBuilder GetBaseUri() => new UriBuilder(_configuration.ExchangeRatesQueryableApiUrl);

        #endregion


        #region Public methods

        public async Task<ExchangeRate> Get(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            var bidirectionalData = await GetBidirectionalData(currencyFrom, currencyTo, date);
            var requestedConversion = GetSingleConversion(currencyFrom, currencyTo);

            CheckRetrivedData(bidirectionalData.Response, requestedConversion);

            return bidirectionalData.Response[requestedConversion];
        }

        public async Task<IEnumerable<ExchangeRate>> GetBidirectional(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            var bidirectionalData = await GetBidirectionalData(currencyFrom, currencyTo, date);

            return bidirectionalData.Response.Values;
        }

        #endregion


        #region Private methods

        private string GetSingleConversion(string currencyFrom, string currencyTo) => currencyFrom + ExchangeRateConstants.SEPARATOR_CURRENCIES + currencyTo;

        private async Task<ExchangeRateApiResponse> GetBidirectionalData(string currencyFrom, string currencyTo, DateTime? date)
        {
            var json = await GetStringData(GetUri(currencyFrom, currencyTo, date));

            if(date == null)
                return JsonConvert.DeserializeObject<ExchangeRateWithoutDateApiResponse>(json);
            else
                return JsonConvert.DeserializeObject<ExchangeRateWithDateApiResponse>(json);
        }

        private void CheckRetrivedData(IDictionary<string, ExchangeRate> results, string requestedConversion)
        {
            if (results == null || !results.ContainsKey(requestedConversion))
                throw new CurrencyNotFoundException(requestedConversion);

            foreach (var er in results.Values)
            {
                er.Date = DateTime.Today;
            }
        }

        private Uri GetUri(string currencyFrom, string currencyTo, DateTime? date)
        {
            var fullConversion = GetSingleConversion(currencyFrom, currencyTo) + ExchangeRateConstants.SEPARATOR_CONVERSIONS + GetSingleConversion(currencyTo, currencyFrom);

            var uriBuilder = GetBaseUri().AddParameter(PARAM_PREFIX.ToString(), fullConversion);

            if (date != null)
                uriBuilder.AddParameter(DATE_PARAM_NAME, date.Value.ToString(ExchangeRateConstants.DATE_API_FORMAT));

            return uriBuilder.Uri;
        }

        #endregion

    }
}

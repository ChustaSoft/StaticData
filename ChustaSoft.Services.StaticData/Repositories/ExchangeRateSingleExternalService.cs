using ChustaSoft.Common.Helpers;
using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Constants;
using ChustaSoft.Services.StaticData.Exceptions;
using ChustaSoft.Services.StaticData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChustaSoft.Services.StaticData.Repositories
{
    internal class ExchangeRateSingleExternalService : ExternalServiceBase, IExchangeRateSingleRepository
    {

        #region Fields

        private const char PARAM_PREFIX = 'q';

        private const string DATE_PARAM_NAME = "date";
        private const string END_DATE_PARAM_NAME = "endDate";

        #endregion


        #region Constructor

        internal ExchangeRateSingleExternalService(InternalConfiguration configuration) : base(configuration) { }

        #endregion


        #region Protected methods

        protected override UriBuilder GetBaseUri() => new UriBuilder(_configuration.ExchangeRatesQueryableApiUrl);

        #endregion


        #region Public methods

        public async Task<ExchangeRate> GetAsync(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            var bidirectionalData = await GetBidirectionalData(currencyFrom, currencyTo, date);
            var requestedConversion = GetSingleConversion(currencyFrom, currencyTo);

            CheckRetrivedData(bidirectionalData.Response, requestedConversion);

            return bidirectionalData.Response[requestedConversion];
        }

        public async Task<IEnumerable<ExchangeRate>> GetBidirectionalAsync(string currencyFrom, string currencyTo, DateTime? date = null)
        {
            var bidirectionalData = await GetBidirectionalData(currencyFrom, currencyTo, date);

            return bidirectionalData.Response.Values;
        }

        public async Task<IEnumerable<ExchangeRate>> GetHistoricalAsync(string currencyFrom, string currencyTo, DateTime beginDate, DateTime endDate)
        {
            var json = await GetStringData(GetUri(currencyFrom, currencyTo, beginDate, endDate));

            var data = JsonConvert.DeserializeObject<ExchangeRateWithMultipleRateApiResponse>(json).Response;

            return data.Values;
        }

        #endregion


        #region Private methods

        private string GetSingleConversion(string currencyFrom, string currencyTo) => currencyFrom + ExchangeRateConstants.SEPARATOR_CURRENCIES + currencyTo;

        private async Task<ExchangeRateApiResponse> GetBidirectionalData(string currencyFrom, string currencyTo, DateTime? date)
        {
            var json = await GetStringData(GetUri(currencyFrom, currencyTo, date));

            if (date == null)
                return JsonConvert.DeserializeObject<ExchangeRateWithoutDateApiResponse>(json);
            else
                return JsonConvert.DeserializeObject<ExchangeRateWithDateApiResponse>(json);
        }

        private void CheckRetrivedData(IDictionary<string, ExchangeRate> results, string requestedConversion)
        {
            if (results == null || !results.ContainsKey(requestedConversion))
                throw new CurrencyNotFoundException(requestedConversion);
        }

        private Uri GetUri(string currencyFrom, string currencyTo, DateTime? date)
        {
            var fullConversion = GetSingleConversion(currencyFrom, currencyTo) + ExchangeRateConstants.SEPARATOR_CONVERSIONS + GetSingleConversion(currencyTo, currencyFrom);

            var uriBuilder = GetBaseUri().AddParameter(PARAM_PREFIX.ToString(), fullConversion);

            if (date != null)
                uriBuilder.AddParameter(DATE_PARAM_NAME, date.Value.ToString(ExchangeRateConstants.DATE_API_FORMAT));

            uriBuilder.AddParameter(AppConstants.FreeConverterApiKeyParam, _configuration.CurrencyConverterApiKey);

            return uriBuilder.Uri;
        }

        private Uri GetUri(string currencyFrom, string currencyTo, DateTime beginDate, DateTime endDate)
        {
            var fullConversion = GetSingleConversion(currencyFrom, currencyTo) + ExchangeRateConstants.SEPARATOR_CONVERSIONS + GetSingleConversion(currencyTo, currencyFrom);

            var uriBuilder = GetBaseUri()
                .AddParameter(PARAM_PREFIX.ToString(), fullConversion)
                .AddParameter(DATE_PARAM_NAME, beginDate.ToString(ExchangeRateConstants.DATE_API_FORMAT))
                .AddParameter(END_DATE_PARAM_NAME, endDate.ToString(ExchangeRateConstants.DATE_API_FORMAT))
                .AddParameter(AppConstants.FreeConverterApiKeyParam, _configuration.CurrencyConverterApiKey);

            return uriBuilder.Uri;
        }

        #endregion

    }
}

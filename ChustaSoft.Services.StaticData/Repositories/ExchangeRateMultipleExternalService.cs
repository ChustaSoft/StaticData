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
    internal class ExchangeRateMultipleExternalService : ExternalServiceBase, IExchangeRateMultipleRepository
    {

        #region Fields

        private const string LATEST_URIPART_NAME = "latest";
        private const string HISTORICAL_URIPART_NAME = "history";
        private const string CURRENCY_BASE_PARA_NAME = "base";
        private const string BEGINDATE_BASE_PARA_NAME = "start_at";
        private const string ENDDATE_BASE_PARA_NAME = "end_at";

        #endregion


        #region Constructor

        internal ExchangeRateMultipleExternalService(ConfigurationBase configuration) : base(configuration) { }

        #endregion


        #region Protected methods

        protected override UriBuilder GetBaseUri() => new UriBuilder(_configuration.ExchangeRatesApiUrl);

        #endregion


        #region Public methods

        public async Task<IEnumerable<ExchangeRate>> GetLatest(string currency)
        {
            string json = await GetStringData(GetUri(currency));

            var data = JsonConvert.DeserializeObject<ExchangeRateCollectionApiResponse>(json);

            CheckRetrivedData(data, currency);

            return data.Response.Values;
        }

        public async Task<IEnumerable<ExchangeRate>> GetHistorical(string currency, DateTime beginDate, DateTime endDate)
        {
            string json = await GetStringData(GetUri(currency, beginDate, endDate));

            var data = JsonConvert.DeserializeObject<ExchangeRateHistoricalApiResponse>(json);

            CheckRetrivedData(data, currency);

            return data.Response.Values;
        }

        #endregion


        #region Private methods

        private Uri GetUri(string currency)
        {
            return GetBaseUri()
                .AddPathPart(LATEST_URIPART_NAME)
                .AddParameter(CURRENCY_BASE_PARA_NAME, currency)
                .Uri;
        }

        private Uri GetUri(string currency, DateTime beginDate, DateTime endDate)
        {
            return GetBaseUri()
                .AddPathPart(HISTORICAL_URIPART_NAME)
                .AddParameter(BEGINDATE_BASE_PARA_NAME, beginDate.ToString(ExchangeRateConstants.DATE_API_FORMAT))
                .AddParameter(ENDDATE_BASE_PARA_NAME, endDate.ToString(ExchangeRateConstants.DATE_API_FORMAT))
                .AddParameter(CURRENCY_BASE_PARA_NAME, currency)
                .Uri;
        }

        private void CheckRetrivedData(ExchangeRateDataApiResponse data, string currency)
        {
            if (!string.IsNullOrEmpty(data.Error))
                throw new CurrencyNotFoundException(currency);
        }

        #endregion

    }
}

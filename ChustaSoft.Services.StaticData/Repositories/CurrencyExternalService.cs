using ChustaSoft.Common.Helpers;
using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    internal class CurrencyExternalService : ExternalServiceBase, ICurrencyRepository
    {
        #region Constants

        private const string APIKEY_PARAM = "apiKey";

        #endregion


        #region Constructor

        internal CurrencyExternalService(InternalConfiguration configuration) : base(configuration) { }

        #endregion


        #region Protected methods

        protected override UriBuilder GetBaseUri()
            => new UriBuilder(_configuration.CurrenciesApiUrl ).AddParameter(APIKEY_PARAM, _configuration.CurrencyConverterApiKey);

        #endregion


        #region Public methods

        public async Task<IEnumerable<Currency>> GetAll()
        {
            var currencies = await GetAllCurrencies();
            
            return currencies.Values.AsEnumerable<Currency>();
        }

        public async Task<Currency> Get(string currencySymbol)
        {
            var currencies = await GetAllCurrencies();

            return currencies.First(currency => currency.Key == currencySymbol).Value;
        }

        #endregion


        #region Private methods

        private async Task<IDictionary<string, CurrencyApi>> GetAllCurrencies()
        {
            string json = await GetStringData(GetBaseUri().Uri);

            var data = JsonConvert.DeserializeObject<CurrencyApiResponse>(json);

            return data.Results;
        }

        #endregion

    }
}

using ChustaSoft.Common.Helpers;
using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Constants;
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

        #region Constructor

        internal CurrencyExternalService(InternalConfiguration configuration) : base(configuration) { }

        #endregion


        #region Protected methods

        protected override UriBuilder GetBaseUri()
            => new UriBuilder(_configuration.CurrenciesApiUrl ).AddParameter(AppConstants.FreeConverterApiKeyParam, _configuration.CurrencyConverterApiKey);

        #endregion


        #region Public methods

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            var currencies = await GetAllCurrencies();
            
            return currencies.Values.AsEnumerable<Currency>();
        }

        public async Task<Currency> GetAsync(string currencySymbol)
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

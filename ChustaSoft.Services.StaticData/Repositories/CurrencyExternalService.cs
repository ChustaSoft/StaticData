using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public class CurrencyExternalService : ExternalServiceBase, ICurrencyRepository
    {

        #region Constructor

        public CurrencyExternalService(ConfigurationBase configuration) : base(configuration) { }

        #endregion


        #region Public methods

        protected override UriBuilder GetBaseUri() => new UriBuilder(_configuration.CurrenciesApiUrl);


        public async Task<IEnumerable<Currency>> GetAll()
        {
            return GetAllCurrencies()
                .Result.Values
                .AsEnumerable<Currency>();
        }

        public async Task<Currency> Get(string currencySymbol)
        {
            return GetAllCurrencies()
                .Result
                .First(currency => currency.Key == currencySymbol)
                .Value;
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

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

        #region Constructor

        internal CurrencyExternalService(ConfigurationBase configuration) : base(configuration) { }

        #endregion


        #region Protected methods

        protected override UriBuilder GetBaseUri() => new UriBuilder(_configuration.CurrenciesApiUrl);

        #endregion


        #region Public methods

        public async Task<IEnumerable<Currency>> GetAll()
        {
            var task = new Task<IEnumerable<Currency>>(() => {
                return GetAllCurrencies().Result.Values.AsEnumerable<Currency>();
            });
            task.Start();

            return await task;
        }

        public async Task<Currency> Get(string currencySymbol)
        {
            var task = new Task<Currency>(() => {
                return GetAllCurrencies().Result.First(currency => currency.Key == currencySymbol).Value;
            });
            task.Start();

            return await task;
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

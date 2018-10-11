using ChustaSoft.Common.Helpers;
using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public class CountryExternalService : ExternalServiceBase, ICountryRepository
    {

        #region Fields

        private const string ParamPrefixConst = "p";

        #endregion


        #region Constructor

        public CountryExternalService(ConfigurationBase configuration) : base(configuration) { }

        #endregion


        #region Public methods

        protected override UriBuilder GetBaseUri() => new UriBuilder(_configuration.CountriesApiUrl);


        public async Task<IEnumerable<Country>> GetAll()
        {
            string json = await GetStringData(GetBaseUri().Uri);

            var data = JsonConvert.DeserializeObject<CountryApiResponse>(json);

            return data.Response;
        }

        public async Task<Country> Get(AlphaCodeType alphaType, string alphaCode)
        {
            var uri = GetUri(alphaType, alphaCode);
            var json = await GetStringData(uri);
            var data = JsonConvert.DeserializeObject<CountryApiResponse>(json);

            return data.Response.First();
        }

        public async Task<Country> Get(string countryName)
        {
            var uri = GetUri(countryName);
            var json = await GetStringData(uri);
            var data = JsonConvert.DeserializeObject<CountryApiResponse>(json);

            return data.Response.First();
        }

        #endregion


        #region Private methods

        private Uri GetUri(string countryName)
        {
            return GetBaseUri()
                .AddParameter(ParamPrefixConst + nameof(Country.Name), countryName)
                .Uri;
        }

        private Uri GetUri(AlphaCodeType alphaType, string alphaCode)
        {
            var uriBuilder = GetBaseUri();

            switch (alphaType)
            {
                case AlphaCodeType.Alpha2:
                    uriBuilder.AddParameter(ParamPrefixConst + nameof(Country.Alpha2Code), alphaCode);
                    break;
                case AlphaCodeType.Alpha3:
                    uriBuilder.AddParameter(ParamPrefixConst + nameof(Country.Alpha3Code), alphaCode);
                    break;
            }

            return uriBuilder.Uri;
        }

        #endregion

    }
}

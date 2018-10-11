using ChustaSoft.Services.StaticData.Constants;


namespace ChustaSoft.Services.StaticData.Base
{
    public class ConfigurationBase
    {

        #region Fields

        private bool _countriesFromApi = false;

        #endregion


        #region Properties

        internal bool CountriesFromApi => _countriesFromApi;
        
        internal string CountriesApiUrl { get; set; }

        internal string CurrenciesApiUrl { get; set; }

        internal string ExchangeRatesApiUrl { get; set; }

        #endregion


        #region Constructor

        public ConfigurationBase()
        {
            CountriesApiUrl = ApiConnection.CountriesApiUrl;
            CurrenciesApiUrl = ApiConnection.CurrenciesApiUrl;
            ExchangeRatesApiUrl = ApiConnection.ExchangeRatesApiUrl;
        }

        #endregion


        #region Public methods

        public void SetCountriesFromApi(bool countriesFromApi)
        {
            _countriesFromApi = countriesFromApi;
        }

        #endregion

    }
}

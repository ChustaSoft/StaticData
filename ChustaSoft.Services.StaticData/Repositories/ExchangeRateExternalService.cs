using ChustaSoft.Services.StaticData.Base;
using ChustaSoft.Services.StaticData.Models;
using System;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public class ExchangeRateExternalService : ExternalServiceBase, IExchangeRateRepository
    {
        
        #region Constructor

        public ExchangeRateExternalService(ConfigurationBase configuration) : base(configuration) { }

        #endregion

        
        #region Protected methods

        protected override UriBuilder GetBaseUri() => new UriBuilder(_configuration.ExchangeRatesApiUrl);

        #endregion


        #region Public methods


        #endregion

    }
}

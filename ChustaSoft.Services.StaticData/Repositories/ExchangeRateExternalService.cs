using ChustaSoft.Services.StaticData.Base;
using System;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public class ExchangeRateExternalService : ExternalServiceBase, IExchangeRateRepository
    {

        public ExchangeRateExternalService(ConfigurationBase configuration) : base(configuration) { }


        protected override UriBuilder GetBaseUri() => new UriBuilder(_configuration.ExchangeRatesApiUrl);

    }
}

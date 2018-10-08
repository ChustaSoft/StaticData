using ChustaSoft.Services.StaticData.Base;

namespace ChustaSoft.Services.StaticData.Repositories
{
    public class ExchangeRateExternalService : ExternalServiceBase, IExchangeRateRepository
    {
        public ExchangeRateExternalService(ConfigurationBase configuration) : base(configuration) { }
    }
}

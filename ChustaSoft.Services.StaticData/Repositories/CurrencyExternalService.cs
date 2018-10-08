using ChustaSoft.Services.StaticData.Base;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public class CurrencyExternalService : ExternalServiceBase, ICurrencyRepository
    {
        public CurrencyExternalService(ConfigurationBase configuration) : base(configuration) { }
    }
}

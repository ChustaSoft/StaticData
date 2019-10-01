using ChustaSoft.Common.Contracts;

namespace ChustaSoft.Services.StaticData.Helpers
{
    public interface IStaticDataConfigurationBuilder : IBuilder<StaticDataConfiguration>
    {
        StaticDataConfigurationBuilder AddConfiguredCurrency(string configuredCurrency);
        StaticDataConfigurationBuilder AddCurrencyConverterApiKey(string apiKey);
        StaticDataConfigurationBuilder SetApiPreferably(bool apiPreferably);
        StaticDataConfigurationBuilder SetBaseCurrency(string currencyBase);
    }
}
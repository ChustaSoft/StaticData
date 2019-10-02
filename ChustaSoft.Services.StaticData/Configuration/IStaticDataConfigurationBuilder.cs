using ChustaSoft.Common.Contracts;

namespace ChustaSoft.Services.StaticData.Configuration
{
    public interface IStaticDataConfigurationBuilder : IBuilder<StaticDataConfiguration>
    {
        StaticDataConfigurationBuilder AddConfiguredCurrency(string configuredCurrency);
        StaticDataConfigurationBuilder AddCurrencyConverterApiKey(string apiKey);
        StaticDataConfigurationBuilder SetApiPreferably(bool apiPreferably);
        StaticDataConfigurationBuilder SetBaseCurrency(string currencyBase);
    }
}
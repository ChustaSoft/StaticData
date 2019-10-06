namespace ChustaSoft.Services.StaticData.Constants
{
    internal class AppConstants
    {
        internal const string CountriesApiUrl = "http://countryapi.gear.host/v1/Country/getCountries";
        internal const string CurrenciesApiUrl = "https://free.currconv.com/api/v7/currencies";
        internal const string ExchangeRatesApiUrl = "https://api.exchangeratesapi.io/";
        internal const string ExchangeRatesQueryableApiUrl = "https://free.currconv.com/api/v7/convert";

        internal const string FreeConverterApiKeyParam = "apiKey";

        internal const char SEPARATOR_CURRENCIES = '_';
        internal const char SEPARATOR_CONVERSIONS = ',';

        internal const string DATE_API_FORMAT = "yyyy-MM-dd";
    }
}

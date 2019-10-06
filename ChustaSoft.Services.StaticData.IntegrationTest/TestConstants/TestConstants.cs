using ChustaSoft.Services.StaticData.Helpers;
using ChustaSoft.Services.StaticData.Models;


namespace ChustaSoft.Services.StaticData.IntegrationTest.TestConstants
{
    public struct  TestCategories
    {

        public const string CountryTestCategory = nameof(Country);
        public const string CityTestCategory = nameof(City);
        public const string CurrencyTestCategory = nameof(Currency);
        public const string ExchangeRateTestCategory = nameof(ExchangeRate);

        public const string StaticDataServiceFactoryCategory = nameof(StaticDataServiceFactory);
    }

    public struct TestKeys
    {
        public const string TestFreeCurrencyConverterKey = "";
    }

}

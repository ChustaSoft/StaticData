using System.Collections.Generic;
using System.Linq;

namespace ChustaSoft.Services.StaticData.Configuration
{
    public class StaticDataConfiguration
    {

        public bool ApiDataPeferably { get; set; }

        public string CurrencyConversionApiKey { get; set; }

        public string BaseCurrency { get; set; }

        public ICollection<string> ConfiguredCurrencies { get; set; }


        public StaticDataConfiguration() { }

        public StaticDataConfiguration(bool apiPreferably, string baseCurrency)
        {
            ApiDataPeferably = apiPreferably;
            BaseCurrency = baseCurrency;
            ConfiguredCurrencies = Enumerable.Empty<string>().ToList();
        }

    }
}

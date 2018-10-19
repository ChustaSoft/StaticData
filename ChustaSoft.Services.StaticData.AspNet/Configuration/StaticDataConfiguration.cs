using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.AspNet.Configuration
{
    public class StaticDataConfiguration
    {

        public bool ApiDataPeferably { get; set; }

        public string BaseCurrency { get; set; }

        public IEnumerable<string> ConfiguredCurrencies { get; set; }

    }
}

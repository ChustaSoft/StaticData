using Newtonsoft.Json;


namespace ChustaSoft.Services.StaticData.Models
{
    public class CountryLocal : Country
    {

        [JsonProperty("ISO")]
        public override string Alpha2Code { get; set; }

        [JsonProperty("ISO3")]
        public override string Alpha3Code { get; set; }

        [JsonProperty("Country")]
        public override string Name { get; set; }

        [JsonProperty("Continent")]
        public string ContinentCode { get; set; }

        [JsonProperty("Tld")]
        public override string TopLevelDomain { get; set; }

        [JsonProperty("Phone")]
        public override string PhonePrefix { get; set; }

    }
}

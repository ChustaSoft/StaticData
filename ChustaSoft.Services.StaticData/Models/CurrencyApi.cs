using Newtonsoft.Json;


namespace ChustaSoft.Services.StaticData.Models
{
    public class CurrencyApi : Currency
    {

        [JsonProperty("currencyName")]
        public override string Name { get; set; }

        [JsonProperty("currencySymbol")]
        public override string Symbol { get; set; }

    }
}
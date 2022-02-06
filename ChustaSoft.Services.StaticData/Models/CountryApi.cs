using Newtonsoft.Json;


namespace ChustaSoft.Services.StaticData.Models
{
    public class CountryApi : Country
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double Latitude { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double Longitude { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public override double Area { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public override int NumericCode { get; set; }

        [JsonProperty("Flag")]
        public string FlagSvgUrl { get; set; }

        [JsonProperty("FlagPng")]
        public string FlagPngUrl { get; set; }

        public string Languages { get; set; }

        public string Neighbours { get; set; }

    }
}

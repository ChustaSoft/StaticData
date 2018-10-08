using Newtonsoft.Json;


namespace ChustaSoft.Services.StaticData.Models
{
    public class CityLocal : City
    {

        [JsonProperty("country_code")]
        public override string CountryAlpha2Code { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public override double Latitude { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public override double Longitude { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public override int Population { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public override double Elevation { get; set; }

    }
}

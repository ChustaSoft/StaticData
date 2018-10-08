using Newtonsoft.Json;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Models
{
    public class CountryLocalParsed
    {
        [JsonProperty("country_data")]
        public Country Country { get; set; }

        [JsonProperty("cities")]
        public virtual IEnumerable<CityLocal> Cities { get; set; }

    }
}

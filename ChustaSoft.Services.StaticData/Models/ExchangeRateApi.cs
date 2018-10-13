using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ChustaSoft.Services.StaticData.Models
{
    public class ExchangeRateApi : ExchangeRate
    {

        [JsonProperty("id")]
        public string ConversionId { get; set; }

        [JsonProperty("fr")]
        public override string From { get; set; }

        [JsonProperty("to")]
        public override string To { get; set; }

    }

    public class ExchangeRateWithDateApi : ExchangeRateApi
    {

        [JsonProperty("val")]
        public IDictionary<DateTime, double> DateRate
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                base.Date = value.FirstOrDefault().Key;
                base.Rate = value.FirstOrDefault().Value;
            }
        }
        private IDictionary<DateTime, double> _value;

    }

    public class ExchangeRateWithoutDateApi : ExchangeRateApi
    {

        [JsonProperty("val")]
        public override double Rate { get; set; }

    }

}
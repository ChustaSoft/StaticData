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

    #region Inherited ExchangeRateApi Single Classes

    public class ExchangeRateWithDateApi : ExchangeRateApi
    {

        [JsonProperty("val")]
        public IDictionary<DateTime, double> DateRate
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
                base.Date = value.FirstOrDefault().Key;
                base.Rate = value.FirstOrDefault().Value;
            }
        }
        private IDictionary<DateTime, double> _values;

    }

    public class ExchangeRateWithoutDateApi : ExchangeRateApi
    {

        [JsonProperty("val")]
        public override double Rate { get; set; }

    }

    #endregion

}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Models
{
    public class ExchangeRateApiResponse
    {

        public DateTime Date { get; set; }

        public virtual IDictionary<string, ExchangeRateApi> Response { get; set; }
        
    }

    public class ExchangeRateWithDateApiResponse : ExchangeRateApiResponse
    {
        public override IDictionary<string, ExchangeRateApi> Response
        {
            get
            {
                var castedData = new Dictionary<string, ExchangeRateApi>();

                foreach (var ex in ResultsCustom)
                    castedData[ex.Key] = ex.Value;

                return castedData;
            }
            set => base.Response = value;
        }
        
        [JsonProperty("results")]
        public IDictionary<string, ExchangeRateWithDateApi> ResultsCustom { get; set; }

    }

    public class ExchangeRateWithoutDateApiResponse : ExchangeRateApiResponse
    {

        public override IDictionary<string, ExchangeRateApi> Response
        {
            get
            {
                var castedData = new Dictionary<string, ExchangeRateApi>();

                foreach (var ex in ResultsCustom)
                    castedData[ex.Key] = ex.Value;

                return castedData;
            }
            set => base.Response = value;
        }

        [JsonProperty("results")]
        public IDictionary<string, ExchangeRateWithoutDateApi> ResultsCustom { get; set; }

    }
}

using ChustaSoft.Services.StaticData.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Models
{
    public class ExchangeRateApiResponse
    {

        public virtual DateTime? Date { get; set; }

        public virtual DateTime? BeginDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual IDictionary<string, ExchangeRate> Response { get; set; }
        
    }


    #region Inherited ExchangeRateApiResponse DataCollection classes

    public class ExchangeRateDataApiResponse : ExchangeRateApiResponse
    {
        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

    }

    public class ExchangeRateCollectionApiResponse : ExchangeRateDataApiResponse
    {
        public override IDictionary<string, ExchangeRate> Response
        {
            get
            {
                var castedData = new Dictionary<string, ExchangeRate>();

                foreach (var er in ResultsCustom)
                    castedData[er.Key + ExchangeRateConstants.SEPARATOR_CURRENCIES + Base] = new ExchangeRate { Date = Date.Value, From = er.Key, To = Base, Rate = er.Value };

                return castedData;
            }
            set => base.Response = value;
        }

        [JsonProperty("rates")]
        public IDictionary<string, double> ResultsCustom { get; set; }
    }

    public class ExchangeRateHistoricalApiResponse : ExchangeRateDataApiResponse
    {
        public override IDictionary<string, ExchangeRate> Response
        {
            get
            {
                var castedData = new Dictionary<string, ExchangeRate>();

                foreach (var erhistory in ResultsCustom)
                    foreach (var er in erhistory.Value)
                        castedData[er.Key + ExchangeRateConstants.SEPARATOR_CURRENCIES + Base + ExchangeRateConstants.SEPARATOR_CURRENCIES + erhistory.Key.ToString(ExchangeRateConstants.DATE_API_FORMAT)] 
                            = new ExchangeRate { Date = erhistory.Key, From = er.Key, To = Base, Rate = er.Value };

                return castedData;
            }
            set => base.Response = value;
        }

        [JsonProperty("rates")]
        public IDictionary<DateTime, IDictionary<string, double>> ResultsCustom { get; set; }

        [JsonProperty("start_at")]
        public override DateTime? BeginDate { get => base.BeginDate; set => base.BeginDate = value; }

        [JsonProperty("end_at")]
        public override DateTime? EndDate { get => base.BeginDate; set => base.BeginDate = value; }

    }

    #endregion


    #region Inherited ExchangeRateApiResponse Queryable classes

    public class ExchangeRateWithDateApiResponse : ExchangeRateApiResponse
    {
        public override IDictionary<string, ExchangeRate> Response
        {
            get
            {
                var castedData = new Dictionary<string, ExchangeRate>();

                foreach (var er in ResultsCustom)
                    castedData[er.Key] = er.Value;

                return castedData;
            }
            set => base.Response = value;
        }

        [JsonProperty("results")]
        public IDictionary<string, ExchangeRateWithDateApi> ResultsCustom { get; set; }

    }

    public class ExchangeRateWithoutDateApiResponse : ExchangeRateApiResponse
    {

        public override IDictionary<string, ExchangeRate> Response
        {
            get
            {
                var castedData = new Dictionary<string, ExchangeRate>();

                foreach (var er in ResultsCustom)
                    castedData[er.Key] = er.Value;

                return castedData;
            }
            set => base.Response = value;
        }

        [JsonProperty("results")]
        public IDictionary<string, ExchangeRateWithoutDateApi> ResultsCustom { get; set; }

    }

    #endregion

}

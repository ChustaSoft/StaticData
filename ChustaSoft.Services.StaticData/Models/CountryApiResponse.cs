using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Models
{
    public class CountryApiResponse
    {

        public bool IsSuccess { get; set; }

        public string UserMessage { get; set; }

        public string TechnicalMessage { get; set; }

        public int TotalCount { get; set; }


        public IEnumerable<CountryApi> Response { get; set; }

    }
}

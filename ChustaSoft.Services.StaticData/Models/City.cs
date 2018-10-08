using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Models
{
    [Serializable]
    public class City
    {

        public virtual int GeoNameId { get; set; }

        public virtual string Name { get; set; }

        public virtual string NameASCII { get; set; }

        public virtual IEnumerable<string> AlternateNames { get; set; }

        public virtual string CountryAlpha2Code { get; set; }

        public virtual double Latitude { get; set; }

        public virtual double Longitude { get; set; }

        public virtual int Population { get; set; }

        public virtual double Elevation { get; set; }

        public virtual string TimeZone { get; set; }

    }
}

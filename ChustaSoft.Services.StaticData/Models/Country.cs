using System;


namespace ChustaSoft.Services.StaticData.Models
{
    [Serializable]
    public class Country
    {
        public virtual string Alpha2Code { get; set; }

        public virtual string Alpha3Code { get; set; }

        public virtual string FipsCode { get; set; }

        public virtual int NumericCode { get; set; }

        public virtual string Name { get; set; }

        public virtual string NativeName { get; set; }

        public virtual string Capital { get; set; }

        public virtual string Region { get; set; }

        public virtual string SubRegion { get; set; }

        public virtual double Area { get; set; }

        public virtual int Population { get; set; }

        public virtual string NativeLanguage { get; set; }

        public virtual string CurrencyCode { get; set; }

        public virtual string CurrencyName { get; set; }

        public virtual string CurrencySymbol { get; set; }

        public virtual string TopLevelDomain { get; set; }

        public virtual string PhonePrefix { get; set; }

    }
}

using System;


namespace ChustaSoft.Services.StaticData.Models
{
    public class ExchangeRate
    {

        public virtual string From { get; set; }

        public virtual string To { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual double Rate { get; set; }

    }
}

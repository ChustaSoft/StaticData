using ChustaSoft.Common.Exceptions;
using System;


namespace ChustaSoft.Services.StaticData.Exceptions
{
    public class CurrencyNotFoundException : BusinessException
    {

        private static string ErrorMessage = "One or more of the specified currencies could not be found: {0}";
        private string requestedConversion;


        public CurrencyNotFoundException(string currencyName) : base(string.Format(ErrorMessage, currencyName)) { }
        
        public CurrencyNotFoundException(string currencyName, Exception innerException)
            : base(string.Format(ErrorMessage, currencyName), innerException) { }

    }
}

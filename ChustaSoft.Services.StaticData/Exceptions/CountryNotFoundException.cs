using ChustaSoft.Common.Exceptions;
using System;


namespace ChustaSoft.Services.StaticData.Exceptions
{
    public class CountryNotFoundException : BusinessException
    {

        private static string ErrorMessage = "The specified country does not exist or is inaccessible. {0}";

        public CountryNotFoundException(string countryName, Exception innerException) 
            : base(string.Format(ErrorMessage, countryName), innerException) { }

    }
}

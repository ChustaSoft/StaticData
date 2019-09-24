using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.AspNet.Controllers
{

    [Route("api/staticdata")]
    public class StaticDataController : Controller
    {
        
        #region Fields

        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly ICurrencyService _currencyService;
        private readonly IExchangeRateService _exchangeRateService;

        #endregion


        #region Constructor

        [Obsolete("Version 2.0 will replace it by one inherited by ChustaSoft ApiControllerBase")]
        public StaticDataController(ICityService cityService, ICountryService countryService, ICurrencyService currencyService, IExchangeRateService exchangeRateService)
        {
            _cityService = cityService;
            _countryService = countryService;
            _currencyService = currencyService;
            _exchangeRateService = exchangeRateService;
        }

        #endregion


        #region Public methods

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("cities/{country}")]
        public ActionResponse<IEnumerable<City>> GetCities(string country)
        {
            return _cityService.Get(country);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("cities")]
        public ActionResponse<IEnumerable<City>> GetCities([FromBody] List<string> countries)
        {
            return _cityService.Get(countries);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("countries")]
        public ActionResponse<IEnumerable<Country>> GetCountries()
        {
            return _countryService.GetAll();
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("countries/{countryName}")]
        public ActionResponse<Country> GetCountry(string countryName)
        {
            return _countryService.Get(countryName);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("countries/{alphaType}/{alphaCode}")]
        public ActionResponse<Country> GetCountry(string alphaType, string alphaCode)
        {
            var enumType = (AlphaCodeType)Enum.Parse(typeof(AlphaCodeType), alphaType);

            return _countryService.Get(enumType, alphaCode);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("currencies")]
        public ActionResponse<IEnumerable<Currency>> GetCurrencies()
        {
            return _currencyService.GetAll();
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("currencies/{currencySymbol}")]
        public ActionResponse<Currency> GetCurrency(string currencySymbol)
        {
            return _currencyService.Get(currencySymbol);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("exchangerates/{currencyFrom}/{currencyTo}/{date}")]
        public ActionResponse<ExchangeRate> GetExchangeRate(string currencyFrom, string currencyTo, string date)
        {
            var parsedDate = ParseDate(date);

            return _exchangeRateService.Get(currencyFrom, currencyTo, parsedDate);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("exchangerates/latest/{currency}")]
        public ActionResponse<IEnumerable<ExchangeRate>> GetLatest(string currency)
        {
            return _exchangeRateService.GetLatest(currency);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("exchangerates/historical/{currency}/{beginDate}/{endDate}")]
        public ActionResponse<IEnumerable<ExchangeRate>> GetHistorical(string currency, string beginDate, string endDate)
        {
            var parsedBeginDate = ParseDate(beginDate);
            var parsedEndDate = ParseDate(endDate);

            return _exchangeRateService.GetHistorical(currency, parsedBeginDate.Value, parsedEndDate.Value);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("exchangerates/bidirectional/{currencyFrom}/{currencyTo}/{date}")]
        public ActionResponse<IEnumerable<ExchangeRate>> GetBidirectional(string currencyFrom, string currencyTo, string date)
        {
            var parsedDate = ParseDate(date);

            return _exchangeRateService.GetBidirectional(currencyFrom, currencyTo, parsedDate);
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("exchangerates/configured/latest")]
        public ActionResponse<ICollection<ExchangeRate>> GetConfiguredLatest()
        {
            return _exchangeRateService.GetConfiguredLatest();
        }

        [Obsolete("Version 2.0 will make it async")]
        [HttpGet("exchangerates/configured/historical/{beginDate}/{endDate}")]
        public ActionResponse<ICollection<ExchangeRate>> GetConfiguredHistorical(string beginDate, string endDate)
        {
            var parsedBeginDate = ParseDate(beginDate);
            var parsedEndDate = ParseDate(endDate);

            return _exchangeRateService.GetConfiguredHistorical(parsedBeginDate.Value, parsedEndDate.Value);
        }

        #endregion


        #region Private methods

        private static DateTime? ParseDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
                return DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            else
                return null;
        }

        #endregion

    }
}

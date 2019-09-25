using ChustaSoft.Common.Base;
using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.AspNet.Controllers
{

    [Route("api/staticdata")]
    public class StaticDataController : ApiControllerBase<StaticDataController>
    {
        
        #region Fields

        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly ICurrencyService _currencyService;
        private readonly IExchangeRateService _exchangeRateService;

        #endregion


        #region Constructor

        public StaticDataController(ILogger<StaticDataController> logger, ICityService cityService, ICountryService countryService, ICurrencyService currencyService, IExchangeRateService exchangeRateService)
            : base(logger)
        {
            _cityService = cityService;
            _countryService = countryService;
            _currencyService = currencyService;
            _exchangeRateService = exchangeRateService;
        }

        #endregion


        #region Public methods

        [HttpGet("cities/{country}")]
        public IActionResult GetCities(string country)
        {
            var actionResponse = GetEmptyResponseBuilder<IEnumerable<City>>();
            try
            {
                return Ok(_cityService.Get(country));
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("cities")]
        public IActionResult GetCities([FromBody] IEnumerable<string> countries)
        {
            var actionResponse = GetEmptyResponseBuilder<IEnumerable<City>>();
            try
            {
                return Ok(_cityService.Get(countries));
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("countries")]
        public ActionResponse<IEnumerable<Country>> GetCountries()
        {
            return _countryService.GetAll();
        }

        [HttpGet("countries/{countryName}")]
        public ActionResponse<Country> GetCountry(string countryName)
        {
            return _countryService.Get(countryName);
        }

        [HttpGet("countries/{alphaType}/{alphaCode}")]
        public ActionResponse<Country> GetCountry(string alphaType, string alphaCode)
        {
            var enumType = (AlphaCodeType)Enum.Parse(typeof(AlphaCodeType), alphaType);

            return _countryService.Get(enumType, alphaCode);
        }

        [HttpGet("currencies")]
        public ActionResponse<IEnumerable<Currency>> GetCurrencies()
        {
            return _currencyService.GetAll();
        }

        [HttpGet("currencies/{currencySymbol}")]
        public ActionResponse<Currency> GetCurrency(string currencySymbol)
        {
            return _currencyService.Get(currencySymbol);
        }

        [HttpGet("exchangerates/{currencyFrom}/{currencyTo}/{date}")]
        public ActionResponse<ExchangeRate> GetExchangeRate(string currencyFrom, string currencyTo, string date)
        {
            var parsedDate = ParseDate(date);

            return _exchangeRateService.Get(currencyFrom, currencyTo, parsedDate);
        }

        [HttpGet("exchangerates/latest/{currency}")]
        public ActionResponse<IEnumerable<ExchangeRate>> GetLatest(string currency)
        {
            return _exchangeRateService.GetLatest(currency);
        }

        [HttpGet("exchangerates/historical/{currency}/{beginDate}/{endDate}")]
        public ActionResponse<IEnumerable<ExchangeRate>> GetHistorical(string currency, string beginDate, string endDate)
        {
            var parsedBeginDate = ParseDate(beginDate);
            var parsedEndDate = ParseDate(endDate);

            return _exchangeRateService.GetHistorical(currency, parsedBeginDate.Value, parsedEndDate.Value);
        }

        [HttpGet("exchangerates/bidirectional/{currencyFrom}/{currencyTo}/{date}")]
        public ActionResponse<IEnumerable<ExchangeRate>> GetBidirectional(string currencyFrom, string currencyTo, string date)
        {
            var parsedDate = ParseDate(date);

            return _exchangeRateService.GetBidirectional(currencyFrom, currencyTo, parsedDate);
        }

        [HttpGet("exchangerates/configured/latest")]
        public ActionResponse<ICollection<ExchangeRate>> GetConfiguredLatest()
        {
            return _exchangeRateService.GetConfiguredLatest();
        }

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

using ChustaSoft.Common.Base;
using ChustaSoft.Common.Helpers;
using ChustaSoft.Services.StaticData.Enums;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
                actionResponse.SetData(_cityService.Get(country));

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("cities")]
        public IActionResult GetCities([FromBody] IEnumerable<string> countries)
        {
            var actionResponse = GetEmptyResponseBuilder<List<City>>();
            try
            {
                var data = _cityService.Get(countries).SelectMany(x => x.Value.Cities).ToList();
                actionResponse.SetData(data);

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var actionResponse = GetEmptyResponseBuilder<IEnumerable<Country>>();
            try
            {
                actionResponse.SetData(_countryService.GetAll());

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("countries/{countryName}")]
        public IActionResult GetCountry(string countryName)
        {
            var actionResponse = GetEmptyResponseBuilder<Country>();
            try
            {
                actionResponse.SetData(_countryService.Get(countryName));

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("countries/{alphaType}/{alphaCode}")]
        public IActionResult GetCountry(string alphaType, string alphaCode)
        {
            var actionResponse = GetEmptyResponseBuilder<Country>();
            try
            {
                var enumType = EnumsHelper.GetByString<AlphaCodeType>(alphaType);
                actionResponse.SetData(_countryService.Get(enumType, alphaCode));

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("currencies")]
        public IActionResult GetCurrencies()
        {
            var actionResponse = GetEmptyResponseBuilder<IEnumerable<Currency>>();
            try
            {
                actionResponse.SetData(_currencyService.GetAll());

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("currencies/{currencySymbol}")]
        public IActionResult GetCurrency(string currencySymbol)
        {
            var actionResponse = GetEmptyResponseBuilder<Currency>();
            try
            {
                actionResponse.SetData(_currencyService.Get(currencySymbol));

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("exchangerates/{currencyFrom}/{currencyTo}/{date}")]
        public IActionResult GetExchangeRate(string currencyFrom, string currencyTo, string date)
        {
            var actionResponse = GetEmptyResponseBuilder<ExchangeRate>();
            try
            {
                var parsedDate = ParseDate(date);

                actionResponse.SetData(_exchangeRateService.Get(currencyFrom, currencyTo, parsedDate));

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("exchangerates/latest/{currency}")]
        public IActionResult GetLatest(string currency)
        {
            var actionResponse = GetEmptyResponseBuilder<IEnumerable<ExchangeRate>>();
            try
            {
                actionResponse.SetData(_exchangeRateService.GetLatest(currency));

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("exchangerates/historical/{currency}/{beginDate}/{endDate}")]
        public IActionResult GetHistorical(string currency, string beginDate, string endDate)
        {
            var actionResponse = GetEmptyResponseBuilder<IEnumerable<ExchangeRate>>();
            try
            {
                var parsedBeginDate = ParseDate(beginDate);
                var parsedEndDate = ParseDate(endDate);

                actionResponse.SetData(_exchangeRateService.GetHistorical(currency, parsedBeginDate.Value, parsedEndDate.Value));

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
        }

        [HttpGet("exchangerates/bidirectional/{currencyFrom}/{currencyTo}/{date}")]
        public IActionResult GetBidirectional(string currencyFrom, string currencyTo, string date)
        {
            var actionResponse = GetEmptyResponseBuilder<IEnumerable<ExchangeRate>>();
            try
            {
                var parsedDate = ParseDate(date);

                actionResponse.SetData(_exchangeRateService.GetBidirectional(currencyFrom, currencyTo, parsedDate));

                return Ok(actionResponse);
            }
            catch (Exception ex)
            {
                return Ko(actionResponse, ex);
            }
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

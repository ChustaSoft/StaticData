using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    /// <summary>
    /// StaticData ExchangeRate Service
    /// </summary>
    public interface IExchangeRateService
    {

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Get Exchange Rate for between two currencies, optional by a date, otherwise the latest will be obtained
        /// </summary>
        /// <param name="currencyFrom">Currency Code from</param>
        /// <param name="currencyTo">Currency code to</param>
        /// <param name="today">Optional, date for requested Exchange Rate</param>
        /// <returns>ActionResponse with Exchange Rate retrived</returns>
        ActionResponse<ExchangeRate> Get(string currencyFrom, string currencyTo, DateTime? today = null);

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Get latest Exchange Rate between the selected currency and Configured Base currency
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>ActionResponse with Exchange Rates retrived</returns>
        ActionResponse<IEnumerable<ExchangeRate>> GetLatest(string currency);

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Get historical Exchange Rates for between the selected currency and Configured Base currency inside a Date Range
        /// </summary>
        /// <param name="currency">Selected currency</param>
        /// <param name="beginDate">Begin Date for filtering</param>
        /// <param name="endDate">End Date for filtering</param>
        /// <returns>ActionResponse with Exchange Rates retrived</returns>
        ActionResponse<IEnumerable<ExchangeRate>> GetHistorical(string currency, DateTime beginDate, DateTime endDate);

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Get Exchange Rates between selected currencies and optionally, in a date, otherwise the latest will be obtained, in both sides
        /// </summary>
        /// <param name="currencyFrom">Currency Code from</param>
        /// <param name="currencyTo">Currency code to</param>
        /// <param name="date">Optional, date for requested Exchange Rates</param>
        /// <returns>ActionResponse with Exchange Rates retrived</returns>
        ActionResponse<IEnumerable<ExchangeRate>> GetBidirectional(string currencyFrom, string currencyTo, DateTime? date = null);

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Gets Exchange Rates for all currencies, taking special account of configured currencies compared with base currency
        /// </summary>
        /// <returns>ActionResponse with retrived Exchange Rates</returns>
        ActionResponse<ICollection<ExchangeRate>> GetConfiguredLatest();

        [Obsolete("Version 2.0 will make it async and replace ActionResponse in this layer")]
        /// <summary>
        /// Gets Exchange Rates for all currencies, taking special account of configured currencies compared with base currency, inside a date range
        /// </summary>
        /// <param name="beginDate">Begin Date for filtering</param>
        /// <param name="endDate">End Date for filtering</param>
        /// <returns>ActionResponse with Exchange Rates retrived</returns>
        ActionResponse<ICollection<ExchangeRate>> GetConfiguredHistorical(DateTime beginDate, DateTime endDate);

    }
}

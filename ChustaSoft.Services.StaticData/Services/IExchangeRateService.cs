using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChustaSoft.Services.StaticData.Services
{
    /// <summary>
    /// StaticData ExchangeRate Service
    /// </summary>
    public interface IExchangeRateService
    {

        /// <summary>
        /// Get Exchange Rate for between two currencies, optional by a date, otherwise the latest will be obtained
        /// </summary>
        /// <param name="currencyFrom">Currency Code from</param>
        /// <param name="currencyTo">Currency code to</param>
        /// <param name="today">Optional, date for requested Exchange Rate</param>
        /// <returns>Exchange Rate retrived</returns>
        ExchangeRate Get(string currencyFrom, string currencyTo, DateTime? today = null);
        Task<ExchangeRate> GetAsync(string currencyFrom, string currencyTo, DateTime? today = null);

        /// <summary>
        /// Get latest Exchange Rate between the selected currency and Configured Base currency
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Exchange Rates retrived</returns>
        IEnumerable<ExchangeRate> GetLatest(string currency);
        Task<IEnumerable<ExchangeRate>> GetLatestAsync(string currency);

        /// <summary>
        /// Get historical Exchange Rates for between the selected currency and Configured Base currency inside a Date Range
        /// </summary>
        /// <param name="currency">Selected currency</param>
        /// <param name="beginDate">Begin Date for filtering</param>
        /// <param name="endDate">End Date for filtering</param>
        /// <returns>Exchange Rates retrived</returns>
        IEnumerable<ExchangeRate> GetHistorical(string currency, DateTime beginDate, DateTime endDate);
        Task<IEnumerable<ExchangeRate>> GetHistoricalAsync(string currency, DateTime beginDate, DateTime endDate);

        /// <summary>
        /// Get Exchange Rates between selected currencies and optionally, in a date, otherwise the latest will be obtained, in both sides
        /// </summary>
        /// <param name="currencyFrom">Currency Code from</param>
        /// <param name="currencyTo">Currency code to</param>
        /// <param name="date">Optional, date for requested Exchange Rates</param>
        /// <returns>Exchange Rates retrived</returns>
        IEnumerable<ExchangeRate> GetBidirectional(string currencyFrom, string currencyTo, DateTime? date = null);
        Task<IEnumerable<ExchangeRate>> GetBidirectionalAsync(string currencyFrom, string currencyTo, DateTime? date = null);

        /// <summary>
        /// Gets Exchange Rates for all currencies, taking special account of configured currencies compared with base currency
        /// </summary>
        /// <returns>Retrived Exchange Rates</returns>
        IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> GetConfiguredLatest();
        Task<IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>> GetConfiguredLatestAsync();

        /// <summary>
        /// Gets Exchange Rates for all currencies, taking special account of configured currencies compared with base currency, inside a date range
        /// </summary>
        /// <param name="beginDate">Begin Date for filtering</param>
        /// <param name="endDate">End Date for filtering</param>
        /// <returns>Exchange Rates retrived</returns>
        IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)> GetConfiguredHistorical(DateTime beginDate, DateTime endDate);
        Task<IDictionary<string, (bool Found, IEnumerable<ExchangeRate> ExchangeRates)>> GetConfiguredHistoricalAsync(DateTime beginDate, DateTime endDate);

    }
}

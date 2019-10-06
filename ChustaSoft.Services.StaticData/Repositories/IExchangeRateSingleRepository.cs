using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    internal interface IExchangeRateSingleRepository
    {

        Task<ExchangeRate> GetAsync(string currencyFrom, string currencyTo, DateTime? today = null);

        Task<IEnumerable<ExchangeRate>> GetBidirectionalAsync(string currencyFrom, string currencyTo, DateTime? today = null);

        Task<IEnumerable<ExchangeRate>> GetHistoricalAsync(string currencyFrom, string currencyTo, DateTime beginDate, DateTime endDate);

    }
}

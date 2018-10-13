using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public interface IExchangeRateQueryableRepository
    {

        Task<ExchangeRate> Get(string currencyFrom, string currencyTo, DateTime? today = null);

        Task<IEnumerable<ExchangeRate>> GetBidirectional(string currencyFrom, string currencyTo, DateTime? today = null);

    }
}

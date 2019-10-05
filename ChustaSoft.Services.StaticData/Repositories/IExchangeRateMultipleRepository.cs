using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    internal interface IExchangeRateMultipleRepository
    {

        Task<IEnumerable<ExchangeRate>> GetLatestAsync(string currency);

        Task<IEnumerable<ExchangeRate>> GetHistoricalAsync(string currency, DateTime beginDate, DateTime endDate);

    }
}

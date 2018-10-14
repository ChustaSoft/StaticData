using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    public interface IExchangeRateMultipleRepository
    {

        Task<IEnumerable<ExchangeRate>> GetLatest(string currency);

        Task<IEnumerable<ExchangeRate>> GetHistorical(string currency, DateTime beginDate, DateTime endDate);

    }
}

using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using System;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    public interface IExchangeRateService
    {

        ActionResponse<ExchangeRate> Get(string currencyFrom, string currencyTo, DateTime? today = null);

        ActionResponse<IEnumerable<ExchangeRate>> GetLatest(string currency);

        ActionResponse<IEnumerable<ExchangeRate>> GetHistorical(string currency, DateTime beginDate, DateTime endDate);

        ActionResponse<IEnumerable<ExchangeRate>> GetBidirectional(string currencyFrom, string currencyTo, DateTime? date = null);

        ActionResponse<IEnumerable<ExchangeRate>> GetConfiguredLatest();

        ActionResponse<IEnumerable<ExchangeRate>> GetConfiguredHistorical(DateTime beginDate, DateTime endDate);

    }
}

using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    public interface ICurrencyService
    {

        ActionResponse<IEnumerable<Currency>> GetAll();

        ActionResponse<Currency> Get(string currencySymbol);

    }
}

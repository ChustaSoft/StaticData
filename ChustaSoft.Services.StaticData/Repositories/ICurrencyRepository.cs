using ChustaSoft.Services.StaticData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Repositories
{
    internal interface ICurrencyRepository
    {

        Task<IEnumerable<Currency>> GetAll();

        Task<Currency> Get(string currencySymbol);

    }
}

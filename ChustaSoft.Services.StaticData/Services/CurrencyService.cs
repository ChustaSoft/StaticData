using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChustaSoft.Services.StaticData.Services
{
    public class CurrencyService : ICurrencyService
    {
        
        #region Fields

        private ICurrencyRepository _currencyRepository;

        #endregion


        #region Constructor

        internal CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        #endregion


        #region Public methods

        public Currency Get(string currencySymbol)
        {
            return _currencyRepository.Get(currencySymbol).Result;
        }

        public async Task<Currency> GetAsync(string currencySymbol)
        {
            return await _currencyRepository.Get(currencySymbol);
        }

        public IEnumerable<Currency> GetAll()
        {
            return _currencyRepository.GetAll().Result;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _currencyRepository.GetAll();
        }

        #endregion

    }
}

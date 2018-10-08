using ChustaSoft.Common.Exceptions;
using ChustaSoft.Common.Utilities;
using ChustaSoft.Services.StaticData.Models;
using ChustaSoft.Services.StaticData.Repositories;
using System.Collections.Generic;


namespace ChustaSoft.Services.StaticData.Services
{
    public class CityService : ICityService
    {
        
        #region Fields

        private ICityRepository _cityRepository;

        #endregion


        #region Constructor

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        } 
        
        
        #endregion


        #region Public methods

        public ActionResponse<IEnumerable<City>> Get(string country)
        {
            try
            {
                var data = _cityRepository.Get(country);

                return new ActionResponse<IEnumerable<City>>(data);
            }
            catch (BusinessException ex)
            {
                return new ActionResponse<IEnumerable<City>>(new List<ErrorMessage> { new ErrorMessage(ex) });
            }

        }
        public ActionResponse<IEnumerable<City>> Get(List<string> countries)
        {
            var actionResponse = new ActionResponse<IEnumerable<City>>();
            var cities = new List<City>();

            foreach (var country in countries)
            {
                try
                {
                    cities.AddRange(_cityRepository.Get(country));
                }
                catch (BusinessException ex)
                {
                    actionResponse.Errors.Add(new ErrorMessage(ex));
                }
            }
            actionResponse.Data = cities;

            return actionResponse;
        }

        #endregion

    }
}

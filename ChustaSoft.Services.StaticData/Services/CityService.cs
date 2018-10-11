﻿using ChustaSoft.Common.Enums;
using ChustaSoft.Common.Exceptions;
using ChustaSoft.Common.Helpers;
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
            var arBuilder = new ActionResponseBuilder<IEnumerable<City>>();
            try
            {
                var data = _cityRepository.Get(country);

                arBuilder.AddData(data);
            }
            catch (BusinessException ex)
            {
                arBuilder.AddError(ex);
            }
            return arBuilder.Build();
        }

        public ActionResponse<IEnumerable<City>> Get(List<string> countries)
        {
            var arBuilder = new ActionResponseBuilder<IEnumerable<City>>();
            var cities = new List<City>();

            foreach (var country in countries)
            {
                try
                {
                    cities.AddRange(_cityRepository.Get(country));
                }
                catch (BusinessException ex)
                {
                    arBuilder.AddError(ex);
                    arBuilder.SetStatus(ActionResponseType.Warning);
                }
            }
            
            return arBuilder.AddData(cities).Build();
        }

        #endregion

    }
}
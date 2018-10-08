using ChustaSoft.Services.StaticData.Constants;


namespace ChustaSoft.Services.StaticData.Base
{
    public abstract class ConfigurationBase
    {

        #region Properties

        public virtual string CountriesApiUrl { get; set; }

        public abstract bool CountriesFromApi { get; set; }

        #endregion


        #region Constructor

        public ConfigurationBase()
        {
            CountriesApiUrl = ApiConnection.CountriesApiUrl;
        }

        #endregion

    }
}

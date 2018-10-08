using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace ChustaSoft.Services.StaticData.Base
{

    public class ExternalServiceBase
    {
        
        #region Fields

        protected readonly ConfigurationBase _configuration;

        #endregion


        #region Constructor

        public ExternalServiceBase(ConfigurationBase configuration)
        {
            _configuration = configuration;
        }

        #endregion


        #region Protected methods

        protected async Task<string> GetStringData(Uri url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                using (var content = response.Content)
                {
                    return await content.ReadAsStringAsync();
                }
            }
        }

        #endregion

    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChustaSoft.Services.StaticData.Base
{

    public abstract class ExternalServiceBase
    {
        
        #region Fields

        protected readonly InternalConfiguration _configuration;

        #endregion


        #region Constructor

        public ExternalServiceBase(InternalConfiguration configuration)
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


        #region Protected abstract methods

        protected abstract UriBuilder GetBaseUri();

        #endregion

    }
}

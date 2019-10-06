using ChustaSoft.Services.StaticData.Repositories;
using ChustaSoft.Services.StaticData.Services;


namespace ChustaSoft.Services.StaticData.IntegrationTest.Helpers
{
    internal class CityTestHelper
    {
        
        #region Internal methods

        internal static ICityRepository CreateMockRepository()
        {
            return new CityLocalRepository();
        }

        internal static ICityService CreateMockService()
        {
            var cityRepository = CreateMockRepository();

            return new CityService(cityRepository);
        }

        #endregion

    }
}

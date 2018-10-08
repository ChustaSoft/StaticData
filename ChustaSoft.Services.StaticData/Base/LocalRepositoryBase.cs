using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace ChustaSoft.Services.StaticData.Base
{
    public class LocalRepositoryBase
    {

        #region Protected methods

        protected T GetParsedData<T>(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings { Error = HandleDeserializationError });
                var stringData = file.ReadToEnd();

                return JsonConvert.DeserializeObject<T>(stringData);
            }
        }

        protected IEnumerable<T> GetAllFileData<T>(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { Error = HandleDeserializationError });

                return (List<T>)serializer.Deserialize(file, typeof(List<T>));
            }
        }

        protected void HandleDeserializationError(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }

        #endregion

    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace ChustaSoft.Services.StaticData.Base
{
    public class LocalRepositoryBase
    {
        
        #region Fields

        private const char NAMESPACE_SEPARATOR = '.';
        private const char FOLDER_SEPARATOR = '\\';
        private const string DATA_FOLDER = "Data";
        private const string JSON_EXTENSION = ".json";

        #endregion
        
        
        #region Protected methods

        protected T GetParsedData<T>(string internalFileName)
        {
            #if DEBUG
            return GetParsedNonInternalData<T>(internalFileName);
            
            #else
            return GetParsedInternalData<T>(internalFileName);
            #endif
        }

        protected IEnumerable<T> GetAllFileData<T>(string fileName)
        {
            var fullPath = GetFilePath(fileName);

            using (StreamReader file = File.OpenText(fullPath))
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


        #region Private methods
        
        private T GetParsedInternalData<T>(string internalFileName)
        {
            var assembly = Assembly.GetCallingAssembly();
            var fullPath = GetAssemblyPath(assembly, internalFileName);

            using (Stream stream = assembly.GetManifestResourceStream(fullPath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var stringData = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<T>(stringData);
                }
            }
        }

        private T GetParsedNonInternalData<T>(string internalFileName)
        {
            var fullPath = GetFilePath(internalFileName);

            using (StreamReader file = File.OpenText(fullPath))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings { Error = HandleDeserializationError });
                var stringData = file.ReadToEnd();

                return JsonConvert.DeserializeObject<T>(stringData);
            }
        }

        private string GetFilePath(string internalFileName)
        {
            return DATA_FOLDER + FOLDER_SEPARATOR + internalFileName + JSON_EXTENSION;
        }

        private string GetAssemblyPath(Assembly assembly, string internalFileName)
        {
            var nameSpace = typeof(LocalRepositoryBase).Assembly.GetName().Name;

            return nameSpace + NAMESPACE_SEPARATOR + DATA_FOLDER + NAMESPACE_SEPARATOR + (internalFileName == string.Empty ? string.Empty : internalFileName + JSON_EXTENSION);
        }

        #endregion

    }
}

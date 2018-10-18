using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace ChustaSoft.Services.StaticData.Base
{
    internal class LocalRepositoryBase
    {
        
        #region Fields

        private const char NAMESPACE_SEPARATOR = '.';
        private const char FOLDER_SEPARATOR = '\\';
        private const string DATA_FOLDER = "Data";
        private const string JSON_EXTENSION = ".json";

        #endregion
        
        
        #region Protected methods

        protected T GetParsedElement<T>(string internalFileName)
        {
            #if DEBUG
            return GetParsedElementFromSolutionData<T>(internalFileName);
            #else
            return GetParsedElementFromAssemblyData<T>(internalFileName);
            #endif
        }

        protected IEnumerable<T> GetParsedCollection<T>(string internalFileName)
        {
            #if DEBUG
            return GetParsedCollectionFromSolutionData<T>(internalFileName);
            #else
            return GetParsedCollectionFromAssemblyData<T>(internalFileName);
            #endif
        }

        #endregion


        #region Private methods

        private T GetParsedElementFromAssemblyData<T>(string internalFileName)
        {
            var fullPath = GetAssemblyFilePath(internalFileName);

            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream(fullPath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var stringData = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<T>(stringData);
                }
            }
        }

        private T GetParsedElementFromSolutionData<T>(string internalFileName)
        {
            var fullPath = GetSolutionFilePath(internalFileName);

            using (StreamReader reader = File.OpenText(fullPath))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings { Error = HandleDeserializationError });
                var stringData = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<T>(stringData);
            }
        }

        private IEnumerable<T> GetParsedCollectionFromAssemblyData<T>(string internalFileName)
        {
            var fullPath = GetAssemblyFilePath(internalFileName);

            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream(fullPath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var serializer = JsonSerializer.Create(new JsonSerializerSettings { Error = HandleDeserializationError });

                    return (List<T>)serializer.Deserialize(reader, typeof(List<T>));
                }
            }
        }

        private IEnumerable<T> GetParsedCollectionFromSolutionData<T>(string internalFileName)
        {
            var fullPath = GetSolutionFilePath(internalFileName);

            using (StreamReader reader = File.OpenText(fullPath))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings { Error = HandleDeserializationError });

                return (List<T>)serializer.Deserialize(reader, typeof(List<T>));
            }
        }

        private string GetSolutionFilePath(string internalFileName)
        {
            return DATA_FOLDER + FOLDER_SEPARATOR + internalFileName + JSON_EXTENSION;
        }

        private string GetAssemblyFilePath(string internalFileName)
        {
            var nameSpace = typeof(LocalRepositoryBase).Assembly.GetName().Name;

            return nameSpace + NAMESPACE_SEPARATOR + DATA_FOLDER + NAMESPACE_SEPARATOR + (internalFileName == string.Empty ? string.Empty : internalFileName + JSON_EXTENSION);
        }

        private void HandleDeserializationError(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }

        #endregion

    }
}

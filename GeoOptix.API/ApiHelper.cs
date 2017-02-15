using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using GeoOptix.API.Interface;
using GeoOptix.API.Model;
using Keystone.API;
using Newtonsoft.Json;

namespace GeoOptix.API
{
    public class ApiHelper
    {
        private readonly string _url;
        private HttpResponseMessage _response;
        public AuthResponseModel AuthToken { get; private set; }

        private const string FOLDERS = "folders";
        private const string FIELDFOLDERS = "fieldfolders";
        private const string FILES = "files";
        private const string DESCRIPTION = "description";
        private const string METRICSCHEMAS = "metricschemas";
        private const string METRICS = "metrics";
        private const string MEASUREMENTS = "measurements";

        public const string SCHEMA_NAME_STRING = "SchemaName";
        public const string SCHEMA_LOCKED_STRING = "SchemaLocked";
        public const string SCHEMA_PUBLISHED_STRING = "SchemaPublished";

        public const string FOLDER_PUBLISHED_STRING = "FolderPublished";
        public const string FOLDER_LOCKED_STRING = "FolderLocked";

        public const string LAST_MEASUREMENT_CHANGE = "lastmeasurementchange";      // Must be lower case!


        public HttpStatusCode StatusCode
        {
            get
            {
                return _response != null ? _response.StatusCode : HttpStatusCode.Unused;
            }
        }


        public ApiHelper(string fullUrl, string authUrl, string clientId, string clientSecret, string username, string password) : this (fullUrl)
        {
            var keystoneApiHelper = new KeystoneApiHelper(authUrl, clientId, clientSecret);
            AuthToken = keystoneApiHelper.GetAuthToken(username, password);
        }


        public ApiHelper(string fullUrl, AuthResponseModel authToken)
            : this(fullUrl)
        {
            AuthToken = authToken;
        }


        public ApiHelper(string fullUrl)
        {
            _url = fullUrl;
        }
        

        public ApiResponse<FolderModel> GetFolder(string folderName)
        {
            var url = MakeFolderUrl(_url, folderName);

            return Get<FolderModel>(url);
        }


        // Get a list of associated folders
        public ApiResponse<FolderSummaryModel[]> GetFolders()
        {
            var url = MakeFolderCollectionUrl(_url);

            return Get<FolderSummaryModel[]>(url);
        }


        // Get a list of associated field folders
        public ApiResponse<FolderSummaryModel[]> GetFieldFolders()
        {
            var url = MakeFieldFolderCollectionUrl(_url);

            return Get<FolderSummaryModel[]>(url);
        }


        public ApiResponse<MeasurementSummaryModel[]> GetMeasurementTypes()
        {
            var url = MakeMeasurementTypeUrl(_url);
            return Get<MeasurementSummaryModel[]>(url);
        }


        public ApiResponse<MeasurementModel<T>> GetMeasurement<T>(string measurementName)
        {
            var url = MakeMeasurementUrl(_url, measurementName);
            return Get<MeasurementModel<T>>(url);
        }


        public ApiResponse<MetricSchemaModel[]> GetMetricSchemas(ObjectType level)
        {
            var url = MakeMetricSchemaCollectionUrl(_url, level);

            return Get<MetricSchemaModel[]>(url);
        }


        public ApiResponse<MetricSchemaModel> GetMetricSchema(ObjectType objectType, string schemaName)
        {
            var url = MakeMetricSchemaUrl(_url, objectType, schemaName);
            return Get<MetricSchemaModel>(url);
        }


        public ApiResponse<MetricSchemaModel> GetMetricSchema(MetricSchemaModel model)
        {
            return Get<MetricSchemaModel>(new Uri(model.Url));
        }


        // Get all instances for the specified object and schema
        public ApiResponse<MetricInstanceModel[]> GetMetricInstances(IHasUrl objectModel, string schemaName)
        {
            var url = MakeMetricCollectionUrl(objectModel.Url, schemaName);

            return Get<MetricInstanceModel[]>(url);
        }


        public ApiResponse<MetricInstanceModel> GetMetricInstance(IHasUrl objectModel, string schemaName, string instanceName)
        {
            var url = MakeMetricInstanceUrl(objectModel.Url, schemaName, instanceName);

            return Get<MetricInstanceModel>(url);
        }


        public ApiResponse<MetricInstanceModel> GetMetricInstance(MetricInstanceSummaryModel model)
        {
            return Get<MetricInstanceModel>(new Uri(model.Url));
        }


        public ApiResponse<MetricInstanceModel> GetMetricInstance(string baseUrl, string schemaName, string instanceName)
        {
            var url = MakeMetricInstanceUrl(baseUrl, schemaName, instanceName);
            return Get<MetricInstanceModel>(url);
        }


        public ApiResponse<FolderModel> CreateFolder(string folderName)
        {
            var url = MakeFolderCollectionUrl(_url);

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("Name", folderName));

            return Post<FolderModel>(url, values);
        }


        public ApiResponse<MetricSchemaModel> CreateSchema(string schemaName, ObjectType objectType, List<KeyValuePair<string, string>> attributes)
        {
            var url = MakeMetricSchemaCollectionUrl(_url, objectType);
            return PostSchema(url, schemaName, attributes);
        }


        public ApiResponse<MetricSchemaModel> CreateSchema(IHasUrl item, string schemaName, List<KeyValuePair<string, string>> attributes)
        {
            var url = MakeMetricSchemaCollectionUrl(item.Url);
            return PostSchema(url, schemaName, attributes);
        }


        private ApiResponse<MetricSchemaModel> PostSchema(Uri url, string schemaName, List<KeyValuePair<string, string>> attributes)
        {
            var attributesToPost = new List<KeyValuePair<string, string>>(attributes.Capacity);
            attributesToPost.AddRange(attributes);

            attributesToPost.Add(new KeyValuePair<string, string>(SCHEMA_NAME_STRING, schemaName));

            return Post<MetricSchemaModel>(url, attributesToPost);
        }


        public ApiResponse<MetricInstanceModel> CreateMetricInstance(IHasUrl item, string schemaName, List<KeyValuePair<string, string>> values)
        {
            var url = MakeMetricCollectionUrl(item.Url, schemaName);
            return Post<MetricInstanceModel>(url, values);
        }


        public ApiResponse<string> DeleteInstance(IHasUrl item, string schemaName, string instanceName)
        {
            var url = MakeMetricInstanceUrl(item.Url, schemaName, instanceName);
            return Delete(url);
        }


        public ApiResponse<string> DeleteInstance(MetricInstanceSummaryModel model)
        {
            return Delete(new Uri(model.Url));
        }


        public ApiResponse<FileModel> GetFile(string folderName, string fileName)
        {
            var url = MakeFileUrl(folderName, fileName);
            return Get<FileModel>(url);
        }


        public Uri MakeFileUrl(string fileName)
        {
            return MakeFileUrlForGetPutDelete(_url, fileName);
        }


        public Uri MakeFileUrl(string folderName, string fileName)
        {
            return MakeFileUrlForGetPutDelete(_url, folderName, fileName, FOLDERS);
        }


        public Uri MakeFieldFileUrl(string folderName, string fileName)
        {
            return MakeFieldFileUrl(_url, folderName, fileName);
        }


        public ApiResponse<FileSummaryModel[]> GetFiles(string folderName)
        {
            var url = MakeFileCollectionUrl(_url, folderName);
            return Get<FileSummaryModel[]>(url);
        }


        public ApiResponse<FileSummaryModel[]> GetFieldFiles(string folderName)
        {
            var url = MakeFieldFileCollectionUrl(_url, folderName);
            return Get<FileSummaryModel[]>(url);
        }


        public ApiResponse<FileModel> GetFile(Uri url)
        {
            return Get<FileModel>(url);
        }


        public ApiResponse<FileSummaryModel[]> GetFiles()
        {
            return Get<FileSummaryModel[]>(_url);
        }


        public ApiResponse<FileModel> GetFile(string fileName)
        {
            var url = MakeFileUrlForGetPutDelete(_url, fileName);
            return Get<FileModel>(url);
        }


        public ApiResponse<FileModel> UploadFile(string folderName, string fullFileName, string description)
        {
            using (var stream = new FileStream(fullFileName, FileMode.Open))
            {
                var fileName = Path.GetFileName(fullFileName);
                var url = MakeFileCollectionUrl(_url, folderName);
                return Post<FileModel>(url, stream, fileName, description);
            }
        }


        public ApiResponse<string> DeleteFolder(string folderName)
        {
            if (folderName == null)
                throw new ArgumentNullException("folderName", "folderName cannot be null.");

            var url = MakeFolderUrl(_url, folderName);
            return Delete(url);
        }


        public ApiResponse<string> DeleteFile(string folderName, string fileName)
        {
            var url = MakeFileUrlForGetPutDelete(_url, folderName, fileName, FOLDERS);
            return Delete(url);
        }


        public ApiResponse<string> DeleteSchema(string schemaName, ObjectType objectType)
        {
            var url = MakeMetricSchemaUrl(_url, objectType, schemaName);
            return Delete(url);
        }


        public ApiResponse<string> DeleteSchema(MetricSchemaModel model)
        {
            return Delete(new Uri(model.Url));
        }


        public ApiResponse<FolderModel> RenameFolder(string oldName, string newName)
        {
            var url = MakeFolderUrl(_url, oldName);
            return ChangeName<FolderModel>(url, newName);
        }


        public ApiResponse<FileModel> ChangeDescription(string newDescription)
        {
            return ChangeDescription<FileModel>(new Uri(_url), newDescription);
        }


        public ApiResponse<MetricSchemaModel> ChangeSchemaAttributes(string schemaName, ObjectType objectType, List<KeyValuePair<string, string>> attributes)
        {
            var url = MakeMetricSchemaUrl(_url, objectType, schemaName);
            return Put<MetricSchemaModel>(url, attributes);
        }


        public ApiResponse<MetricInstanceModel> UpdateMetricInstance(string schemaName, string metricName, ObjectType objectType, List<KeyValuePair<string, string>> attributes)
        {
            var url = MakeMetricInstanceUrl(_url, objectType, schemaName, metricName);
            return Put<MetricInstanceModel>(url, attributes);
        }


        private static Uri MakeFolderUrl(string baseUrl, string folderName)
        {
            if (folderName == null)
                return new Uri(baseUrl);

            return new Uri(baseUrl + "/" + FOLDERS + "/" + folderName);
        }


        private static Uri MakeFieldFolderUrl(string baseUrl, string folderName)
        {
            if (folderName == null)
                return new Uri(baseUrl);

            return new Uri(baseUrl + "/" + FIELDFOLDERS + "/" + folderName);
        }


        private static Uri MakeFolderCollectionUrl(string baseUrl)
        {
            return new Uri(baseUrl + "/" + FOLDERS);
        }


        private static Uri MakeFieldFolderCollectionUrl(string baseUrl)
        {
            return new Uri(baseUrl + "/" + FIELDFOLDERS);
        }


        private static Uri MakeMeasurementTypeUrl(string baseUrl)
        {
            return new Uri(baseUrl + "/" + MEASUREMENTS);
        }


        private static Uri MakeMeasurementUrl(string baseUrl, string measurementName)
        {
            return new Uri(baseUrl + "/" + MEASUREMENTS + "/" + measurementName);
        }


        private static Uri MakeFieldFileUrl(string baseUrl, string folderName, string fileName)
        {
            return MakeFileUrlForGetPutDelete(baseUrl, folderName, fileName, FIELDFOLDERS);
        }


        private static Uri MakeFileUrlForGetPutDelete(string baseUrl, string folderName, string fileName, string stem)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName", "fileName cannot be null.");

            if (folderName == null)
                return new Uri(baseUrl + "/" + FILES + "/" + fileName);

            return new Uri(baseUrl + "/" + stem  + "/" + folderName + "/" + FILES + "/" + fileName);
        }


        private static Uri MakeFileUrlForGetPutDelete(string folderUrl, string fileName)
        {
            return MakeFileUrlForGetPutDelete(folderUrl, null, fileName, FOLDERS);
        }


        private static Uri MakeFileCollectionUrl(string baseUrl, string folderName)
        {
            return MakeFileFieldFileCollectionUrl(baseUrl, folderName, FOLDERS);
        }


        private static Uri MakeFieldFileCollectionUrl(string baseUrl, string folderName)
        {
            return MakeFileFieldFileCollectionUrl(baseUrl, folderName, FIELDFOLDERS);
        }


        private static Uri MakeFileFieldFileCollectionUrl(string baseUrl, string folderName, string stem)
        {
            if(string.IsNullOrEmpty(folderName))
                return new Uri(baseUrl + "/" + FILES);

            return new Uri(baseUrl + "/" + stem + "/" + folderName + "/" + FILES);

        }


        private static Uri MakeMetricSchemaCollectionUrl(string baseUrl)
        {
            return new Uri(baseUrl + "/" + METRICSCHEMAS);
        }


        private static Uri MakeMetricSchemaCollectionUrl(string itemUrl, ObjectType level)
        {
            return new Uri(itemUrl + "/" +  level +  "/" + METRICSCHEMAS);
        }


        private static Uri MakeMetricSchemaUrl(string baseUrl, ObjectType objectType, string schemaName)
        {
            return new Uri(baseUrl + "/" + objectType +  "/" + METRICSCHEMAS + "/" + schemaName);
        }


        private static Uri MakeMetricCollectionUrl(string itemUrl, string schemaName)
        {
            return new Uri(itemUrl +  "/" + METRICSCHEMAS + "/" + schemaName + "/" + METRICS + "/");
        }


        private static Uri MakeMetricInstanceUrl(string baseUrl, ObjectType objectType, string schemaName, string metricName)
        {
            return new Uri(MakeMetricSchemaUrl(baseUrl, objectType, schemaName) + "/" + METRICS + "/" + metricName);
        }


        private static Uri MakeMetricInstanceUrl(string itemUrl, string schemaName, string metricName)
        {
            return new Uri(itemUrl +  "/" + METRICSCHEMAS + "/" + schemaName + "/" + METRICS + "/" + metricName);
        }


        private void AddAuthToken(HttpClient client)
        {
            if (AuthToken != null)
                AddAuthToken(client, AuthToken);
        }


        public static void AddAuthToken(HttpClient client, AuthResponseModel token)
        {
            if (client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Remove("Authorization");

            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token.AccessToken);
        }


        public ApiResponse<T> Get<T>()
        {
            var uri = new Uri(_url);
            return Get<T>(uri);
        }


        public ApiResponse<T> Get<T>(string objectType)
        {
            var uri = new Uri(_url + "/" + objectType);
            return Get<T>(uri);
        }


        public ApiResponse<T> Get<T>(string objectType, string itemName)
        {
            var uri = new Uri(_url + "/" + objectType + "s/" + itemName);       // Pluralize objectType to refer to the collection
            return Get<T>(uri);
        }


        public ApiResponse<T> Get<T>(string objectType, List<Tuple<string,string>> queryParams)
        {
            var queryParamString = "";
            foreach (var param in queryParams)
            {
                if (queryParamString != "")
                    queryParamString += "&";

                queryParamString += param.Item1 + "=" + param.Item2;
            }

            var uri = new Uri(_url + "/" + objectType + "s?" + queryParamString);
            return Get<T>(uri);
        }

        
        private ApiResponse<T> Get<T>(Uri url)
        {
            using (var client = new HttpClient())
            {
                AddAuthToken(client);
                _response = client.GetAsync(url).Result;
            }

            return ProcessResponse<T>(_response);
        }


        public ApiResponse<T> Post<T>(Uri url, Stream stream, string fileName, string description)
        {
            using (var form = new MultipartFormDataContent())
            {
                var len = stream.Length;
                byte[] buffer = new byte[len];

                using (var ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                form.Add(new StringContent(fileName), "name");
                form.Add(new StringContent(description), DESCRIPTION);
                form.Add(new ByteArrayContent(buffer, 0, buffer.Length), "filex", fileName);        // The token here is not important

                using (var client = new HttpClient())
                {
                    AddAuthToken(client);
                    _response = client.PostAsync(url, form).Result;
                }
                return ProcessResponse<T>(_response);
            }
        }
        

        public ApiResponse<T> Post<T>(Uri url, List<KeyValuePair<string, string>> values)
        {
            using (var client = new HttpClient())
            {
                AddAuthToken(client);
                _response = client.PostAsync(url, new FormUrlEncodedContent(values)).Result;
            }
            return ProcessResponse<T>(_response);
        }


        public ApiResponse<T> ChangeName<T>(Uri url, string newName)
        {
            return SetAttribute<T>(url, "name", newName);
        }


        public ApiResponse<FolderModel> ChangeFolderName(string currentName, string newName)
        {
            return ChangeName<FolderModel>(MakeFolderUrl(_url, currentName), newName);
        }


        public ApiResponse<MetricSchemaModel> Unlock(string schemaName, ObjectType objectType)
        {
            return SetSchemaSpecialAttribute(schemaName, objectType, SCHEMA_LOCKED_STRING, false);
        }


        public ApiResponse<MetricSchemaModel> Lock(string schemaName, ObjectType objectType)
        {
            return SetSchemaSpecialAttribute(schemaName, objectType, SCHEMA_LOCKED_STRING, true);
        }


        public ApiResponse<FolderModel> Unlock(string folderName)
        {
            return SetFolderSpecialAttribute(folderName, FOLDER_LOCKED_STRING, false);
        }


        public ApiResponse<FolderModel> Lock(string folderName)
        {
            return SetFolderSpecialAttribute(folderName, FOLDER_LOCKED_STRING, true);
        }


        public ApiResponse<MetricSchemaModel> Unpublish(string schemaName, ObjectType objectType)
        {
            return SetSchemaSpecialAttribute(schemaName, objectType, SCHEMA_PUBLISHED_STRING, false);
        }


        public ApiResponse<FolderModel> Unpublish(string folderName)
        {
            return SetFolderSpecialAttribute(folderName, FOLDER_PUBLISHED_STRING, false);
        }


        public ApiResponse<FolderModel> Publish(MetricSchemaModel model)
        {
            return SetAttribute<FolderModel>(new Uri(model.Url), SCHEMA_PUBLISHED_STRING, "true");
        }


        public ApiResponse<FolderModel> Unpublish(MetricSchemaModel model)
        {
            return SetAttribute<FolderModel>(new Uri(model.Url), SCHEMA_PUBLISHED_STRING, "false");
        }


        public ApiResponse<MetricSchemaModel> Publish(string schemaName, ObjectType objectType)
        {
            return SetSchemaSpecialAttribute(schemaName, objectType, SCHEMA_PUBLISHED_STRING, true);
        }


        public ApiResponse<FolderModel> Publish(string folderName)
        {
            return SetFolderSpecialAttribute(folderName, FOLDER_PUBLISHED_STRING, true);
        }


        private ApiResponse<MetricSchemaModel> SetSchemaSpecialAttribute(string schemaName, ObjectType objectType, string key, bool value)
        {
            var url = MakeMetricSchemaUrl(_url, objectType, schemaName);

            return SetAttribute<MetricSchemaModel>(url, key, value.ToString());
        }


        private ApiResponse<FolderModel> SetFolderSpecialAttribute(string folderName, string key, bool value)
        {
            var url = MakeFolderUrl(_url, folderName);

            return SetAttribute<FolderModel>(url, key, value.ToString());
        }


        private ApiResponse<T> ChangeDescription<T>(Uri url, string newDescription)
        {
            return SetAttribute<T>(url, DESCRIPTION, newDescription);
        }


        // Assumes helper is based on something that has folders
        public ApiResponse<FileModel> ChangeFileDescription(string folderName, string fileName, string newDescription)
        {
            return SetAttribute<FileModel>(MakeFileUrl(folderName, fileName), DESCRIPTION, newDescription);
        }


        private ApiResponse<T> SetAttribute<T>(Uri url, string key, string value)
        {
            var attributes = new List<KeyValuePair<string, string>> 
            {
                new KeyValuePair<string, string>(key, value)
            };

            return Put<T>(url, attributes);
        }


        public ApiResponse<T> Put<T>(Uri url, List<MetricAttributeModel> fields)
        {
            var flds = new List<KeyValuePair<string, string>>();

            foreach (var f in fields)
                flds.Add(new KeyValuePair<string, string>(f.Name, f.Type));

            return Put<T>(url, flds);
        }


        private ApiResponse<T> Put<T>(Uri url, List<KeyValuePair<string, string>> fields)
        {
            using (var client = new HttpClient())
            {
                AddAuthToken(client);
                var body = new FormUrlEncodedContent(fields);
                _response = client.PutAsync(url, body).Result;
            }
            return ProcessResponse<T>(_response);
        }


        private ApiResponse<string> Delete(Uri url)
        {
            using (var client = new HttpClient())
            {
                AddAuthToken(client);
                _response = client.DeleteAsync(url).Result;
            }
            return ProcessResponse<string>(_response);
        }


        private static ApiResponse<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<T>
                {
                    StatusCode = response.StatusCode
                };
            }

            return new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                Payload = DeserializeResponse<T>(response)
            };
        }


        // Following advice here: https://stackoverflow.com/questions/1329739/nested-using-statements-in-c-sharp
        private static T DeserializeResponse<T>(HttpResponseMessage response)
        {
            using (var streamReader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return new JsonSerializer().Deserialize<T>(jsonTextReader);
            }
        }
    }
}
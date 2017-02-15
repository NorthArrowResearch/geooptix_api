using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Keystone.API
{
    public class KeystoneApiHelper
    {
        private readonly string _authUrl;
        private string ClientIdentifier { get; set; }
        private string ClientSecret { get; set; }
        
        public KeystoneApiHelper(string authUrl, string clientIdentifier, string clientSecret)
        {
            _authUrl = authUrl;
            ClientIdentifier = clientIdentifier;
            ClientSecret = clientSecret;
        }

        public AuthResponseModel GetAuthToken(string username, string password)
        {
            if (_authUrl == null)
                throw new Exception("Need to specify authorization URL if you want to authorize.  Try a different constructor.");

            var attributes = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", username), 
                new KeyValuePair<string, string>("password", password), 
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", ClientIdentifier), 
                new KeyValuePair<string, string>("client_secret", ClientSecret)
            };
            
            var response = Post(attributes);

            return response;
        }

        private AuthResponseModel Post(List<KeyValuePair<string, string>> values)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_authUrl, new FormUrlEncodedContent(values)).Result;
                using (var streamReader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();

                        return serializer.Deserialize<AuthResponseModel>(jsonTextReader);
                    }
                }
            }
        }
    }
}
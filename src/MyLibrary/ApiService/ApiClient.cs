using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace MyLibrary.ApiService
{
    public abstract class ApiClient
    {
        public const string BASE_URL = "https://openlibrary.org";

        protected readonly HttpClient _client;

        public ApiClient()
        {
            this._client = new HttpClient();
        }

        protected async Task<string> GetJson(string url)
        {
            var response = await this._client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            return json;
        }
        
        public void Dispose()
        {
            this._client?.Dispose();
        }
    }//class
}

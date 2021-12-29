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

        protected async Task<HttpResponseWrapper> GetResponseBase(string url)
        {
            return new HttpResponseWrapper(await this._client.GetAsync(url));
        }

        public void Dispose()
        {
            this._client?.Dispose();
        }
    }//class
}

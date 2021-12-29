using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace MyLibrary.ApiService
{
    public class IsbnApiClient : ApiClient, IIsbnApiClient
    {
        public IsbnApiClient()
            : base()
        {

        }

        public async Task<HttpResponseWrapper> GetResponse(string isbn)
        {
            string url = BASE_URL + "/isbn/" + isbn + ".json";

            return await GetResponseBase(url);
        }
    }//class
}

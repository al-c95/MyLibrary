using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace MyLibrary.ApiService
{
    public class AuthorApiClient : ApiClient, IAuthorApiClient
    {
        public AuthorApiClient()
            : base()
        {

        }

        public async Task<HttpResponseWrapper> GetResponse(string authorKey)
        {
            string url = BASE_URL + authorKey + ".json";

            return await GetResponseBase(url);
        }
    }//class
}

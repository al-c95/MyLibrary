using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace MyLibrary.ApiService
{
    public class HttpResponseWrapper : IHttpResponseWrapper
    {
        private readonly HttpResponseMessage _response;

        public virtual HttpStatusCode StatusCode => _response.StatusCode;
        public virtual HttpContent Content => _response.Content;

        public HttpResponseWrapper() { }
        
        public HttpResponseWrapper(HttpResponseMessage response)
        {
            this._response = response;
        }

        public async virtual Task<string> ReadAsStringAsync()
        {
            return await this._response.Content.ReadAsStringAsync();
        }
    }//class
}

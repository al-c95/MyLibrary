using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace MyLibrary.ApiService
{
    public interface IHttpResponseWrapper
    {
        Task<string> ReadAsStringAsync();
        HttpStatusCode StatusCode { get; }
        HttpContent Content { get; }
    }
}

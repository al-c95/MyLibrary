using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace MyLibrary.ApiService
{
    public interface IIsbnApiClient : IDisposable
    {
        Task<HttpResponseWrapper> GetResponse(string isbn);
    }
}
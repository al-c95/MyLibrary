using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace MyLibrary.ApiService
{
    public interface IAuthorApiClient : IDisposable
    {
        Task<HttpResponseWrapper> GetResponse(string authorKey);
    }
}
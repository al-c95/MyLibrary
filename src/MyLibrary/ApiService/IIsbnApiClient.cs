using System.Threading.Tasks;
using System;

namespace MyLibrary.ApiService
{
    public interface IIsbnApiClient : IDisposable
    {
        Task<string> GetAsJson(string isbn);
    }
}
using System.Threading.Tasks;
using System;

namespace MyLibrary.ApiService
{
    public interface IAuthorApiClient : IDisposable
    {
        Task<string> GetAsJson(string authorKey);
    }
}
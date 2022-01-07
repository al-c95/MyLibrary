using MyLibrary.Models.Entities;
using System.Threading.Tasks;
using System;

namespace MyLibrary.ApiService
{
    public interface IBookApiService : IDisposable
    {
        Task<Book> GetBookByIsbnAsync(string isbn);
    }
}
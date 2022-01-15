using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface IBookService
    {
        Task Add(Book book);
        Task<bool> ExistsWithId(int id);
        Task<bool> ExistsWithIsbn(string isbn);
        Task<bool> ExistsWithLongTitle(string longTitle);
        Task<bool> ExistsWithTitle(string title);
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<int> GetIdByTitle(string title);
        Task Update(Book book);
        Task DeleteById(int id);
        Task UpdateTags(ItemTagsDto dto);
    }
}
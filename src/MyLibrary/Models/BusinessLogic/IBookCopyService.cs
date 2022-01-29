//MIT License

using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface IBookCopyService
    {
        Task Create(BookCopy copy);
        Task DeleteById(int id);
        Task<bool> ExistsWithDescription(string description);
        Task<IEnumerable<BookCopy>> GetAll();
        Task<IEnumerable<BookCopy>> GetByItemId(int itemId);
        Task Update(BookCopy copy);
    }
}
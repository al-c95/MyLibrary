using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface IAuthorService
    {
        Task Add(Author author);
        Task<bool> ExistsWithName(string name);
        Task<bool> ExistsWithName(string firstName, string lastName);
        Task<IEnumerable<Author>> GetAll();
    }
}
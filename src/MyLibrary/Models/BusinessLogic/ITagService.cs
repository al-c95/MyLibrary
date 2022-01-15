using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface ITagService
    {
        Task Add(Tag tag);
        Task DeleteByName(string name);
        Task<bool> ExistsWithName(string name);
        Task<IEnumerable<Tag>> GetAll();
    }
}
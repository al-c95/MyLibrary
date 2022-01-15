using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface IPublisherService
    {
        Task Create(Publisher publisher);
        Task<bool> Exists(string name);
        Task<IEnumerable<Publisher>> GetAll();
    }
}
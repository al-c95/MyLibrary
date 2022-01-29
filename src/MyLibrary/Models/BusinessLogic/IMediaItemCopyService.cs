//MIT License

using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface IMediaItemCopyService
    {
        Task Create(MediaItemCopy copy);
        Task DeleteById(int id);
        Task<bool> ExistsWithDescription(string description);
        Task<IEnumerable<MediaItemCopy>> GetAll();
        Task<IEnumerable<MediaItemCopy>> GetByItemId(int itemId);
        Task Update(MediaItemCopy copy);
    }
}
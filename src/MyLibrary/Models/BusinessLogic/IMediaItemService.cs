using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface IMediaItemService
    {
        Task Add(MediaItem item);
        Task DeleteById(int id);
        Task<bool> ExistsWithId(int id);
        Task<bool> ExistsWithTitle(string title);
        Task<IEnumerable<MediaItem>> GetAll();
        Task<MediaItem> GetById(int id);
        Task<IEnumerable<MediaItem>> GetByType(ItemType type);
        Task<int> GetIdByTitle(string title);
        Task Update(MediaItem item);
        Task UpdateTags(ItemTagsDto dto);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic
{
    public interface IMediaItemRepository
    {
        Task<IEnumerable<MediaItem>> GetAll();
        Task<MediaItem> GetById(int id);
        Task<bool> ItemWithIdExists(int id);
        Task<IEnumerable<MediaItem>> GetByType(ItemType type);
    }
}

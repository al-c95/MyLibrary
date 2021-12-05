using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic
{
    public class MediaItemRepository : IMediaItemRepository
    {
        private ItemDataAccessor<MediaItem> _dao;

        public MediaItemRepository()
        {
            this._dao = new MediaItemDataAccessor();
        }

        public MediaItemRepository(ItemDataAccessor<MediaItem> dataAccessor)
        {
            this._dao = dataAccessor;
        }

        public async Task<IEnumerable<MediaItem>> GetAll()
        {
            return await this._dao.ReadAll();
        }

        public async Task<MediaItem> GetById(int id)
        {
            var allItems = await GetAll();

            return allItems.FirstOrDefault(i => i.Id == id);
        }

        public async Task<bool> ItemWithIdExists(int id)
        {
            var allItems = await GetAll();

            return allItems.Any(i => i.Id == id);
        }

        public async Task<IEnumerable<MediaItem>> GetByType(ItemType type)
        {
            var allItems = await GetAll();

            var filteredItems = from i in allItems
                                where i.Type == type
                                select i;
            return filteredItems;
        }//GetByType
    }//class
}

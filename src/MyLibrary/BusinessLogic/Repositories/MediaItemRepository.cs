using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic.Repositories
{
    public class MediaItemRepository : ItemRepository<MediaItem>
    {
        // ctor
        public MediaItemRepository()
            :base(new MediaItemDataAccessor())
        {

        }

        public MediaItemRepository(ItemDataAccessor<MediaItem> dataAccessor)
            : base(dataAccessor)
        {

        }

        public async Task<IEnumerable<MediaItem>> GetByType(ItemType type)
        {
            var allItems = await GetAll();

            var filteredItems = from i in allItems
                                where i.Type == type
                                select i;
            return filteredItems;
        }
    }//class
}

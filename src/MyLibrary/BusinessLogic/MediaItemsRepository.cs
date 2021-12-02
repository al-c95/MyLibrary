﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic
{
    public class MediaItemRepository
    {
        private MediaItemDataAccessor _dao;

        public MediaItemRepository(MediaItemDataAccessor dataAccessor)
        {
            this._dao = dataAccessor;
        }

        public async Task<IEnumerable<MediaItem>> GetAll()
        {
            return await this._dao.ReadAll();
        }

        public async Task<IEnumerable<MediaItem>> GetById(int id)
        {
            var allItems = await GetAll();

            return allItems.Where(i => i.Id == id);
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

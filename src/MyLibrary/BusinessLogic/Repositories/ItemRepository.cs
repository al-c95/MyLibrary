using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic.Repositories
{
    public class ItemRepository<T> where T : Item
    {
        protected ItemDataAccessor<T> _dao;

        public ItemRepository(ItemDataAccessor<T> dataAccessor)
        {
            this._dao = dataAccessor;
        }

        public async Task Create(T item)
        {
            await this._dao.Create(item);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this._dao.ReadAll();
        }

        public async Task<T> GetById(int id)
        {
            IEnumerable<T> allItems = await GetAll();

            return allItems.FirstOrDefault(i => i.Id == id);
        }

        public async Task Update(T item)
        {
            await this._dao.Update(item);
        }

        public async Task AssociateTag(T item, Tag tag)
        {
            await this._dao.AssociateExistingTag(item, tag);
        }

        public async Task RemoveTag(T item, Tag toRemove)
        {
            await this._dao.RemoveTag(item, toRemove);
        }

        public async Task DeleteById(int id)
        {
            await this._dao.DeleteById(id);
        }

        public async Task<bool> ExistsWithId(int id)
        {
            IEnumerable<T> allItems = await GetAll();

            return allItems.Any(i => i.Id == id);
        }

        public virtual async Task<bool> ExistsWithTitle(string title)
        {
            IEnumerable<T> allItems = await GetAll();

            return allItems.Any(i => i.Title == title);
        }
    }//class
}

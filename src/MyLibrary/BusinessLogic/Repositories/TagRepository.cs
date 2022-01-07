using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic.Repositories
{
    public class TagRepository
    {
        private ITagDataAccessor _dao;

        // ctor
        public TagRepository()
        {
            this._dao = new TagDataAccessor();
        }

        // ctor
        public TagRepository(ITagDataAccessor dataAccessor)
        {
            this._dao = dataAccessor;
        }

        public async virtual Task Create(Tag tag)
        {
            await Task.Run(() => this._dao.Create(tag));
        }

        public async virtual Task<IEnumerable<Tag>> GetAll()
        {
            return await Task.Run(() => this._dao.ReadAll());
        }

        public async virtual Task DeleteByName(string name)
        {
            await Task.Run(() => this._dao.DeleteByName(name));
        }

        public async virtual Task<bool> ExistsWithName(string name)
        {
            var allTags = await GetAll();

            return allTags.Any(t => t.Name == name);
        }//TagWithNameExists
    }//class
}

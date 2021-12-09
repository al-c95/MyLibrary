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

        public async Task Create(Tag tag)
        {
            await this._dao.Create(tag);
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await this._dao.ReadAll();
        }

        public async Task DeleteByName(string name)
        {
            await this._dao.DeleteByName(name);
        }

        public async Task<bool> ExistsWithName(string name)
        {
            var allTags = await GetAll();

            return allTags.Any(t => t.Name == name);
        }//TagWithNameExists
    }//class
}

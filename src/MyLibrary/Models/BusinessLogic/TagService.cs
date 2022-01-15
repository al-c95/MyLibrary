using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;

namespace MyLibrary.Models.BusinessLogic
{
    public class TagService : ITagService
    {
        public TagService()
        {

        }

        public async virtual Task<IEnumerable<Tag>> GetAll()
        {
            IEnumerable<Tag> allTags = null;
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                TagRepository repo = new TagRepository(uow);
                allTags = repo.ReadAll();
                uow.Dispose();
            });

            return allTags;
        }

        public async Task<bool> ExistsWithName(string name)
        {
            var allTags = await GetAll();
            return allTags.Any(t => t.Name.Equals(name));
        }

        public async virtual Task Add(Tag tag)
        {
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                TagRepository repo = new TagRepository(uow);
                repo.Create(tag);
                uow.Dispose();
            });
        }

        public async virtual Task DeleteByName(string name)
        {
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                TagRepository repo = new TagRepository(uow);
                repo.DeleteByName(name);
                uow.Dispose();
            });
        }//DeleteByName
    }//class
}

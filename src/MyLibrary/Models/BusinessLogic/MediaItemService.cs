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
    public class MediaItemService : IMediaItemService
    {
        public MediaItemService() { }

        public async virtual Task<IEnumerable<MediaItem>> GetAll()
        {
            IEnumerable<MediaItem> allItems = null;
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                MediaItemRepository repo = new MediaItemRepository(uow);
                allItems = repo.ReadAll();
                uow.Dispose();
            });

            return allItems;
        }

        public async Task<IEnumerable<MediaItem>> GetByType(ItemType type)
        {
            var allItems = await GetAll();

            var filteredItems = from i in allItems
                                where i.Type == type
                                select i;
            return filteredItems;
        }

        public async Task<MediaItem> GetById(int id)
        {
            var allItems = await GetAll();
            return allItems.FirstOrDefault(i => i.Id == id);
        }

        public async Task<int> GetIdByTitle(string title)
        {
            var allItems = await GetAll();
            return allItems.FirstOrDefault(i => i.Title.Equals(title)).Id;
        }

        public async Task<bool> ExistsWithId(int id)
        {
            var allItems = await GetAll();
            return allItems.Any(i => i.Id == id);
        }

        public async Task<bool> ExistsWithTitle(string title)
        {
            var allItems = await GetAll();
            return allItems.Any(i => i.Title.Equals(title));
        }

        public async virtual Task Add(MediaItem item)
        {
            await Task.Run(() =>
            {
                // begin transaction
                UnitOfWork uow = new UnitOfWork();
                MediaItemRepository itemRepo = new MediaItemRepository(uow);
                TagRepository tagRepo = new TagRepository(uow);
                uow.Begin();

                // insert Media table record
                itemRepo.Create(item);

                // handle tags
                // get all tag Ids
                List<int> tagIds = new List<int>();
                foreach (var tag in item.Tags)
                {
                    if (tagRepo.ExistsWithName(tag.Name))
                    {
                        // tag exists
                        // get the Id
                        int tagId = tagRepo.GetIdByName(tag.Name);
                        tagIds.Add(tagId);
                    }
                    else
                    {
                        // tag does not exist
                        // insert tag
                        tagRepo.Create(tag);
                        // get the Id
                        int tagId = tagRepo.GetIdByName(tag.Name);
                        tagIds.Add(tagId);
                    }
                }
                // insert record(s) in Media_Tag link table
                int itemId = itemRepo.GetIdByTitle(item.Title);
                foreach (int tagId in tagIds)
                {
                    tagRepo.LinkMediaItem(itemId, tagId);
                }

                // commit transaction
                uow.Commit();
            });
        }

        public async virtual Task DeleteById(int id)
        {
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                MediaItemRepository repo = new MediaItemRepository(uow);
                repo.DeleteById(id);
                uow.Dispose();
            });
        }

        public async virtual Task Update(MediaItem item)
        {
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                MediaItemRepository repo = new MediaItemRepository(uow);
                repo.Update(item);
                uow.Dispose();
            });
        }

        public async virtual Task UpdateTags(ItemTagsDto dto)
        {
            await Task.Run(() =>
            {
                // begin transaction
                UnitOfWork uow = new UnitOfWork();
                MediaItemRepository itemRepo = new MediaItemRepository(uow);
                TagRepository tagRepo = new TagRepository(uow);
                uow.Begin();

                // add tags
                foreach (var tag in dto.TagsToAdd)
                {
                    if (tagRepo.ExistsWithName(tag))
                    {
                        // tag exists
                        // get the Id
                        int tagId = tagRepo.GetIdByName(tag);
                        // insert record into link table
                        tagRepo.LinkMediaItem(dto.Id, tagId);
                    }
                    else
                    {
                        // tag does not exist
                        // insert it
                        tagRepo.Create(new Tag { Name = tag });
                        // get the id
                        int tagId = tagRepo.GetIdByName(tag);
                        // insert record into link table
                        tagRepo.LinkMediaItem(dto.Id, tagId);
                    }
                }

                // remove tags
                foreach (var tag in dto.TagsToRemove)
                {
                    if (tagRepo.ExistsWithName(tag))
                    {
                        // tag exists
                        // get the id
                        int tagId = tagRepo.GetIdByName(tag);
                        // delete record from link table
                        tagRepo.UnlinkMediaItem(dto.Id, tagId);
                    }
                }

                // commit transaction
                uow.Commit();
            });
        }
    }//class
}

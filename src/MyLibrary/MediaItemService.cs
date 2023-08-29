//MIT License

//Copyright (c) 2021-2023

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary.Models.BusinessLogic
{
    public class MediaItemService : ServiceBase, IMediaItemService
    {
        protected IMediaItemRepositoryProvider _repoProvider;
        protected ITagRepositoryServiceProvider _tagRepoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MediaItemService()
            :base()
        {
            this._repoProvider = new MediaItemRepositoryProvider();
            this._tagRepoProvider = new TagRepositoryServiceProvider();
        }

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="uowProvider"></param>
        /// <param name="repoProvider"></param>
        public MediaItemService(IUnitOfWorkProvider uowProvider, IMediaItemRepositoryProvider repoProvider, ITagRepositoryServiceProvider tagRepoProvider)
            :base(uowProvider)
        {
            this._repoProvider = repoProvider;
            this._tagRepoProvider = tagRepoProvider;
        }

        public async virtual Task<IEnumerable<MediaItem>> GetAllAsync()
        {
            using (var uow = this._uowProvider.Get())
            {
                IMediaItemRepository repo = this._repoProvider.Get(uow);
                return await repo.ReadAllAsync();
            }
        }

        public async Task<IEnumerable<MediaItem>> GetByTypeAsync(ItemType type)
        {
            var allItems = await GetAllAsync();

            var filteredItems = from i in allItems
                                where i.Type == type
                                select i;
            return filteredItems;
        }

        public async Task<MediaItem> GetByIdAsync(int id)
        {
            using (var uow = this._uowProvider.Get())
            {
                IMediaItemRepository repo = this._repoProvider.Get(uow);
                return await repo.GetByIdAsync(id);
            }
        }

        public async Task<int> GetIdByTitleAsync(string title)
        {
            var allItems = await GetAllAsync();
            return allItems.FirstOrDefault(i => i.Title.Equals(title)).Id;
        }

        public async Task<bool> ExistsWithIdAsync(int id)
        {
            var allItems = await GetAllAsync();
            return allItems.Any(i => i.Id == id);
        }

        public async Task<bool> ExistsWithTitleAsync(string title)
        {
            using (var uow = this._uowProvider.Get())
            {
                IMediaItemRepository repo = this._repoProvider.Get(uow);
                return await repo.ExistsWithTitleAsync(title);
            }
        }

        public async Task<bool> AddIfNotExistsAsync(MediaItem item)
        {
            if (await ExistsWithTitleAsync(item.Title))
            {
                return false;
            }
            else
            {
                await AddAsync(item);

                return true;
            }
        }

        public async Task Add(MediaItem item)
        {
            // begin transaction
            IUnitOfWork uow = this._uowProvider.Get();
            IMediaItemRepository itemRepo = this._repoProvider.Get(uow);
            ITagRepository tagRepo = this._tagRepoProvider.Get(uow);
            uow.Begin();

            // insert Media table record
            await itemRepo.CreateAsync(item);

            // handle tags
            // get all tag Ids
            List<int> tagIds = new List<int>();
            foreach (var tag in item.Tags)
            {
                if (await tagRepo.ExistsWithNameAsync(tag.Name))
                {
                    // tag exists
                    // get the Id
                    int tagId = await tagRepo.GetIdByNameAsync(tag.Name);
                    tagIds.Add(tagId);
                }
                else
                {
                    // tag does not exist
                    // insert tag
                    await tagRepo.CreateAsync(tag);
                    // get the Id
                    int tagId = await tagRepo.GetIdByNameAsync(tag.Name);
                    tagIds.Add(tagId);
                }
            }
            // insert record(s) in Media_Tag link table
            int itemId = await itemRepo.GetIdByTitleAsync(item.Title);
            foreach (int tagId in tagIds)
            {
                await tagRepo.LinkMediaItemAsync(itemId, tagId);
            }

            // commit transaction
            uow.Commit();
        }

        public async Task AddAsync(MediaItem item)
        {
            await Add(item);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() =>
            {
                // begin transaction
                IUnitOfWork uow = this._uowProvider.Get();
                IMediaItemRepository repo = this._repoProvider.Get(uow);
                uow.Begin();
                
                // do the work
                repo.DeleteByIdAsync(id);

                // commit transaction
                uow.Commit();
            });
        }

        public void Update(MediaItem item, bool includeImage)
        {
            // begin transaction
            IUnitOfWork uow = this._uowProvider.Get();
            IMediaItemRepository repo = this._repoProvider.Get(uow);
            uow.Begin();

            // do the work
            repo.UpdateAsync(item, includeImage);

            // commit transaction
            uow.Commit();
        }

        public async Task UpdateAsync(MediaItem item, bool includeImage)
        {
            await Task.Run(() =>
            {
                Update(item, includeImage);
            });
        }

        public async Task UpdateTags(ItemTagsDto dto)
        {
            // begin transaction
            IUnitOfWork uow = this._uowProvider.Get();
            IMediaItemRepository itemRepo = this._repoProvider.Get(uow);
            ITagRepository tagRepo = this._tagRepoProvider.Get(uow);
            uow.Begin();

            // add tags
            foreach (var tag in dto.TagsToAdd)
            {
                if (await tagRepo.ExistsWithNameAsync(tag))
                {
                    // tag exists
                    // get the Id
                    int tagId = await tagRepo.GetIdByNameAsync(tag);
                    // insert record into link table
                    await tagRepo.LinkMediaItemAsync(dto.Id, tagId);
                }
                else
                {
                    // tag does not exist
                    // insert it
                    await tagRepo.CreateAsync(new Tag { Name = tag });
                    // get the id
                    int tagId = await tagRepo.GetIdByNameAsync(tag);
                    // insert record into link table
                    await tagRepo.LinkMediaItemAsync(dto.Id, tagId);
                }
            }

            // remove tags
            foreach (var tag in dto.TagsToRemove)
            {
                if (await tagRepo.ExistsWithNameAsync(tag))
                {
                    // tag exists
                    // get the id
                    int tagId = await tagRepo.GetIdByNameAsync(tag);
                    // delete record from link table
                    await tagRepo.UnlinkMediaItemAsync(dto.Id, tagId);
                }
            }

            // commit transaction
            uow.Commit();
        }

        public async Task UpdateTagsAsync(ItemTagsDto dto)
        {
            await Task.Run(() =>
            {
                UpdateTags(dto);
            });
        }//UpdateTagsAsync
    }//class
}
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
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary.Models.BusinessLogic
{
    public class MediaItemCopyService : ServiceBase, IMediaItemCopyService
    {
        protected IMediaItemCopyRepositoryProvider _repoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MediaItemCopyService()
            : base()
        {
            this._repoProvider = new MediaItemCopyRepositoryProvider();
        }

        public MediaItemCopyService(IUnitOfWorkProvider uowProvider, IMediaItemCopyRepositoryProvider repoProvider)
            : base(uowProvider)
        {
            this._repoProvider = repoProvider;
        }

        public async Task Create(MediaItemCopy copy)
        {
            using (var uow = this._uowProvider.Get())
            {
                IMediaItemCopyRepository repo = this._repoProvider.Get(uow);
                await repo.CreateAsync(copy);
            }
        }

        public async virtual Task<IEnumerable<MediaItemCopy>> GetAll()
        {
            using (var uow = this._uowProvider.Get())
            {
                IMediaItemCopyRepository repo = this._repoProvider.Get(uow);
                return await repo.ReadAllAsync();
            }
        }

        public async Task<IEnumerable<MediaItemCopy>> GetByItemId(int itemId)
        {
            var allCopies = await GetAll();

            return allCopies.Where(c => c.MediaItemId == itemId);
        }

        public async Task DeleteById(int id)
        {
            using (var uow = this._uowProvider.Get())
            {
                IMediaItemCopyRepository repo = this._repoProvider.Get(uow);
                await repo.DeleteByIdAsync(id);
            }
        }

        public async Task Update(MediaItemCopy copy)
        {
            using (var uow = this._uowProvider.Get())
            {
                IMediaItemCopyRepository repo = this._repoProvider.Get(uow);
                await repo.UpdateAsync(copy);
            }
        }

        public async Task<bool> ExistsWithDescription(string description)
        {
            var allCopies = await GetAll();
            return allCopies.Any(c => c.Description.Equals(description));
        }
    }//class
}

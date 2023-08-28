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
    public class WishlistService : IWishlistService
    {
        protected IUnitOfWorkProvider _uowProvider;
        protected IWishlistRepositoryProvider _repoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WishlistService()
        {
            this._uowProvider = new UnitOfWorkProvider();
            this._repoProvider = new WishlistRepositoryProvider();
        }

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="uowProvider"></param>
        public WishlistService(IUnitOfWorkProvider uowProvider, IWishlistRepositoryProvider repoProvider)
        {
            this._uowProvider = uowProvider;
            this._repoProvider = repoProvider;
        }

        public async virtual Task Add(WishlistItem item)
        {
            using (var uow = this._uowProvider.Get())
            {
                IWishlistRepository repo = this._repoProvider.Get(uow);
                await repo.CreateAsync(item);
            }
        }

        public async virtual Task<IEnumerable<WishlistItem>> GetAll()
        {
            using (var uow = this._uowProvider.Get())
            {
                IWishlistRepository repo = this._repoProvider.Get(uow);
                return await repo.ReadAllAsync();
            }
        }

        public async Task<IEnumerable<WishlistItem>> GetByType(ItemType type)
        {
            var allItems = await GetAll();

            var filteredItems = from i in allItems
                                where i.Type == type
                                select i;
            return filteredItems;
        }

        public async Task<bool> ExistsWithTitle(string title)
        {
            using (var uow = this._uowProvider.Get())
            {
                IWishlistRepository repo = this._repoProvider.Get(uow);
                return await repo.ExistsWithTitleAsync(title);
            }
        }

        public async Task<bool> ExistsWithId(int id)
        {
            var allItems = await GetAll();
            return allItems.Any(i => i.Id == id);
        }

        public async Task Update(WishlistItem item, bool includeImage)
        {
            using (var uow = this._uowProvider.Get())
            {
                IWishlistRepository repo = this._repoProvider.Get(uow);
                await repo.UpdateAsync(item,includeImage);
            }
        }

        public async Task DeleteById(int id)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IWishlistRepository repo = this._repoProvider.Get(uow);
                repo.DeleteByIdAsync(id);
                uow.Dispose();
            });
        }
    }//class
}

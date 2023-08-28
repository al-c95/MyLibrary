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
    public class BookCopyService : ServiceBase, IBookCopyService
    {
        protected readonly IBookCopyRepositoryProvider _repoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BookCopyService()
            : base()
        {
            this._repoProvider = new BookCopyRepositoryProvider();
        }

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="uowProvider"></param>
        /// <param name="repoProvider"></param>
        public BookCopyService(IUnitOfWorkProvider uowProvider, IBookCopyRepositoryProvider repoProvider)
            : base(uowProvider)
        {
            this._repoProvider = repoProvider;
        }

        public async Task Create(BookCopy copy)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookCopyRepository repo = this._repoProvider.Get(uow);
                repo.CreateAsync(copy);
                uow.Dispose();
            });
        }

        public async virtual Task<IEnumerable<BookCopy>> GetAll()
        {
            using (var uow = this._uowProvider.Get())
            {
                IBookCopyRepository repo = this._repoProvider.Get(uow);
                return await repo.ReadAllAsync();
            }
        }

        public async Task<IEnumerable<BookCopy>> GetByItemId(int itemId)
        {
            var allCopies = await GetAll();

            return allCopies.Where(c => c.BookId == itemId);
        }

        public async Task DeleteById(int id)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookCopyRepository repo = this._repoProvider.Get(uow);
                repo.DeleteByIdAsync(id);
                uow.Dispose();
            });
        }

        public async Task Update(BookCopy copy)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookCopyRepository repo = this._repoProvider.Get(uow);
                repo.UpdateAsync(copy);
                uow.Dispose();
            });
        }

        public async Task<bool> ExistsWithDescription(string description)
        {
            var allCopies = await GetAll();
            return allCopies.Any(c => c.Description.Equals(description));
        }
    }//class
}

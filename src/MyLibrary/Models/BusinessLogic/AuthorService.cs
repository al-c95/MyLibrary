//MIT License

//Copyright (c) 2021

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary.Models.BusinessLogic
{
    public class AuthorService : IAuthorService
    {
        protected readonly IUnitOfWorkProvider _uowProvider;
        protected readonly IAuthorRepositoryProvider _repoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AuthorService() 
        {
            this._uowProvider = new UnitOfWorkProvider();
            this._repoProvider = new AuthorRepositoryProvider();
        }

        /// <summary>
        /// Constructor with service providers dependency injection.
        /// </summary>
        /// <param name="uowProvider"></param>
        public AuthorService(IUnitOfWorkProvider uowProvider, IAuthorRepositoryProvider repoProvider)
        {
            this._uowProvider = uowProvider;
            this._repoProvider = repoProvider;
        }

        public async virtual Task<IEnumerable<Author>> GetAll()
        {
            IEnumerable<Author> allAuthors = null;
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IAuthorRepository repo = this._repoProvider.Get(uow);
                allAuthors = repo.ReadAll();
                uow.Dispose();
            });

            return allAuthors;
        }

        public async Task<bool> ExistsWithName(string name)
        {
            var allAuthors = await GetAll();
            return allAuthors.Any(a => a.FirstName.Equals(name) || a.LastName.Equals(name));
        }

        public async Task<bool> ExistsWithName(string firstName, string lastName)
        {
            var allAuthors = await GetAll();
            return allAuthors.Any(a => a.FirstName.Equals(firstName) && a.LastName.Equals(lastName));
        }

        public async Task Add(Author author)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IAuthorRepository repo = this._repoProvider.Get(uow);
                repo.Create(author);
                uow.Dispose();
            });
        }
    }//class
}

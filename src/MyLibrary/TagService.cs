﻿//MIT License

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
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary
{
    public class TagService : ServiceBase, ITagService
    {
        protected readonly ITagRepositoryServiceProvider _repoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TagService()
            :base()
        {
            this._repoProvider = new TagRepositoryServiceProvider();
        }

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="uowProvider"></param>
        /// <param name="repoProvider"></param>
        public TagService(IUnitOfWorkProvider uowProvider, ITagRepositoryServiceProvider repoProvider)
            :base(uowProvider)
        {
            this._repoProvider = repoProvider;
        }

        public async virtual Task<IEnumerable<Tag>> GetAll()
        {
            using (var uow = this._uowProvider.Get())
            {
                ITagRepository repo = this._repoProvider.Get(uow);
                return await repo.ReadAllAsync();
            }
        }

        public async Task<bool> ExistsWithName(string name)
        {
            using (var uow = this._uowProvider.Get())
            {
                ITagRepository repo = this._repoProvider.Get(uow);
                return await repo.ExistsWithNameAsync(name);
            }
        }

        public async Task Add(Tag entity)
        {
            using (var uow = this._uowProvider.Get())
            {
                ITagRepository repo = this._repoProvider.Get(uow);
                await repo.CreateAsync(entity);
            }
        }

        public async Task<bool> AddIfNotExists(Tag entity)
        {
            bool exists = await ExistsWithName(entity.Name);
            if (!exists)
            {
                await Add(entity);
            }

            return !exists;
        }

        public async Task DeleteByName(string name)
        {
            using (var uow = this._uowProvider.Get())
            {
                ITagRepository repo = this._repoProvider.Get(uow);
                await repo.DeleteByNameAsync(name);
            }
        }//DeleteByName
    }//class
}

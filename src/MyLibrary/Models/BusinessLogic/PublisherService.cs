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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary.Models.BusinessLogic
{
    public class PublisherService : ServiceBase, IPublisherService
    {
        protected readonly IPublisherRepositoryProvider _repoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PublisherService()
            :base()
        {
            this._repoProvider = new PublisherRepositoryProvider();
        }

        public PublisherService(IUnitOfWorkProvider uowProvider, IPublisherRepositoryProvider repoProvider)
            :base(uowProvider)
        {
            this._repoProvider = repoProvider;
        }

        public async virtual Task<IEnumerable<Publisher>> GetAll()
        {
            using (var uow = this._uowProvider.Get())
            {
                IPublisherRepository repo = this._repoProvider.Get(uow);
                return await repo.ReadAllAsync();
            }
        }

        public async Task<Boolean> ExistsWithName(string name)
        {
            var allPublishers = await GetAll();
            return allPublishers.Any(p => p.Name.Equals(name));
        }

        public async Task Add(Publisher entity)
        {
            using (var uow = this._uowProvider.Get())
            {
                IPublisherRepository repo = this._repoProvider.Get(uow);
                await repo.CreateAsync(entity);
            }
        }
    }//class
}

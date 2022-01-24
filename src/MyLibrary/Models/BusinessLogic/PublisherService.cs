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
    public class PublisherService : IPublisherService
    {
        protected readonly IUnitOfWorkProvider _uowProvider;
        protected readonly IPublisherRepositoryProvider _repoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PublisherService() { }

        public PublisherService(IUnitOfWorkProvider uowProvider, IPublisherRepositoryProvider repoProvider)
        {
            this._uowProvider = uowProvider;
            this._repoProvider = repoProvider;
        }

        public async virtual Task<IEnumerable<Publisher>> GetAll()
        {
            IEnumerable<Publisher> allPublishers = null;
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IPublisherRepository repo = this._repoProvider.Get(uow);
                allPublishers = repo.ReadAll();
                uow.Dispose();
            });

            return allPublishers;
        }

        public async Task<Boolean> Exists(string name)
        {
            var allPublishers = await GetAll();
            return allPublishers.Any(p => p.Name.Equals(name));
        }

        public async Task Create(Publisher publisher)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IPublisherRepository repo = this._repoProvider.Get(uow);
                repo.Create(publisher);
                uow.Dispose();
            });
        }
    }//class
}

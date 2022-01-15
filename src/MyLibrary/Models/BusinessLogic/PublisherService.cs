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
    public class PublisherService : IPublisherService
    {
        public PublisherService() { }

        public async virtual Task<IEnumerable<Publisher>> GetAll()
        {
            IEnumerable<Publisher> allPublishers = null;
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                PublisherRepository repo = new PublisherRepository(uow);
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

        public async virtual Task Create(Publisher publisher)
        {
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                PublisherRepository repo = new PublisherRepository(uow);
                repo.Create(publisher);
                uow.Dispose();
            });
        }
    }//class
}

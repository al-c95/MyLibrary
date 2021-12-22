using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic.Repositories
{
    public class PublisherRepository
    {
        protected IPublisherDataAccessor _dao;

        //ctor
        public PublisherRepository()
        {
            this._dao = new PublisherDataAccessor();
        }

        //ctor
        public PublisherRepository(IPublisherDataAccessor dataAccessor)
        {
            this._dao = dataAccessor;
        }

        public async Task Create(Publisher publisher)
        {
            await Task.Run(() => this._dao.Create(publisher));
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await Task.Run(() => this._dao.ReadAll());
        }

        public async Task<bool> Exists(string name)
        {
            IEnumerable<Publisher> allPublishers = await this._dao.ReadAll();

            return allPublishers.Any(p => p.Name == name);
        }
    }//class
}

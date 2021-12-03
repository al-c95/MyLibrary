using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic
{
    public class BookRepository : IBookRepository
    {
        private IBookDataAccessor _dao;

        public BookRepository(IBookDataAccessor dataAccessor)
        {
            this._dao = dataAccessor;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await this._dao.ReadAll();
        }

        public async Task<Book> GetById(int id)
        {
            var allItems = await GetAll();

            return allItems.FirstOrDefault(i => i.Id == id);
        }

        public async Task<bool> ItemWithIdExists(int id)
        {
            var allItems = await GetAll();

            return allItems.Any(i => i.Id == id);
        }//ItemWithIdExists
    }//class
}

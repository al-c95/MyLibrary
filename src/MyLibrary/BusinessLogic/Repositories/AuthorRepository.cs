using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic.Repositories
{
    public class AuthorRepository
    {
        protected IAuthorDataAccessor _dao;

        //ctor
        public AuthorRepository()
        {
            this._dao = new AuthorDataAccessor();
        }

        //ctor
        public AuthorRepository(IAuthorDataAccessor dataAccessor)
        {
            this._dao = dataAccessor;
        }

        public async virtual Task Create(Author author)
        {
            await Task.Run(() => this._dao.Create(author));
        }

        public async virtual Task<IEnumerable<Author>> GetAll()
        {
            return await Task.Run(() => this._dao.ReadAll());
        }

        public async virtual Task<bool> ExistsWithName(string firstName, string lastName)
        {
            IEnumerable<Author> allAuthors = await GetAll();

            return allAuthors.Any(a => (a.FirstName == firstName && a.LastName == lastName));
        }

        public async virtual Task<bool> ExistsWithName(string name)
        {
            IEnumerable<Author> allAuthors = await GetAll();

            return allAuthors.Any(a => (a.FirstName == name || a.LastName == name));
        }
    }//class
}

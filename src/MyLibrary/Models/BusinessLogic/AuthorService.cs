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
    public class AuthorService : IAuthorService
    {
        public AuthorService() { }

        public async virtual Task<IEnumerable<Author>> GetAll()
        {
            IEnumerable<Author> allAuthors = null;
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                AuthorRepository repo = new AuthorRepository(uow);
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

        public async virtual Task Add(Author author)
        {
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                AuthorRepository repo = new AuthorRepository(uow);
                repo.Create(author);
                uow.Dispose();
            });
        }
    }//class
}

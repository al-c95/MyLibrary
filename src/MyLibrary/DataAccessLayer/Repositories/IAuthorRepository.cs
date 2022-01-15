using MyLibrary.Models.Entities;
using System.Collections.Generic;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public interface IAuthorRepository
    {
        bool AuthorExists(string firstName, string lastName);
        void Create(Author entity);
        int GetIdByName(string firstName, string lastName);
        void LinkBook(int bookId, int authorId);
        IEnumerable<Author> ReadAll();
    }
}
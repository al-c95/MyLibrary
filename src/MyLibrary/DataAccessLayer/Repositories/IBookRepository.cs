using MyLibrary.Models.Entities;
using System.Collections.Generic;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public interface IBookRepository
    {
        void Create(Book entity);
        void DeleteById(int id);
        IEnumerable<Book> ReadAll();
        void Update(Book toUpdate);
    }
}
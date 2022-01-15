using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(IUnitOfWork uow)
            : base(uow)
        {

        }

        public override void Create(Author entity)
        {
            const string SQL = "INSERT INTO Authors(firstName,lastName) " +
                "VALUES (@firstName,@lastName);";

            this._uow.Connection.Execute(SQL, new
            {
                entity.FirstName,
                entity.LastName
            });
        }

        public override IEnumerable<Author> ReadAll()
        {
            const string SQL = "SELECT * FROM Authors;";

            return this._uow.Connection.Query<Author>(SQL);
        }

        public bool AuthorExists(string firstName, string lastName)
        {
            return this._uow.Connection.ExecuteScalar<bool>("SELECT COUNT(1) FROM Authors WHERE firstName=@firstName AND lastName=@lastName", new
            {
                firstName = firstName,
                lastName = lastName
            });
        }//AuthorExists

        public int GetIdByName(string firstName, string lastName)
        {
            return this._uow.Connection.QuerySingle<int>("SELECT id FROM Authors WHERE firstName=@firstName AND lastName=@lastName", new
            {
                firstName = firstName,
                lastName = lastName
            });
        }//GetIdByName

        public void LinkBook(int bookId, int authorId)
        {
            const string INSERT_BOOK_AUTHOR_SQL = "INSERT INTO Book_Author(bookId,authorId) VALUES(@bookId,@authorId)";
            this._uow.Connection.Execute(INSERT_BOOK_AUTHOR_SQL, new
            {
                bookId = bookId,
                authorId = authorId
            });
        }//LinkBook
    }//class
}

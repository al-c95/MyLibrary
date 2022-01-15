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

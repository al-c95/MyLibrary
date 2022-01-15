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
using Dapper;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class BookRepository : ItemRepository<Book>, IBookRepository
    {
        public BookRepository(IUnitOfWork uow)
            : base(uow)
        {

        }

        public override void Create(Book entity)
        {
            const string INSERT_BOOK_SQL = "INSERT INTO Books(title,titleLong,isbn,isbn13,deweyDecimal,publisherId,format,language,datePublished,placeOfPublication,edition,pages,dimensions,overview,image,msrp,excerpt,synopsys,notes) " +
                            "VALUES(@title,@titleLong,@isbn,@isbn13,@deweyDecimal,@publisherId,@format,@language,@datePublished,@placeOfPublication,@edition,@pages,@dimensions,@overview,@image,@msrp,@excerpt,@synopsys,@notes);";
            this._uow.Connection.Execute(INSERT_BOOK_SQL, new
            {
                title = entity.Title,
                titleLong = entity.TitleLong,
                isbn = entity.Isbn,
                isbn13 = entity.Isbn13,
                deweyDecimal = entity.DeweyDecimal,
                publisherId = entity.Publisher.Id,
                format = entity.Format,
                language = entity.Language,
                datePublished = entity.DatePublished,
                placeOfPublication = entity.PlaceOfPublication,
                edition = entity.Edition,
                pages = entity.Pages,
                dimensions = entity.Dimensions,
                overview = entity.Overview,
                image = entity.Image,
                msrp = entity.Msrp,
                excerpt = entity.Excerpt,
                synopsys = entity.Synopsys,
                notes = entity.Notes
            });
        }

        /// <summary>
        /// Delete a book record by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override void DeleteById(int id)
        {
            const string SQL = "DELETE FROM Books WHERE id = @id;";

            this._uow.Connection.ExecuteAsync(SQL, new { id });
        }

        /// <summary>
        /// Retrieve all Books from the database, including linked entities.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Book> ReadAll()
        {
            var authorsSql = "SELECT A.*, B.id AS BookId FROM Books as B " +
                         "INNER JOIN Book_Author AS B2A ON B.id = B2A.bookId " +
                         "INNER JOIN Authors A ON B2A.authorId = A.id;";
            var allAuthorsWithBookId = this._uow.Connection.Query<dynamic>(authorsSql);

            var tagsSql = "SELECT T.*, B.id AS BookId FROM Books as B " +
                          "INNER JOIN Book_Tag AS B2T ON B.id = B2T.bookId " +
                          "INNER JOIN Tags T ON B2T.tagId = T.id;";
            var allTagsWithBookId = this._uow.Connection.Query<dynamic>(tagsSql);

            var sql = "SELECT * FROM Books as B " +
                      "INNER JOIN Publishers AS P ON B.publisherId = P.id;";
            var allBooks = this._uow.Connection.Query<Book, Publisher, Book>(sql, (book, publisher) =>
            {
                book.Publisher = publisher;
                book.Authors = allAuthorsWithBookId.Where(row => row.BookId == (int)book.Id).Select(row => new Author { Id = (int)row.id, FirstName = row.firstName, LastName = row.lastName }).AsList();
                book.Tags = allTagsWithBookId.Where(row => (int)row.BookId == book.Id).Select(row => new Tag { Id = (int)row.id, Name = row.name }).AsList();
                return book;
            });

            return allBooks;
        }

        public int GetIdByTitle(string title)
        {
            const string SQL = "SELECT id FROM Books WHERE title=@title;";

            return this._uow.Connection.QuerySingle<int>(SQL, new { title = title });
        }

        /// <summary>
        /// Update image and/or notes fields of book record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public override void Update(Book toUpdate)
        {
            const string SQL = "UPDATE Books " +
                "SET image = @image, notes = @notes " +
                "WHERE id = @id;";

            this._uow.Connection.ExecuteAsync(SQL, new
            {
                toUpdate.Id,

                toUpdate.Image,
                toUpdate.Notes
            });
        }
    }//class
}

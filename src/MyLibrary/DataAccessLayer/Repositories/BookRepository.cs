//MIT License

//Copyright (c) 2021-2023

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

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class BookRepository : ItemRepository<Book>, IBookRepository
    {
        public BookRepository(IUnitOfWork uow)
            : base(uow)
        {

        }

        public override async Task CreateAsync(Book entity)
        {
            await Task.Run(() =>
            {
                // insert Books table record
                const string INSERT_BOOK_SQL = "INSERT INTO Books(title,titleLong,isbn,isbn13,deweyDecimal,publisherId,format,language,datePublished,placeOfPublication,edition,pages,dimensions,overview,msrp,excerpt,synopsys,notes) " +
                                "VALUES(@title,@titleLong,@isbn,@isbn13,@deweyDecimal,@publisherId,@format,@language,@datePublished,@placeOfPublication,@edition,@pages,@dimensions,@overview,@msrp,@excerpt,@synopsys,@notes);";
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
                    msrp = entity.Msrp,
                    excerpt = entity.Excerpt,
                    synopsys = entity.Synopsys,
                    notes = entity.Notes
                });
                int bookId = this._uow.Connection.QuerySingle<int>("SELECT last_insert_rowid();");

                // insert Images table record, if any
                if (entity.Image != null)
                {
                    this._uow.Connection.Execute("INSERT INTO Images(image) VALUES(@image);", new
                    {
                        image = entity.Image
                    });

                    // update foreign key
                    int imageId = this._uow.Connection.QuerySingle<int>("SELECT last_insert_rowid();");
                    const string SET_IMAGE_ID_SQL = "UPDATE Books SET imageId=@imageId WHERE id=@bookId;";
                    this._uow.Connection.Execute(SET_IMAGE_ID_SQL, new
                    {
                        imageId = imageId,
                        bookId = bookId
                    });
                }
            });
        }//CreateAsync

        /// <summary>
        /// Delete a book record by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() =>
            {
                // delete Images table record, if any
                var imageId = this._uow.Connection.QuerySingle<int?>("SELECT imageId FROM Books WHERE id=@id", new
                {
                    id = id
                });
                if (imageId != null)
                {
                    const string DELETE_IMAGE_SQL = "DELETE FROM Images WHERE id = @id;";
                    this._uow.Connection.ExecuteAsync(DELETE_IMAGE_SQL, new { id = imageId });
                }

                // delete Books table record
                const string DELETE_BOOK_SQL = "DELETE FROM Books WHERE id = @id;";
                this._uow.Connection.ExecuteAsync(DELETE_BOOK_SQL, new { id });
            }); 
        }

        /// <summary>
        /// Retrieve all Books from the database, including linked entities.
        /// Does not include images.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Book>> ReadAllAsync()
        {
            IEnumerable<Book> result = new List<Book>();
            await Task.Run(() =>
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

                result = this._uow.Connection.Query<Book, Publisher, Book>(sql, (book, publisher) =>
                {
                    book.Publisher = publisher;
                    book.Authors = allAuthorsWithBookId.Where(row => row.BookId == (int)book.Id).Select(row => new Author { Id = (int)row.id, FirstName = row.firstName, LastName = row.lastName }).AsList();
                    book.Tags = allTagsWithBookId.Where(row => (int)row.BookId == book.Id).Select(row => new Tag { Id = (int)row.id, Name = row.name }).AsList();

                    return book;
                });
            });

            return result.AsEnumerable();
        }

        /// <summary>
        /// Get a single Book by its Id. Includes image.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Book> GetByIdAsync(int id)
        {
            Book result=null;
            await Task.Run(() =>
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
                          "INNER JOIN Publishers AS P ON B.publisherId = P.id WHERE B.id=@bookId;";
                var b = this._uow.Connection.Query<Book, Publisher, Book>(sql, (book, publisher) =>
                {
                    book.Publisher = publisher;
                    book.Authors = allAuthorsWithBookId.Where(row => row.BookId == (int)book.Id).Select(row => new Author { Id = (int)row.id, FirstName = row.firstName, LastName = row.lastName }).AsList();
                    book.Tags = allTagsWithBookId.Where(row => (int)row.BookId == book.Id).Select(row => new Tag { Id = (int)row.id, Name = row.name }).AsList();

                    return book;
                }, new { bookId = id });
                Book item = b.FirstOrDefault();

                // read Image table record, if any
                var imageId = this._uow.Connection.QuerySingle<int?>("SELECT imageId FROM Books WHERE id=@id", new
                {
                    id = id
                });
                if (imageId != null)
                {
                    var image = this._uow.Connection.QuerySingle<byte[]>("SELECT image FROM Images WHERE id=@id", new
                    {
                        id = (int)imageId
                    });
                    item.Image = image;
                }
                else
                {
                    item.Image = null;
                }

                result = item;
            });

            return result;
        }

        public async Task<int> GetIdByTitleAsync(string title)
        {
            int? result = null;
            await Task.Run(() =>
            {
                const string SQL = "SELECT id FROM Books WHERE title=@title;";

                result = this._uow.Connection.QuerySingle<int>(SQL, new { title = title });
            });

            return (int)result;
        }

        public override async Task<IEnumerable<string>> GetTitlesAsync()
        {
            IEnumerable<string> result = null;
            await Task.Run(() =>
            {
                const string SQL = "SELECT title FROM Books;";

                result = this._uow.Connection.Query<string>(SQL);
            });

            return result;
        }

        public async Task<bool> ExistsWithTitleAsync(string title)
        {
            const string SQL = "SELECT COUNT(1) FROM Books WHERE title=@title;";

            bool result = false;
            await Task.Run(() =>
            {
                result = this._uow.Connection.ExecuteScalar<bool>(SQL, new
                {
                    title = title
                });
            });

            return result;
        }

        public async Task<bool> ExistsWithLongTitleAsync(string title)
        {
            const string SQL = "SELECT COUNT(1) FROM Books WHERE titleLong=@title;";

            bool result = false;
            await Task.Run(() =>
            {
                result = this._uow.Connection.ExecuteScalar<bool>(SQL, new
                {
                    title = title
                });
            });

            return result;
        }

        public async Task<IEnumerable<string>> GetLongTitlesAsync()
        {
            IEnumerable<string> result = null;
            await Task.Run(() =>
            {
                const string SQL = "SELECT titleLong FROM Books;";

                result = this._uow.Connection.Query<string>(SQL);
            });

            return result;
        }

        public async Task<IEnumerable<string>> GetIsbnsAsync()
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
            {
                var allIsbns = this._uow.Connection.Query<string>("SELECT isbn FROM Books;");
                foreach (var isbn in allIsbns)
                {
                    if (isbn != null)
                        result.Add(isbn);
                }
            });

            return result;
        }

        public async Task<IEnumerable<string>> GetIsbn13sAsync()
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
            {
                var allIsbns = this._uow.Connection.Query<string>("SELECT isbn13 FROM Books;");
                foreach (var isbn in allIsbns)
                {
                    if (isbn != null)
                        result.Add(isbn);
                }
            });

            return result;
        }

        public async Task<bool> ExistsWithIsbnAsync(string isbn)
        {
            const string SQL = "SELECT COUNT(1) FROM Books WHERE isbn=@isbn;";

            bool result = false;
            await Task.Run(() =>
            {
                result = this._uow.Connection.ExecuteScalar<bool>(SQL, new
                {
                    isbn = isbn
                });
            });

            return result;
        }

        public async Task<bool> ExistsWithIsbn13Async(string isbn)
        {
            const string SQL = "SELECT COUNT(1) FROM Books WHERE isbn13=@isbn;";

            bool result = false;
            await Task.Run(() =>
            {
                result = this._uow.Connection.ExecuteScalar<bool>(SQL, new
                {
                    isbn13 = isbn
                });
            });

            return result;
        }

        /// <summary>
        /// Update image and other fields of book record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public override async Task UpdateAsync(Book toUpdate, bool includeImage)
        {
            await Task.Run(() =>
            {
                if (includeImage)
                {
                    // update image
                    // delete old Images table record, if it exists
                    var imageId = this._uow.Connection.QuerySingleOrDefault<int?>("SELECT imageId FROM Books WHERE id=@id", new
                    {
                        id = toUpdate.Id
                    });
                    if (imageId != null)
                    {
                        const string DELETE_OLD_IMAGE_SQL = "DELETE FROM Images WHERE id=@oldImageId;";
                        this._uow.Connection.Execute(DELETE_OLD_IMAGE_SQL, new
                        {
                            oldImageId = imageId
                        });
                    }

                    if (toUpdate.Image != null)
                    {
                        // item has new image
                        // insert new Images table record
                        this._uow.Connection.Execute("INSERT INTO Images(image) VALUES(@image);", new { image = toUpdate.Image });
                        int newImageId = this._uow.Connection.QuerySingle<int>("SELECT last_insert_rowid();");
                        // update foreign key
                        this._uow.Connection.Execute("UPDATE Books SET imageId = @imageId WHERE id = @id;", new
                        {
                            id = toUpdate.Id,
                            imageId = newImageId
                        });
                    }
                    else
                    {
                        // item has removed image
                        // set the foreign key to null
                        int? nullInt = null;
                        this._uow.Connection.Execute("UPDATE Books SET imageId = @imageId WHERE id = @id;", new
                        {
                            id = toUpdate.Id,
                            imageId = nullInt
                        });
                    }
                }

                const string UPDATE_BOOK_SQL = "UPDATE Books SET notes = @notes, deweyDecimal = @deweyDecimal, msrp = @msrp," +
                    " format = @format, datePublished = @datePublished, placeOfPublication = @placeOfPublication, edition = @edition, dimensions = @dimensions," +
                    " overview = @overview, excerpt = @excerpt, synopsys = @synopsys, titleLong = @titleLong" +
                    " WHERE id = @id;";
                this._uow.Connection.Execute(UPDATE_BOOK_SQL, new
                {
                    id = toUpdate.Id,
                    notes = toUpdate.Notes,
                    deweyDecimal = toUpdate.DeweyDecimal,
                    msrp = toUpdate.Msrp,
                    format = toUpdate.Format,
                    datePublished = toUpdate.DatePublished,
                    placeOfPublication = toUpdate.PlaceOfPublication,
                    edition = toUpdate.Edition,
                    dimensions = toUpdate.Dimensions,
                    overview = toUpdate.Overview,
                    excerpt = toUpdate.Excerpt,
                    synopsys = toUpdate.Synopsys,
                    titleLong = toUpdate.TitleLong
                });
            });
        }//Update
    }//class
}
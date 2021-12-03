using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer
{
    public class BookDataAccessor : DataAccessor, IBookDataAccessor
    {
        public async void Create(Book toAdd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve all Books from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> ReadAll()
        {
            using (var conn = GetConnection())
            {
                var authorsSql = "SELECT A.*, B.id AS BookId FROM Books as B " +
                         "INNER JOIN Book_Author AS B2A On B.id = B2A.bookId " +
                         "INNER JOIN Authors A On B2A.authorId = A.id;";
                var allAuthorsWithBookId = await conn.QueryAsync<dynamic>(authorsSql);

                var tagsSql = "SELECT T.*, B.id AS BookId FROM Books as B " +
                              "INNER JOIN Book_Tag AS B2T On B.id = B2T.bookId " +
                              "INNER JOIN Tags T On B2T.tagId = T.id;";
                var allTagsWithBookId = await conn.QueryAsync<dynamic>(tagsSql);

                var sql = "SELECT * FROM Books as B " +
                          "INNER JOIN Publishers AS P On B.publisherId = P.id;";
                var allBooks = await conn.QueryAsync<Book, Publisher, Book>(sql, (book, publisher) =>
                {
                    book.Publisher = publisher;
                    book.Authors = allAuthorsWithBookId.Where(row => row.BookId == (int)book.Id).Select(row => new Author { Id = (int)row.id, FirstName = row.firstName, LastName = row.lastName }).AsList();
                    book.Tags = allTagsWithBookId.Where(row => (int)row.BookId == book.Id).Select(row => new Tag { Id = (int)row.id, Name = row.name }).AsList();
                    return book;
                });

                return allBooks;
            }
        }

        /// <summary>
        /// Update image and/or notes fields of book record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public async Task Update(Book toUpdate)
        {
            const string SQL = "UPDATE Books " +
                "SET image = @image, notes = @notes " +
                "WHERE id = @id;";

            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync(SQL, new
                {
                    toUpdate.Id,

                    toUpdate.Image,
                    toUpdate.Notes
                });
            }
        }

        /// <summary>
        /// Associate an existing tag to an existing book.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task AssociateExistingTag(Book book, Tag tag)
        {
            const string SQL = "INSERT INTO Book_Tag (bookId,tagId) " +
                "VALUES(@itemId,@tagId);";

            using (var conn = GetConnection())
            {
                int itemId = book.Id;
                int tagId = tag.Id;
                await conn.ExecuteAsync(SQL, new
                {
                    itemId,
                    tagId
                });
            }
        }

        /// <summary>
        /// Disassociate a tag from an existing item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toRemove"></param>
        /// <returns></returns>
        public async Task RemoveTag(Book item, Tag toRemove)
        {
            const string SQL = "DELETE FROM Book_Tag WHERE bookId = @bookId AND tagId = @tagId;";

            using (var conn = GetConnection())
            {
                int itemId = item.Id;
                int tagId = toRemove.Id;
                await conn.ExecuteAsync(SQL, new
                {
                    itemId,
                    tagId
                });
            }
        }

        /// <summary>
        /// Delete a book record by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            const string SQL = "DELETE FROM Books WHERE id = @id;";

            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync(SQL, new { id });
            }
        }//DeleteById
    }//class
}

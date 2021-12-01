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
    public class BookDataAccessor
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
            // TODO: finish implementation
            using (var conn = new SQLiteConnection(Configuration.CONNECTION_STRING))
            {
                return await conn.QueryAsync<Book>("SELECT * FROM Books;");
            }
        }

        /// <summary>
        /// Update image and/or notes fields of book record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public async Task Update(Book toUpdate)
        {
            string SQL = "UPDATE Books " +
                "SET image = @image, notes = @notes " +
                "WHERE id = @id;";

            using (var conn = new SQLiteConnection(Configuration.CONNECTION_STRING))
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
            string SQL = "INSERT INTO Book_Tag (bookId,tagId) " +
                "VALUES(@itemId,@tagId);";

            using (var conn = new SQLiteConnection(Configuration.CONNECTION_STRING))
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
        public async Task RemoveTag(MediaItem item, Tag toRemove)
        {
            string SQL = "DELETE FROM Book_Tag WHERE bookId = @bookId AND tagId = @tagId;";

            using (var conn = new SQLiteConnection(Configuration.CONNECTION_STRING))
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
            string SQL = "DELETE FROM Books WHERE id = @id;";

            using (var conn = new SQLiteConnection(Configuration.CONNECTION_STRING))
            {
                await conn.ExecuteAsync(SQL, new { id });
            }
        }//DeleteById
    }//class
}

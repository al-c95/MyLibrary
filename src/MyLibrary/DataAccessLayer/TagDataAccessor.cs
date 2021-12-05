using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using Dapper;

namespace MyLibrary.DataAccessLayer
{
    /// <summary>
    /// Database interface for creating, reading and deleting tags.
    /// </summary>
    public class TagDataAccessor : DataAccessor
    {
        /// <summary>
        /// Create a new tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task Create(Tag tag)
        {
            const string SQL = "INSERT INTO Tags(name) " +
                "VALUES (@name);";

            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync(SQL, new { tag.Name });
            }
        }

        /// <summary>
        /// Read all tags from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Tag>> ReadAll()
        {
            // TODO: include associated items
            const string SQL = "SELECT * FROM Tags;";
            return await GetConnection().QueryAsync<Tag>(SQL);
        }

        /// <summary>
        /// Delete a tag by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            const string SQL = "DELETE FROM Tags WHERE id = @id;";

            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync(SQL, new { id });
            }
        }//DeleteByIdAsync
    }//class
}

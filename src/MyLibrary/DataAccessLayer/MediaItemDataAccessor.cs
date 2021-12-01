using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer
{
    public class MediaItemDataAccessor
    {
        public async void Create(MediaItem toAdd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read all media item and associated tags records from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MediaItem>> ReadAll()
        {
            string SQL = "SELECT M.id, title, type, number, image, runningTime, releaseYear, notes, T.id, name " +
                "FROM Media M " +
                "INNER JOIN Media_Tag MT ON MT.mediaId = M.id " +
                "INNER JOIN Tags T ON T.id = MT.tagId;";

            // https://stackoverflow.com/questions/25833426/using-async-await-keywords-with-dapper
            // https://www.learndapper.com/relationships

            using (var conn = new SQLiteConnection(Configuration.CONNECTION_STRING))
            {
                var items = await conn.QueryAsync<MediaItem, Tag, MediaItem>(SQL, (item, tag) =>
                {
                    item.Tags.Add(tag);
                    return item;
                }, splitOn: "id");

                var result = items.GroupBy(i => i.Id).Select(g =>
                {
                    var groupedItem = g.First();
                    groupedItem.Tags = g.Select(i => i.Tags.Single())
                                                .ToList();

                    return groupedItem;
                });

                return result;
            }
        }//ReadAll

        /// <summary>
        /// Update image and/or notes fields of media item record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public async Task Update(MediaItem toUpdate)
        {
            string SQL = "UPDATE Media " +
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
        /// Delete a media item record by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            string SQL = "DELETE FROM Media WHERE id = @id;";

            using (var conn = new SQLiteConnection(Configuration.CONNECTION_STRING))
            {
                await conn.ExecuteAsync(SQL, new { id });
            }
        }//DeleteById
    }//class
}

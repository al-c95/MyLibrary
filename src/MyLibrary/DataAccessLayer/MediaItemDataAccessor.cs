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

        public async Task<MediaItem> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MediaItem>> ReadAll()
        {
            throw new NotImplementedException();
        }

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

        public async void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

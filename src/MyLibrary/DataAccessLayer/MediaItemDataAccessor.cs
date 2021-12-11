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
    /// <summary>
    /// Database interface for media item operations.
    /// </summary>
    public class MediaItemDataAccessor : ItemDataAccessor<MediaItem>
    {
        public override async Task Create(MediaItem toAdd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read all media item and associated tags records from the database.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<MediaItem>> ReadAll()
        {
            const string SQL = "SELECT M.id, title, type, number, image, runningTime, releaseYear, notes, T.id, name " +
                "FROM Media M " +
                "INNER JOIN Media_Tag MT ON MT.mediaId = M.id " +
                "INNER JOIN Tags T ON T.id = MT.tagId;";

            using (var conn = GetConnection())
            {
                // get items with tags
                var items = await conn.QueryAsync<MediaItem, Tag, MediaItem>(SQL, (item, tag) =>
                {
                    item.Tags.Add(tag);
                    return item;
                }, splitOn: "id");
                IEnumerable<MediaItem> itemsWithTags = items.GroupBy(i => i.Id).Select(g =>
                {
                    var groupedItem = g.First();
                    groupedItem.Tags = g.Select(i => i.Tags.First())
                                                .ToList();

                    return groupedItem;
                });

                // get items with no tags
                IEnumerable<MediaItem> allItems = await conn.QueryAsync<MediaItem>("SELECT * FROM Media;");
                List<MediaItem> itemsNoTags = new List<MediaItem>();
                foreach (var item in allItems)
                {
                    if (!itemsWithTags.Any(i => i.Id==item.Id))
                    {
                        itemsNoTags.Add(item);
                    }
                }

                // concatenate and return the results
                IEnumerable<MediaItem> result = itemsWithTags.Concat(itemsNoTags);
                return result;
            }
        }//ReadAll

        /// <summary>
        /// Update image and/or notes fields of media item record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public override async Task Update(MediaItem toUpdate)
        {
            const string SQL = "UPDATE Media " +
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
        /// Associate an existing tag to an existing item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public override async Task AssociateExistingTag(MediaItem item, Tag tag)
        {
            const string SQL = "INSERT INTO Media_Tag (mediaId,tagId) " +
                "VALUES(@itemId,@tagId);";

            using (var conn = GetConnection())
            {
                int itemId = item.Id;
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
        public override async Task RemoveTag(MediaItem item, Tag toRemove)
        {
            const string SQL = "DELETE FROM Media_Tag WHERE mediaId = @itemId AND tagId = @tagId;";

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
        /// Delete a media item record by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteById(int id)
        {
            const string SQL = "DELETE FROM Media WHERE id = @id;";

            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync(SQL, new { id });
            }
        }//DeleteById
    }//class
}
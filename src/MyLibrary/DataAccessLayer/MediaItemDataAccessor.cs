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
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // insert Media table record
                        const string INSERT_MEDIA_SQL = "INSERT INTO Media (title,type,number,image,runningTime,releaseYear,notes) " +
                            "VALUES(@title,@type,@number,@image,@runningTime,@releaseYear,@notes);";
                        string title = toAdd.Title;
                        ItemType type = toAdd.Type;
                        long number = toAdd.Number;
                        byte[] image = toAdd.Image;
                        int? runningTime = toAdd.RunningTime;
                        int releaseYear = toAdd.ReleaseYear;
                        string notes = toAdd.Notes;
                        await conn.ExecuteAsync(INSERT_MEDIA_SQL, new
                        {
                            toAdd.Title,
                            toAdd.Type,
                            toAdd.Number,
                            toAdd.Image,
                            toAdd.RunningTime,
                            toAdd.ReleaseYear,
                            toAdd.Notes
                        });

                        // get all tag ids
                        List<int> tagIds = new List<int>();
                        foreach (var tag in toAdd.Tags)
                        {
                            bool exists = await conn.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Tags WHERE name=@name", new
                            {
                                tag.Name
                            });
                            if (exists)
                            {
                                // tag exists
                                // get the id
                                int tagId = await conn.QuerySingleAsync<int>("SELECT id FROM Tags WHERE name=@name", new
                                {
                                    tag.Name
                                });
                                tagIds.Add(tagId);
                            }
                            else
                            {
                                // tag does not exist
                                // insert tag
                                await conn.ExecuteAsync("INSERT INTO Tags(name) VALUES(@name);", new
                                {
                                    tag.Name
                                });
                                // get the id
                                int tagId = await conn.QuerySingleAsync<int>("SELECT id FROM Tags WHERE name=@name", new
                                {
                                    tag.Name
                                });
                                tagIds.Add(tagId);
                            }
                        }//foreach

                        // insert record(s) in Media_Tags table
                        int itemId = await conn.QuerySingleAsync<int>("SELECT id FROM Media WHERE title=@title", new
                        {
                            toAdd.Title
                        });
                        foreach (int tagId in tagIds)
                        {
                            const string INSERT_MEDIA_TAG_SQL = "INSERT INTO Media_Tag(mediaId,tagId) " +
                                "VALUES(@mediaId,@tagId);";
                            await conn.ExecuteAsync(INSERT_MEDIA_TAG_SQL, new
                            {
                                mediaId = itemId,
                                tagId = tagId
                            });
                        }

                        // if the transaction succeeded, commit it
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // transaction failed
                        // roll it back
                        transaction.Rollback();
                        // pass up the exception
                        throw;
                    }
                }//transaction

                conn.Close();
            }//connection
        }//Create

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
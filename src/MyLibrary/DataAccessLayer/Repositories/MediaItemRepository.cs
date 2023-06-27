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

using System.Collections.Generic;
using System.Linq;
using Dapper;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class MediaItemRepository : ItemRepository<MediaItem>, IMediaItemRepository
    { 
        public MediaItemRepository(IUnitOfWork uow)
            : base(uow) { }

        public override void Create(MediaItem entity)
        {
            // insert Media table record
            const string INSERT_MEDIA_SQL = "INSERT INTO Media (title,type,number,runningTime,releaseYear,notes) " +
                "VALUES(@title,@type,@number,@runningTime,@releaseYear,@notes);";
            this._uow.Connection.Execute(INSERT_MEDIA_SQL, new
            {
                title = entity.Title,
                type = entity.Type,
                number = entity.Number,
                runningTime = entity.RunningTime,
                releaseYear = entity.ReleaseYear,
                notes = entity.Notes
            });
            int itemId = this._uow.Connection.QuerySingle<int>("SELECT last_insert_rowid();");

            // insert Images table record, if any
            if (entity.Image != null)
            {
                this._uow.Connection.Execute("INSERT INTO Images(image) VALUES(@image);", new
                {
                    image = entity.Image
                });

                // update foreign key
                int imageId = this._uow.Connection.QuerySingle<int>("SELECT last_insert_rowid();");
                const string SET_IMAGE_ID_SQL = "UPDATE Media SET imageId=@imageId WHERE id=@itemId;";
                this._uow.Connection.Execute(SET_IMAGE_ID_SQL, new
                {
                    imageId = imageId,
                    itemId = itemId
                });
            }
        }

        public override void DeleteById(int id)
        {
            // delete Images table record, if any
            var imageId = this._uow.Connection.QuerySingle<int?>("SELECT imageId FROM Media WHERE id=@id", new
            {
                id = id
            });
            if (imageId != null)
            {
                const string DELETE_IMAGE_SQL = "DELETE FROM Images WHERE id = @id;";
                this._uow.Connection.ExecuteAsync(DELETE_IMAGE_SQL, new { id = imageId });
            }

            // delete Media table record
            const string DELETE_ITEM_SQL = "DELETE FROM Media WHERE id = @id;";
            this._uow.Connection.Execute(DELETE_ITEM_SQL, new { id });
        }

        /// <summary>
        /// Retrieves all media items from the database, including linked tags.
        /// Does not include images.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<MediaItem> ReadAll()
        {
            const string SQL = "SELECT M.id, title, type, number, runningTime, releaseYear, notes, T.id, name " +
                "FROM Media M " +
                "INNER JOIN Media_Tag MT ON MT.mediaId = M.id " +
                "INNER JOIN Tags T ON T.id = MT.tagId;";

            // get items with tags
            var items = this._uow.Connection.Query<MediaItem, Tag, MediaItem>(SQL, (item, tag) =>
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
            IEnumerable<MediaItem> allItems = this._uow.Connection.Query<MediaItem>("SELECT * FROM Media;");
            List<MediaItem> itemsNoTags = new List<MediaItem>();
            foreach (var item in allItems)
            {
                if (!itemsWithTags.Any(i => i.Id == item.Id))
                {
                    itemsNoTags.Add(item);
                }
            }

            // concatenate and return the results
            IEnumerable<MediaItem> result = itemsWithTags.Concat(itemsNoTags);
            return result;
        }

        /// <summary>
        /// Retrieves a single media item. Includes image.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override MediaItem GetById(int id)
        {
            // read Media table record
            var item = this._uow.Connection.QuerySingle<MediaItem>("SELECT * FROM Media WHERE id=@id", new
            {
                id = id
            });
            if (item is null)
                return null;

            // read Image table record, if any
            var imageId = this._uow.Connection.QuerySingle<int?>("SELECT imageId FROM Media WHERE id=@id", new
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
            
            // check for tags
            IEnumerable<int> tagIds = this._uow.Connection.Query<int>("SELECT tagId FROM Media_Tag WHERE mediaId=@mediaId;", new
            {
                mediaId=id
            });
            foreach (var tagId in tagIds)
            {
                var tag = this._uow.Connection.QuerySingle<Tag>("SELECT * FROM Tags WHERE id=@id", new
                {
                    id = tagId
                });
                item.Tags.Add(tag);
            }

            return item;
        }

        public int GetIdByTitle(string title)
        {
            const string SQL = "SELECT id FROM Media WHERE title=@title;";

            return this._uow.Connection.QuerySingle<int>(SQL, new { title = title });
        }

        /// <summary>
        /// Update image and/or notes, number, running time fields of media item record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public override void Update(MediaItem toUpdate, bool includeImage)
        {
            if (includeImage)
            { 
                // update image
                // delete old Images table record, if it exists
                var imageId = this._uow.Connection.QuerySingleOrDefault<int?>("SELECT imageId FROM Media WHERE id=@id", new
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
                    int newImageId = this._uow.Connection.QuerySingleOrDefault<int>("SELECT last_insert_rowid();");
                    // update foreign key
                    this._uow.Connection.Execute("UPDATE Media SET imageId = @imageId WHERE id = @id;", new
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
                    this._uow.Connection.Execute("UPDATE Media SET imageId = @imageId WHERE id = @id;", new
                    {
                        id = toUpdate.Id,
                        imageId = nullInt
                    });
                }
            }

            // update notes, running time and number fields in Media table record
            const string UPDATE_ITEM_SQL = "UPDATE Media SET notes = @notes, runningTime = @runningTime, number = @number WHERE id = @id;";
            this._uow.Connection.Execute(UPDATE_ITEM_SQL, new
            {
                id = toUpdate.Id,
                notes = toUpdate.Notes,
                runningTime = toUpdate.RunningTime,
                number = toUpdate.Number
            });
        }//Update

        public override IEnumerable<string> GetTitles()
        {
            return this._uow.Connection.Query<string>("SELECT title FROM Media;");
        }
    }//class
}
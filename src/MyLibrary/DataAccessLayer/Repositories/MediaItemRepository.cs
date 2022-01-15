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
            const string INSERT_MEDIA_SQL = "INSERT INTO Media (title,type,number,image,runningTime,releaseYear,notes) " +
                "VALUES(@title,@type,@number,@image,@runningTime,@releaseYear,@notes);";
            this._uow.Connection.Execute(INSERT_MEDIA_SQL, new
            {
                title = entity.Title,
                type = entity.Type,
                number = entity.Number,
                image = entity.Image,
                runningTime = entity.RunningTime,
                releaseYear = entity.ReleaseYear,
                notes = entity.Notes
            });
        }

        public override void DeleteById(int id)
        {
            const string SQL = "DELETE FROM Media WHERE id = @id;";

            this._uow.Connection.Execute(SQL, new { id });
        }

        public override IEnumerable<MediaItem> ReadAll()
        {
            const string SQL = "SELECT M.id, title, type, number, image, runningTime, releaseYear, notes, T.id, name " +
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

        public int GetIdByTitle(string title)
        {
            const string SQL = "SELECT id FROM Media WHERE title=@title;";

            return this._uow.Connection.QuerySingle<int>(SQL, new { title = title });
        }

        /// <summary>
        /// Update image and/or notes fields of media item record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public override void Update(MediaItem toUpdate)
        {
            const string SQL = "UPDATE Media " +
                "SET image = @image, notes = @notes " +
                "WHERE id = @id;";

            this._uow.Connection.Execute(SQL, new
            {
                toUpdate.Id,

                toUpdate.Image,
                toUpdate.Notes
            });
        }//Update
    }//class
}

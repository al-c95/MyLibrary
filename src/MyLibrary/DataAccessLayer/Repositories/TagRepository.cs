//MIT License

//Copyright (c) 2021-2022

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
using Dapper;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(IUnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// Create a new tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public override void Create(Tag entity)
        {
            const string SQL = "INSERT INTO Tags(name) " +
                "VALUES (@name);";

            this._uow.Connection.Execute(SQL, new { entity.Name });
        }

        /// <summary>
        /// Read all tags from the database.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Tag> ReadAll()
        {
            const string SQL = "SELECT * FROM Tags;";
            return this._uow.Connection.Query<Tag>(SQL);
        }

        /// <summary>
        /// Delete a tag by its id.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            const string SQL = "DELETE FROM Tags WHERE id = @id;";

            this._uow.Connection.Execute(SQL, new { id });
        }//DeleteById

        /// <summary>
        /// Delete a tag by its name. Tag names are unique.
        /// </summary>
        /// <param name="name"></param>
        public void DeleteByName(string name)
        {
            const string SQL = "DELETE FROM Tags WHERE name = @name;";

            this._uow.Connection.Execute(SQL, new { name });
        }//DeleteByName

        public bool ExistsWithName(string name)
        {
            const string SQL = "SELECT COUNT(1) FROM Tags WHERE name=@name;";

            return this._uow.Connection.ExecuteScalar<bool>(SQL, new
            {
                name = name
            });
        }//ExistsWithName

        public int GetIdByName(string name)
        {
            const string SQL = "SELECT id FROM Tags WHERE name=@name;";

            return this._uow.Connection.QuerySingle<int>(SQL, new
            {
                name = name
            });
        }//GetIdByName

        public void LinkBook(int bookId, int tagId)
        {
            this._uow.Connection.Execute("INSERT INTO Book_Tag (bookId,tagId) VALUES(@bookId,@tagId);", new
            {
                bookId = bookId,
                tagId = tagId
            });
        }

        public void LinkMediaItem(int mediaId, int tagId)
        {
            this._uow.Connection.Execute("INSERT INTO Media_Tag (mediaId,tagId) VALUES(@mediaId,@tagId);", new
            {
                mediaId = mediaId,
                tagId = tagId
            });
        }

        public void UnlinkBook(int bookId, int tagId)
        {
            this._uow.Connection.Execute("DELETE FROM Book_Tag WHERE bookId=@bookId AND tagId=@tagId;", new
            {
                bookId = bookId,
                tagId = tagId
            });
        }

        public void UnlinkMediaItem(int mediaId, int tagId)
        {
            this._uow.Connection.Execute("DELETE FROM Media_Tag WHERE mediaId=@mediaId AND tagId=@tagId;", new
            {
                mediaId = mediaId,
                tagId = tagId
            });
        }

        public void UnlinkAllTagsForBook(int bookId)
        {
            this._uow.Connection.Execute("DELETE FROM Book_Tag WHERE bookId=@bookId", new
            {
                bookId = bookId
            });
        }

        public void UnlinkAllTagsForMediaItem(int itemId)
        {
            this._uow.Connection.Execute("DELETE FROM Media_Tag WHERE mediaId=@mediaId", new
            {
                mediaId = itemId
            });
        }
    }//class
}
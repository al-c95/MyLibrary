//MIT License

//Copyright (c) 2021-2023

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
using System.Threading.Tasks;
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
        public override async Task CreateAsync(Tag entity)
        {
            await Task.Run(() =>
            {
                const string SQL = "INSERT INTO Tags(name) " +
                "VALUES (@name);";

                this._uow.Connection.Execute(SQL, new { entity.Name });
            });
        }

        /// <summary>
        /// Read all tags from the database.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Tag>> ReadAllAsync()
        {
            IEnumerable<Tag> result = new List<Tag>();
            await Task.Run(() =>
            {
                const string SQL = "SELECT * FROM Tags;";
                result = this._uow.Connection.Query<Tag>(SQL);
            });

            return result.AsEnumerable();
        }

        /// <summary>
        /// Delete a tag by its id.
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() =>
            {
                const string SQL = "DELETE FROM Tags WHERE id = @id;";

                this._uow.Connection.Execute(SQL, new { id });
            });
        }//DeleteByIdAsync

        /// <summary>
        /// Delete a tag by its name. Tag names are unique.
        /// </summary>
        /// <param name="name"></param>
        public async Task DeleteByNameAsync(string name)
        {
            await Task.Run(() =>
            {
                const string SQL = "DELETE FROM Tags WHERE name = @name;";

                this._uow.Connection.Execute(SQL, new { name });
            });
        }//DeleteByIdAsync

        public async Task<bool> ExistsWithNameAsync(string name)
        {
            bool result = false;
            await Task.Run(() =>
            {
                const string SQL = "SELECT COUNT(1) FROM Tags WHERE name=@name;";

                result = this._uow.Connection.ExecuteScalar<bool>(SQL, new
                {
                    name = name
                });
            });

            return result;
        }//ExistsWithNameAsync

        public async Task<int> GetIdByNameAsync(string name)
        {
            int? result=null;
            await Task.Run(() =>
            {
                const string SQL = "SELECT id FROM Tags WHERE name=@name;";

                result = this._uow.Connection.QuerySingle<int>(SQL, new
                {
                    name = name
                });
            });

            return (int)result;
        }//GetIdByNameAsync

        public async Task LinkBookAsync(int bookId, int tagId)
        {
            await Task.Run(() =>
            {
                this._uow.Connection.Execute("INSERT INTO Book_Tag (bookId,tagId) VALUES(@bookId,@tagId);", new
                {
                    bookId = bookId,
                    tagId = tagId
                });
            });
        }

        public async Task LinkMediaItemAsync(int mediaId, int tagId)
        {
            await Task.Run(() =>
            {
                this._uow.Connection.Execute("INSERT INTO Media_Tag (mediaId,tagId) VALUES(@mediaId,@tagId);", new
                {
                    mediaId = mediaId,
                    tagId = tagId
                });
            });
        }

        public async Task UnlinkBookAsync(int bookId, int tagId)
        {
            await Task.Run(() =>
            {
                this._uow.Connection.Execute("DELETE FROM Book_Tag WHERE bookId=@bookId AND tagId=@tagId;", new
                {
                    bookId = bookId,
                    tagId = tagId
                });
            });
        }

        public async Task UnlinkMediaItemAsync(int mediaId, int tagId)
        {
            await Task.Run(() =>
            {
                this._uow.Connection.Execute("DELETE FROM Media_Tag WHERE mediaId=@mediaId AND tagId=@tagId;", new
                {
                    mediaId = mediaId,
                    tagId = tagId
                });
            });
        }

        public async Task UnlinkAllTagsForBookAsync(int bookId)
        {
            await Task.Run(() =>
            {
                this._uow.Connection.Execute("DELETE FROM Book_Tag WHERE bookId=@bookId", new
                {
                    bookId = bookId
                });
            });
        }

        public async Task UnlinkAllTagsForMediaItemAsync(int itemId)
        {
            await Task.Run(() =>
            {
                this._uow.Connection.Execute("DELETE FROM Media_Tag WHERE mediaId=@mediaId", new
                {
                    mediaId = itemId
                });
            });
        }
    }//class
}
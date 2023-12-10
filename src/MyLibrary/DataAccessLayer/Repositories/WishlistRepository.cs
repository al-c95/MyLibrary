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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class WishlistRepository : ItemRepository<WishlistItem>, IWishlistRepository
    {
        public WishlistRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public override async Task CreateAsync(WishlistItem entity)
        {
            await Task.Run(() =>
            {
                const string SQL = "INSERT INTO Wishlist (title,type,notes) VALUES(@title,@type,@notes);";

                this._uow.Connection.Execute(SQL, new
                {
                    title = entity.Title,
                    type = entity.Type,
                    notes = entity.Notes
                });
            });
        }

        public override async Task<IEnumerable<WishlistItem>> ReadAllAsync()
        {
            IEnumerable<WishlistItem> result = new List<WishlistItem>();
            await Task.Run(() =>
            {
                const string SQL = "SELECT * FROM Wishlist;";

                result = this._uow.Connection.Query<WishlistItem>(SQL);
            });

            return result;
        }

        public override async Task UpdateAsync(WishlistItem toUpdate, bool includeImage = false)
        {
            await Task.Run(() =>
            {
                const string SQL = "UPDATE Wishlist SET notes = @notes WHERE id = @id;";

                this._uow.Connection.Execute(SQL, new
                {
                    notes = toUpdate.Notes,
                    id = toUpdate.Id
                });
            });
        }

        public async Task<bool> ExistsWithTitleAsync(string title)
        {
            const string SQL = "SELECT COUNT(1) FROM Wishlist WHERE title=@title;";

            bool result = false;
            await Task.Run(() =>
            {
                result = this._uow.Connection.ExecuteScalar<bool>(SQL, new
                {
                    title = title
                });
            });

            return result;
        }

        public override async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() =>
            {
                const string SQL = "DELETE FROM Wishlist WHERE id = @id;";

                this._uow.Connection.Execute(SQL, new { id });
            });
        }

        public override async Task<IEnumerable<string>> GetTitlesAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<WishlistItem> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }//class
}

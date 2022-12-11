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

using System;
using System.Collections.Generic;
using Dapper;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class WishlistRepository : ItemRepository<WishlistItem>, IWishlistRepository
    {
        public WishlistRepository(IUnitOfWork uow) : base(uow)
        {

        }

        public override void Create(WishlistItem entity)
        {
            const string SQL = "INSERT INTO Wishlist (title,type,notes) VALUES(@title,@type,@notes);";

            this._uow.Connection.Execute(SQL, new
            {
                title = entity.Title,
                type = entity.Type,
                notes = entity.Notes
            });
        }

        public override IEnumerable<WishlistItem> ReadAll()
        {
            const string SQL = "SELECT * FROM Wishlist;";

            return this._uow.Connection.Query<WishlistItem>(SQL);
        }

        public override void Update(WishlistItem toUpdate, bool includeImage = false)
        {
            const string SQL = "UPDATE Wishlist SET notes = @notes WHERE id = @id;";

            this._uow.Connection.Execute(SQL, new
            {
                notes = toUpdate.Notes,
                id = toUpdate.Id
            });
        }

        public override void DeleteById(int id)
        {
            const string SQL = "DELETE FROM Wishlist WHERE id = @id;";

            this._uow.Connection.Execute(SQL, new { id });
        }

        public override IEnumerable<string> GetTitles()
        {
            throw new NotImplementedException();
        }

        public override WishlistItem GetById(int id)
        {
            throw new NotImplementedException();
        }
    }//class
}

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
using MyLibrary.Models.Entities;
using Dapper;
using System.Threading.Tasks;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class BookCopyRepository : Repository<BookCopy>, IBookCopyRepository
    {
        public BookCopyRepository(IUnitOfWork uow)
            : base(uow)
        { 
        }

        public override async Task CreateAsync(BookCopy entity)
        {
            await Task.Run(() =>
            {
                const string SQL = "INSERT INTO BookCopies(bookId,description,notes) VALUES(@bookId,@description,@notes);";

                this._uow.Connection.Execute(SQL, new
                {
                    bookId = entity.BookId,
                    description = entity.Description,
                    notes = entity.Notes
                });
            });
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() =>
            {
                const string SQL = "DELETE FROM BookCopies WHERE id = @id;";

                this._uow.Connection.Execute(SQL, new { id = id });
            });
        }

        public override async Task<IEnumerable<BookCopy>> ReadAllAsync()
        {
            IEnumerable<BookCopy> result = new List<BookCopy>();
            await Task.Run(() =>
            {
                const string SQL = "SELECT * FROM BookCopies;";

                result = this._uow.Connection.Query<BookCopy>(SQL);
            });

            return result;
        }

        public async Task UpdateAsync(BookCopy toUpdate)
        {
            await Task.Run(() =>
            {
                const string SQL = "UPDATE BookCopies SET description = @description, notes = @notes WHERE id = @id;";

                this._uow.Connection.Execute(SQL, new
                {
                    id = toUpdate.Id,
                    description = toUpdate.Description,
                    notes = toUpdate.Notes
                });
            });
        }    
    }//class
}
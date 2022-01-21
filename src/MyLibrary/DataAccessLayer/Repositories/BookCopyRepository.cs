﻿//MIT License

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
using MyLibrary.Models.Entities;
using Dapper;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class BookCopyRepository : CopyRepository
    {
        public BookCopyRepository(IUnitOfWork uow)
            : base(uow) { }

        public override void Create(Copy entity)
        {
            const string SQL = "INSERT INTO BookCopies(bookId,notes) VALUES(@bookId,@notes);";

            this._uow.Connection.Execute(SQL, new
            {
                bookId = entity.Item.Id,
                notes = entity.Item.Notes
            });
        }

        public override void DeleteById(int id)
        {
            const string SQL = "DELETE FROM BookCopies WHERE id = @id;";

            this._uow.Connection.Execute(SQL, new { id = id });
        }

        public override IEnumerable<Copy> ReadAll()
        {
            const string SQL = "SELECT * FROM BookCopies;";

            // TODO: include book
            return this._uow.Connection.Query<Copy>(SQL);
        }

        public override void Update(Copy toUpdate)
        {
            const string SQL = "UPDATE BookCopies SET notes = @notes;";

            this._uow.Connection.Execute(SQL, new { notes = toUpdate.Notes });
        }
    }//class
}

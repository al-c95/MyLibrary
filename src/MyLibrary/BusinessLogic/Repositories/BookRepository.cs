﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary.BusinessLogic.Repositories
{
    public class BookRepository : ItemRepository<Book>
    {
        // ctor
        public BookRepository()
            :base(new BookDataAccessor())
        {

        }
        
        // ctor
        public BookRepository(ItemDataAccessor<Book> dataAccessor)
            :base(dataAccessor)
        {

        }

        public async Task<Boolean> ExistsWithLongTitle(string longTitle)
        {
            var allBooks = await this._dao.ReadAll();

            return allBooks.Any(b => b.TitleLong.Equals(longTitle));
        }
    }//class
}

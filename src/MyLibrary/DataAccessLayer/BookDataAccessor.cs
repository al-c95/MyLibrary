using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer
{
    public class BookDataAccessor
    {
        public async void Create(Book toAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve all Books from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> ReadAll()
        {
            // TODO: finish implementation
            using (var conn = new SQLiteConnection(Configuration.CONNECTION_STRING))
            {
                return await conn.QueryAsync<Book>("SELECT * FROM Books;");
            }
        }

        public async void Update(Book toUpdate)
        {
            throw new NotImplementedException();
        }

        public async void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

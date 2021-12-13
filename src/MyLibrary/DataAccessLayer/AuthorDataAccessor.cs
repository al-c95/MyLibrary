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
    public class AuthorDataAccessor : DataAccessor, IAuthorDataAccessor
    {
        public AuthorDataAccessor() { }

        public async Task Create(Author author)
        {
            using (var conn = GetConnection())
            {
                const string SQL = "INSERT INTO Authors(firstName,lastName) " +
                    "VALUES (@firstName,@lastName);";

                await conn.ExecuteAsync(SQL, new
                {
                    author.FirstName,
                    author.LastName
                });
            }
        }//Create

        public async Task<IEnumerable<Author>> ReadAll()
        {
            using (var conn = GetConnection())
            {
                const string SQL = "SELECT * FROM Authors;";

                return await conn.QueryAsync<Author>(SQL);
            }
        }//ReadAll
    }//class
}

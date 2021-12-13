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
    public class PublisherDataAccessor : DataAccessor, IPublisherDataAccessor
    {
        public PublisherDataAccessor() { }

        public async Task Create(Publisher publisher)
        {
            using (var conn = GetConnection())
            {
                const string SQL = "INSERT INTO Publishers(name) " +
                    "VALUES(@name);";

                await conn.ExecuteAsync(SQL, new
                {
                    publisher.Name
                });
            }
        }//Create

        public async Task<IEnumerable<Publisher>> ReadAll()
        {
            using (var conn = GetConnection())
            {
                const string SQL = "SELECT * FROM Publishers;";

                return await conn.QueryAsync<Publisher>(SQL);
            }
        }//ReadAll
    }//class
}

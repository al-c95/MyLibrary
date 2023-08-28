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
using System.Linq;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public override async Task CreateAsync(Publisher entity)
        {
            await Task.Run(() =>
            {
                const string SQL = "INSERT INTO Publishers(name) " +
                    "VALUES(@name);";

                this._uow.Connection.Execute(SQL, new
                {
                    name = entity.Name
                });
            });
        }

        public override async Task<IEnumerable<Publisher>> ReadAllAsync()
        {
            IEnumerable<Publisher> result = new List<Publisher>();
            await Task.Run(() =>
            {
                const string SQL = "SELECT * FROM Publishers;";

                result = this._uow.Connection.Query<Publisher>(SQL);
            });

            return result.AsEnumerable();
        }

        public async Task<bool> ExistsWithNameAsync(string name)
        {
            bool result = false;
            await Task.Run(() =>
            {
                const string SQL = "SELECT COUNT(1) FROM Publishers WHERE name=@name;";

                result = this._uow.Connection.ExecuteScalar<bool>(SQL, new
                {
                    name = name
                });
            });
            
            return result;
        }

        public async Task<int> GetIdByNameAsync(string name)
        {
            int? result = null;
            await Task.Run(() =>
            {
                const string SQL = "SELECT id FROM Publishers WHERE name=@name;";

                result = this._uow.Connection.QuerySingle<int>(SQL, new
                {
                    name = name
                });
            });
            
            return (int)result;
        }
    }//class
}

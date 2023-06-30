//MIT License

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

using Dapper;
using System.Threading.Tasks;

namespace MyLibrary.DataAccessLayer
{
    public class StatsService : IStatsService
    {
        IUnitOfWork _uow;

        public StatsService()
        {
            this._uow = new UnitOfWork();
        }

        public StatsService(IUnitOfWork unitOfWork)
        {
            this._uow = unitOfWork;
        }

        private async Task<int> GetCount(string entity)
        {
            int count = 0;
            await Task.Run(() =>
            {
                count = this._uow.Connection.QuerySingle<int>($"SELECT COUNT(*) FROM {entity};");
            });

            return count;
        }

        public async Task<int> GetBooksCountAsync()
        {
            return await GetCount("Books;");
        }

        public async Task<int> GetAuthorsCountAsync()
        {
            return await GetCount("Authors;");
        }

        public async Task<int> GetPublishersCountAsync()
        {
            return await GetCount("Publishers;");
        }

        public async Task<int> GetMediaItemsCountAsync()
        {
            return await GetCount("Media;");
        }

        public async Task<int> GetTagsCountAsync()
        {
            return await GetCount("Tags;");
        }
    }
}
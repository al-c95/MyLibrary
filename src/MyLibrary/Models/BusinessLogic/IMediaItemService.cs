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

using MyLibrary.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic
{
    public interface IMediaItemService
    {
        void Add(MediaItem item);
        bool AddIfNotExists(MediaItem item);
        Task<bool> AddIfNotExistsAsync(MediaItem item);
        Task AddAsync(MediaItem item);
        Task DeleteByIdAsync(int id);
        Task<bool> ExistsWithIdAsync(int id);
        bool ExistsWithTitle(string title);
        Task<bool> ExistsWithTitleAsync(string title);
        Task<IEnumerable<MediaItem>> GetAllAsync();
        Task<MediaItem> GetByIdAsync(int id);
        Task<IEnumerable<MediaItem>> GetByTypeAsync(ItemType type);
        Task<int> GetIdByTitleAsync(string title);
        void Update(MediaItem item, bool includeImage);
        Task UpdateAsync(MediaItem item, bool includeImage);
        void UpdateTags(ItemTagsDto dto);
        Task UpdateTagsAsync(ItemTagsDto dto);
    }
}
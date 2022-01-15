using MyLibrary.Models.Entities;
using System.Collections.Generic;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public interface IMediaItemRepository
    {
        void Create(MediaItem entity);
        void DeleteById(int id);
        IEnumerable<MediaItem> ReadAll();
        void Update(MediaItem toUpdate);
    }
}
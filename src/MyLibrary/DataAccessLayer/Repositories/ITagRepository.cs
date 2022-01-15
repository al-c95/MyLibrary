using MyLibrary.Models.Entities;
using System.Collections.Generic;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public interface ITagRepository
    {
        void Create(Tag entity);
        void DeleteById(int id);
        void DeleteByName(string name);
        bool ExistsWithName(string name);
        int GetIdByName(string name);
        void LinkBook(int bookId, int tagId);
        void LinkMediaItem(int mediaId, int tagId);
        IEnumerable<Tag> ReadAll();
        void UnlinkBook(int bookId, int tagId);
        void UnlinkMediaItem(int mediaId, int tagId);
    }
}
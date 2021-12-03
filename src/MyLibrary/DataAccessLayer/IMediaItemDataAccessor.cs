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
    public interface IMediaItemDataAccessor
    {
        Task<IEnumerable<MediaItem>> ReadAll();
        Task Update(MediaItem toUpdate);
        Task AssociateExistingTag(MediaItem item, Tag tag);
        Task RemoveTag(MediaItem item, Tag toRemove);
        Task DeleteById(int id);
    }
}

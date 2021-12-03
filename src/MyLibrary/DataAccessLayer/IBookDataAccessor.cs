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
    public interface IBookDataAccessor
    {
        Task<IEnumerable<Book>> ReadAll();
        Task Update(Book toUpdate);
        Task AssociateExistingTag(Book book, Tag tag);
        Task RemoveTag(MediaItem item, Tag toRemove);
        Task DeleteById(int id);
    }
}

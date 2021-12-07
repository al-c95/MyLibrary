using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer
{
    public interface ITagDataAccessor
    {
        Task Create(Tag tag);
        Task<IEnumerable<Tag>> ReadAll();
        Task DeleteById(int id);
        Task DeleteByName(string name);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer
{
    public abstract class ItemDataAccessor<T> : DataAccessor where T : Item
    {
        public ItemDataAccessor() { }
        public abstract Task Create(T toAdd);
        public abstract Task<IEnumerable<T>> ReadAll();
        public abstract Task Update(T toUpdate);
        public abstract Task UpdateTags(ItemTagsDto itemTagsDto);
        public abstract Task DeleteById(int id);
    }
}

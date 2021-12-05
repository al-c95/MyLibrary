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
        public abstract Task Create(T toAdd);
        public abstract Task<IEnumerable<T>> ReadAll();
        public abstract Task Update(T toUpdate);
        public abstract Task AssociateExistingTag(T item, Tag tag);
        public abstract Task RemoveTag(T item, Tag toRemove);
        public abstract Task DeleteById(int id);
    }
}

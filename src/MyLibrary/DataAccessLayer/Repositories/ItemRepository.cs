using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public abstract class ItemRepository<T> : Repository<T> where T : Item
    {
        public ItemRepository(IUnitOfWork uow)
            :base(uow) { }

        public abstract void Update(T toUpdate);
        public abstract void DeleteById(int id);
    }//class
}

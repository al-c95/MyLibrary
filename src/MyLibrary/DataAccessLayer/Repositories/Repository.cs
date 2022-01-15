using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public abstract class Repository<T> where T : Entity
    {
        protected readonly IUnitOfWork _uow;

        internal Repository()
        {
            this._uow = null;
        }

        internal Repository(IUnitOfWork unitOfWork)
        {
            this._uow = unitOfWork;
        }

        public abstract void Create(T entity);
        public abstract IEnumerable<T> ReadAll();
    }//class
}

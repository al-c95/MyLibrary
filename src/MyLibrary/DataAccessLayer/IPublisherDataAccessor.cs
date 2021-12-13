using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer
{
    public interface IPublisherDataAccessor
    {
        Task Create(Publisher publisher);
        Task<IEnumerable<Publisher>> ReadAll();
    }
}

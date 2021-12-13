using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer
{
    public interface IAuthorDataAccessor
    {
        Task Create(Author author);
        Task<IEnumerable<Author>> ReadAll();
    }
}

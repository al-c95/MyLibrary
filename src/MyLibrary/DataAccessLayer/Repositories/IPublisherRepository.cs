using MyLibrary.Models.Entities;
using System.Collections.Generic;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public interface IPublisherRepository
    {
        void Create(Publisher entity);
        bool ExistsWithName(string name);
        int GetIdByName(string name);
        IEnumerable<Publisher> ReadAll();
    }
}
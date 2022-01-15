using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using Dapper;

namespace MyLibrary.DataAccessLayer.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public override void Create(Publisher entity)
        {
            const string SQL = "INSERT INTO Publishers(name) " +
                    "VALUES(@name);";

            this._uow.Connection.Execute(SQL, new
            {
                name = entity.Name
            });
        }

        public override IEnumerable<Publisher> ReadAll()
        {
            const string SQL = "SELECT * FROM Publishers;";

            return this._uow.Connection.Query<Publisher>(SQL);
        }

        public bool ExistsWithName(string name)
        {
            const string SQL = "SELECT COUNT(1) FROM Publishers WHERE name=@name;";

            return this._uow.Connection.ExecuteScalar<bool>(SQL, new
            {
                name = name
            });
        }

        public int GetIdByName(string name)
        {
            const string SQL = "SELECT id FROM Publishers WHERE name=@name;";

            return this._uow.Connection.QuerySingle<int>(SQL, new
            {
                name = name
            });
        }
    }//class
}

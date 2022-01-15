using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MyLibrary.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public SQLiteConnection Connection { get; private set; }
        private SQLiteTransaction _transaction;

        public UnitOfWork()
        {
            SetConnection(new SQLiteConnection(Configuration.CONNECTION_STRING));
        }

        public UnitOfWork(SQLiteConnection connection)
        {
            SetConnection(connection);
        }

        private void SetConnection(SQLiteConnection connection)
        {
            this.Connection = connection;
            this.Connection.Open();
        }

        public void Begin()
        {
            this._transaction = this.Connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                this._transaction.Commit();
            }
            catch (Exception e)
            {
                Rollback();

                throw e;
            }
            finally
            {
                this._transaction.Dispose();
                Dispose();
            }
        }

        public void Rollback()
        {
            this._transaction.Rollback();
        }

        public void Dispose()
        {
            if (this._transaction != null)
            {
                this._transaction.Dispose();
            }
            this._transaction = null;
        }
    }//class
}

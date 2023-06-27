//MIT License

//Copyright (c) 2021

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
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

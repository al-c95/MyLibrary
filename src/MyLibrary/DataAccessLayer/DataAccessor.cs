using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MyLibrary.DataAccessLayer
{
    public abstract class DataAccessor
    {
        /// <summary>
        /// Provides a connection to the database for all implementors. This can be switched out for a connection to another database,
        /// though it has generated a lot of repetitive code in the form of using{} blocks.
        /// </summary>
        /// <returns></returns>
        protected SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(Configuration.CONNECTION_STRING);
        }
    }
}

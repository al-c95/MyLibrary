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
        protected SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(Configuration.CONNECTION_STRING);
        }
    }
}

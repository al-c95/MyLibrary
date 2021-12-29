using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.ApiService
{
    public class BookNotFoundException : Exception
    {
        public string Isbn { get; private set; }

        public BookNotFoundException(string isbn)
        {
            this.Isbn = isbn;
        }
    }//class
}

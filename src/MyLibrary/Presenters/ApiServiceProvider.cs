using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.ApiService;

namespace MyLibrary.Presenters
{
    public class ApiServiceProvider : IApiServiceProvider
    {
        public IBookApiService Get()
        {
            return new BookApiService();
        }
    }//class
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Utils
{
    public interface IImageFileReader
    {
        string Path { get; set; }
        byte[] ReadBytes();
    }
}

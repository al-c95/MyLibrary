using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Utils
{
    public class ImageFileReader : IImageFileReader
    {
        public string Path { get; set; }

        public ImageFileReader()
        {

        }

        public ImageFileReader(string path)
        {

        }

        public byte[] ReadBytes()
        {
            return System.IO.File.ReadAllBytes(this.Path);
        }
    }//class
}

//MIT License

//Copyright (c) 2021-2022

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
using System.Threading.Tasks;

namespace MyLibrary.Models.BusinessLogic.ImportCsv
{
    public class CsvFile : ICsvFile
    {
        public string Path { get; private set; }

        public CsvFile(string path)
        {
            if (System.IO.Path.GetExtension(path).Equals(".csv"))
                this.Path = path;
            else
                throw new Exception("Import must be a CSV file");
        }

        public async Task<string[]> ReadLinesAsync()
        {
            using (var reader = System.IO.File.OpenText(this.Path))
            {
                var text = await reader.ReadToEndAsync();
                return text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            }
        }
    }//class
}
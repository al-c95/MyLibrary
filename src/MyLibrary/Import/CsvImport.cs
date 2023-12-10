//MIT License

//Copyright (c) 2021-2023

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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Import
{
    /*
    public abstract class CsvImport : IEnumerable<CsvRowResult>
    {
        protected string[] _lines;

        public int LinesCount => this._lines.Count();
        public abstract string GetTypeName { get; }

        public abstract IEnumerator<CsvRowResult> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public abstract Task<bool> AddIfNotExists(CsvRowResult row);

        /// <summary>
        /// Factory method for generating appropriate CSV import object based on type.
        /// Register new ones here.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal static async Task<CsvImport> ImportFactory(string type, string filePath)
        {
            switch (type)
            {
                case "tag":
                    return await TagCsvImport.BuildAsync(new CsvFile(filePath));
                case "author":
                    return await AuthorCsvImport.BuildAsync(new CsvFile(filePath));
                case "publisher":
                    return await PublisherCsvImport.BuildAsync(new CsvFile(filePath));
                default:
                    throw new Exception("Unknown CSV import type: " + type);
            }
        }
    }//class
    */
}
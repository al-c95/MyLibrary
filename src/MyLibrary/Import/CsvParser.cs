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

using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace MyLibrary.Import
{
    public class CsvParser : IEnumerable<string[]>, ICsvParser
    {
        private readonly string _filePath;
        private readonly char _delimiter;

        public string FilePath { get { return this._filePath; } }

        public CsvParser(string filePath, char delimiter = ',')
        {
            this._filePath = filePath;
            this._delimiter = delimiter;
        }//ctor

        public IEnumerator<string[]> GetEnumerator()
        {
            return new CsvFileEnumerator(this._filePath, this._delimiter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class CsvFileEnumerator : IEnumerator<string[]>
        {
            private TextFieldParser parser;
            private string[] _currentRow;
            private string _filePath;

            public CsvFileEnumerator(string filePath, char delimiter)
            {
                this._filePath = filePath;
                parser = new TextFieldParser(filePath);
                parser.SetDelimiters(delimiter.ToString());
                parser.HasFieldsEnclosedInQuotes = true;
            }

            public string[] Current => this._currentRow;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (parser.EndOfData)
                {
                    return false;
                }

                this._currentRow = parser.ReadFields();
                return true;
            }

            public void Reset()
            {
                parser.Close();
                parser = new TextFieldParser(this._filePath);
                parser.SetDelimiters(parser.Delimiters);
                parser.HasFieldsEnclosedInQuotes = true;
            }

            public void Dispose()
            {
                parser.Close();
            }
        }//class
    }//class
}
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
using MyLibrary.Models.Entities;

namespace MyLibrary.Import
{
    public class PublisherCsvImportCollection : CsvImportCollection<Publisher>
    {
        public PublisherCsvImportCollection(ICsvParserService parserService)
            : base(parserService)
        {
        }

        public override void LoadFromFile(string fileName, Action<int, int> progressCallback)
        {
            ParsedCount = 0;
            SkippedCount = 0;

            var csvParser = this._csvParserService.Get(fileName);
            int index = 0;
            foreach (var row in csvParser)
            {
                // deal with header
                if (index == 0)
                {
                    if (row[0] != "Publisher")
                    {
                        throw new FormatException("CSV file has incorrect format.");
                    }
                }
                else
                {
                    // read data
                    if (row.Length != 1)
                    {
                        throw new FormatException("CSV file has incorrect format.");
                    }
                    if (Publisher.ValidateName(row[0]))
                    {
                        this._entities.Add(new Publisher(row[0]));

                        ParsedCount++;
                        progressCallback?.Invoke(ParsedCount, SkippedCount);
                    }
                    else
                    {
                        SkippedCount++;
                        progressCallback?.Invoke(ParsedCount, SkippedCount);
                    }
                }

                index++;
            }
        }//LoadFromFile
    }//class
}
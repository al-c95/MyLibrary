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

using MyLibrary.Models.Entities;
using MyLibrary.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace MyLibrary.Import
{
    public class PublisherCsvReader : CsvReaderBase<Publisher>
    {
        public PublisherCsvReader(CsvFile csvFile, AppVersion runningVersion)
            : base(csvFile, runningVersion)
        {

        }

        public override IEnumerable<Publisher> Read(Action<int, int> progressCallback)
        {
            int parsedCount = 0;
            int skippedCount = 0;

            var allLines = this._csv.ReadLinesSync();
            int index = 0;
            foreach (var line in allLines)
            {
                // skip header line
                if (index == 0)
                {
                    index++;
                    continue;
                }

                if (Publisher.ValidateName(line))
                {
                    parsedCount++;
                    progressCallback?.Invoke(parsedCount, skippedCount);
                    yield return new Publisher(line);
                }
                else
                {
                    skippedCount++;
                    progressCallback?.Invoke(parsedCount, skippedCount);
                }

                index++;
            }
        }//Read
    }//class
}
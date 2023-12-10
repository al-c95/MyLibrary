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
using System.Text.RegularExpressions;

namespace MyLibrary.Import
{
    public class AuthorCsvReader : CsvReaderBase<Author>
    {
        public AuthorCsvReader(CsvFile csvFile, AppVersion runningVersion) : base(csvFile, runningVersion)
        {
        }

        public override IEnumerable<Author> Read(Action<int, int> progressCallback)
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

                // TODO: refactor validation
                const string NAME_ENTRY_PATTERN = @"^[a-zA-Z-]+,([a-zA-Z-']+ )*[a-zA-Z-']+$";
                const string NAME_ENTRY_PATTERN_WITH_MIDDLE_NAME = @"^[a-zA-Z-]+ [a-zA-Z]\.,([a-zA-Z-']+ )*[a-zA-Z-']+$";
                if (Regex.IsMatch(line, NAME_ENTRY_PATTERN) ||
                    Regex.IsMatch(line, NAME_ENTRY_PATTERN_WITH_MIDDLE_NAME))
                {
                    string[] parts = line.Split(',');
                    string processedName = parts[0] + " " + parts[1];

                    parsedCount++;
                    progressCallback?.Invoke(parsedCount, skippedCount);
                    yield return new Author { FirstName = parts[0], LastName = parts[1] };
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
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
using System.Collections.Generic;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Models.ValueObjects;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace MyLibrary.Import
{
    public class MediaItemExcelReader : ExcelReaderBase<MediaItem>, IMediaItemExcelReader
    {
        public MediaItemExcelReader(ExcelPackage excel, string worksheet, AppVersion runningVersion)
            : base(excel, worksheet, runningVersion)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // validate worksheet
            bool sane = true;
            sane = sane && Read("B2").Equals("Media items");
            sane = sane && Read("A6").Equals("Id");
            sane = sane && Read("B6").Equals("Title");
            sane = sane && Read("C6").Equals("Type");
            sane = sane && Read("D6").Equals("Number");
            sane = sane && Read("E6").Equals("Running Time");
            sane = sane && Read("F6").Equals("Release Year");
            sane = sane && Read("G6").Equals("Tags");
            sane = sane && Read("H6").Equals("Notes");
            if (!sane)
            {
                throw new FormatException("Provided Excel is not a valid media items export from MyLibrary");
            }
        }

        private string Read(int row, int column)
        {
            return ReadCellAsString(this._excel, "Media item", row, column);
        }

        private string Read(string address)
        {
            return ReadCellAsString(this._excel, "Media item", address);
        }

        public override IEnumerable<MediaItem> Read(Action<int, int> progressCallback)
        {
            int parsedCount = 0;
            int skippedCount = 0;

            ExcelAddressBase usedRange = this._excel.Workbook.Worksheets["Media item"].Dimension;
            for (int index = HEADER_ROW + 1; index <= usedRange.End.Row; index++)
            {
                string idEntry = Read(index, 1);
                string title = Read(index, 2);
                string typeEntry = Read(index, 3);
                string numberEntry = Read(index, 4);
                string runningTimeEntry = Read(index, 5);
                string releaseYearEntry = Read(index, 6);
                string tagsEntry = Read(index, 7);
                string notes = Read(index, 8);

                MediaItem item = new MediaItem();
                MediaItemBuilder builder = new MediaItemBuilder();
                try
                {
                    List<string> tags = new List<string>();
                    if (!string.IsNullOrWhiteSpace(tagsEntry))
                    {
                        Regex tagSplitRegex = new Regex(", ");
                        string[] tagNames = tagSplitRegex.Split(tagsEntry);
                        foreach (var tagName in tagNames)
                        {
                            tags.Add(tagName);
                        }
                    }
                    item = builder
                        .WithTitle(title)
                        .WithNumber(numberEntry)
                        .WithRunningTime(runningTimeEntry)
                        .WithYear(releaseYearEntry)
                        .WithTags(tags)
                        .WithId(idEntry)
                        .Build();

                    item.Notes = notes;
                    item.Type = Item.ParseType(typeEntry);

                    parsedCount++;
                    progressCallback?.Invoke(parsedCount, skippedCount);
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentException || ex is FormatException)
                    {
                        skippedCount++;
                        progressCallback?.Invoke(parsedCount, skippedCount);

                        continue;
                    }

                    throw ex;
                }

                yield return item;
            }
        }//Read
    }//class
}
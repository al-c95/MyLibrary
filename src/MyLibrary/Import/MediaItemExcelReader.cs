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
using MyLibrary.Models.Entities.Factories;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Models.ValueObjects;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Linq;

namespace MyLibrary.Import
{
    public class MediaItemExcelReader : ExcelReaderBase<MediaItem>
    {
        public MediaItemExcelReader(ExcelPackage excel, string worksheet, AppVersion runningVersion)
            :base(excel, worksheet, runningVersion)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // validate worksheet
            bool sane = true;
            sane = sane && ReadCellAsString(this._excel, "Media item", "B2").Equals("Media items");
            sane = sane && ReadCellAsString(this._excel, "Media item", "A6").Equals("Id");
            sane = sane && ReadCellAsString(this._excel, "Media item", "B6").Equals("Title");
            sane = sane && ReadCellAsString(this._excel, "Media item", "C6").Equals("Type");
            sane = sane && ReadCellAsString(this._excel, "Media item", "D6").Equals("Number");
            sane = sane && ReadCellAsString(this._excel, "Media item", "E6").Equals("Running Time");
            sane = sane && ReadCellAsString(this._excel, "Media item", "F6").Equals("Release Year");
            sane = sane && ReadCellAsString(this._excel, "Media item", "G6").Equals("Tags");
            sane = sane && ReadCellAsString(this._excel, "Media item", "H6").Equals("Notes");
            if (!sane)
            {
                throw new FormatException("Provided Excel is not a valid media items export from MyLibrary");
            }
        }

        public override IEnumerable<MediaItem> Read(Action<int, int> progressCallback)
        {
            int importedCount = 0;
            int skippedCount = 0;

            ExcelAddressBase usedRange = this._excel.Workbook.Worksheets["Media item"].Dimension;
            for (int index = HEADER_ROW + 1; index <= usedRange.End.Row; index++)
            {
                string idEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 1].GetValue<string>();
                string title = this._excel.Workbook.Worksheets["Media item"].Cells[index, 2].GetValue<string>();
                string typeEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 3].GetValue<string>();
                string numberEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 4].GetValue<string>();
                string runningTimeEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 5].GetValue<string>();
                string releaseYearEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 6].GetValue<string>();
                string tagsEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 7].GetValue<string>();
                string notes = this._excel.Workbook.Worksheets["Media item"].Cells[index, 8].GetValue<string>();

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

                    importedCount++;
                    progressCallback?.Invoke(importedCount, skippedCount);
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentException || ex is FormatException)
                    {
                        skippedCount++;
                        progressCallback?.Invoke(importedCount, skippedCount);

                        continue;
                    }

                    throw ex;
                }

                yield return item;
            }
        }//Read
    }//class
}
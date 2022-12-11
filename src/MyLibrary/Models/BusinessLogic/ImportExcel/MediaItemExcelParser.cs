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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using MyLibrary.Models.Entities;
using MyLibrary.Models.ValueObjects;

namespace MyLibrary.Models.BusinessLogic.ImportExcel
{
    // https://stackoverflow.com/questions/37380788/unit-testing-with-complex-setup-and-logic
    public class MediaItemExcelParser : ExcelParserBase<MediaItem>, IDisposable
    {
        public MediaItemExcelParser(ExcelPackage excel, AppVersion runningVersion)
            :base(excel, "Media item", runningVersion)
        {
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

        public override IEnumerable<ExcelRowResult> Run()
        {
            ExcelAddressBase usedRange = this._excel.Workbook.Worksheets["Media item"].Dimension;
            for (int index = HEADER_ROW+1; index <= usedRange.End.Row; index++)
            {
                // read row
                // if empty, just move to the next one
                string idEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 1].GetValue<string>();
                string title = this._excel.Workbook.Worksheets["Media item"].Cells[index, 2].GetValue<string>();
                string typeEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 3].GetValue<string>();
                string numberEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 4].GetValue<string>();
                string runningTimeEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 5].GetValue<string>();
                string releaseYearEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 6].GetValue<string>();
                string tagsEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 7].GetValue<string>();
                string notes = this._excel.Workbook.Worksheets["Media item"].Cells[index, 8].GetValue<string>();
                bool allEmpty = true;
                allEmpty = allEmpty && string.IsNullOrWhiteSpace(idEntry);
                allEmpty = allEmpty && string.IsNullOrWhiteSpace(title);
                allEmpty = allEmpty && string.IsNullOrWhiteSpace(typeEntry);
                allEmpty = allEmpty && string.IsNullOrWhiteSpace(numberEntry);
                allEmpty = allEmpty && string.IsNullOrWhiteSpace(runningTimeEntry);
                allEmpty = allEmpty && string.IsNullOrWhiteSpace(releaseYearEntry);
                allEmpty = allEmpty && string.IsNullOrWhiteSpace(tagsEntry);
                allEmpty = allEmpty && string.IsNullOrWhiteSpace(notes);
                if (allEmpty)
                {
                    continue;
                }

                // process Id
                int? id = null;
                if (!string.IsNullOrWhiteSpace(idEntry))
                {
                    // if the entry in this column is not empty, it should be an integer
                    int outValue;
                    if (!int.TryParse(idEntry, out outValue))
                    {
                        yield return new ExcelRowResult
                        {
                            Row = index,
                            Item = null,
                            Status = ExcelRowResultStatus.Error,
                            Message = "Invalid Id: " + idEntry
                        };

                        continue;
                    }
                    else
                    {
                        id = outValue;
                    }
                }
                // process Title  
                if (string.IsNullOrWhiteSpace(title))
                {
                    yield return new ExcelRowResult
                    {
                        Row = index,
                        Item = null,
                        Status = ExcelRowResultStatus.Error,
                        Message = "Title cannot be empty"
                    };

                    continue;
                }
                // process Type
                ItemType type;
                if (typeEntry.Equals("Cd") || 
                    typeEntry.Equals("Dvd") ||
                    typeEntry.Equals("BluRay") ||
                    typeEntry.Equals("Vhs") ||
                    typeEntry.Equals("Vinyl") ||
                    typeEntry.Equals("Other") ||
                    typeEntry.Equals("Floppy Disk") ||
                    typeEntry.Equals("Flash Drive"))
                {
                    type = Item.ParseType(typeEntry);
                }
                else
                {
                    yield return new ExcelRowResult
                    {
                        Row = index,
                        Item = null,
                        Status = ExcelRowResultStatus.Error,
                        Message = "Unsupported type: " + typeEntry
                    };

                    continue;
                }
                // process Number
                long number;
                if (!long.TryParse(numberEntry, out number))
                {
                    yield return new ExcelRowResult
                    {
                        Row = index,
                        Item = null,
                        Status = ExcelRowResultStatus.Error,
                        Message = "Invalid number: " + numberEntry
                    };

                    continue;
                }
                // process Running Time
                int? runningTime = null;
                if (!string.IsNullOrWhiteSpace(runningTimeEntry))
                {
                    // if the entry in this column is not empty, it should be an integer
                    int outValue;
                    if (!int.TryParse(runningTimeEntry, out outValue))
                    {
                        yield return new ExcelRowResult
                        {
                            Row = index,
                            Item = null,
                            Status = ExcelRowResultStatus.Error,
                            Message = "Invalid running time: " + runningTimeEntry
                        };

                        continue;
                    }
                    else
                    {
                        runningTime = outValue;
                    }
                }
                // process Release Year
                int releaseYear;
                if (!int.TryParse(releaseYearEntry, out releaseYear))
                {
                    yield return new ExcelRowResult
                    {
                        Row = index,
                        Item = null,
                        Status = ExcelRowResultStatus.Error,
                        Message = "Invalid release year: " + releaseYearEntry
                    };

                    continue;
                }
                // process Tags
                List<Tag> tags = new List<Tag>();
                if (!string.IsNullOrWhiteSpace(tagsEntry))
                {
                    Regex tagSplitRegex = new Regex(", ");
                    string[] tagNames = tagSplitRegex.Split(tagsEntry);
                    foreach (var tagName in tagNames)
                    {
                        tags.Add(new Tag { Name = tagName });
                    }
                }
           
                // all ok
                // create the object
                MediaItem item = new MediaItem();
                if (id != null)
                    item.Id = (int)id;
                item.Title = title;
                item.Type = type;
                item.Number = number;
                item.ReleaseYear = releaseYear;
                item.RunningTime = runningTime;
                item.Notes = notes;
                item.Tags = tags;
                yield return new ExcelRowResult
                {
                    Row = index,
                    Item = item,
                    Status = ExcelRowResultStatus.Success
                };
            }//For
        }//Run

        public void Dispose()
        {
            this._excel?.Dispose();
        }
    }//class
}
//MIT License

//Copyright (c) 2022

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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OfficeOpenXml;
using MyLibrary.Models.Entities;
using MyLibrary.Models.ValueObjects;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;
using MyLibrary.Utils;

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
                // read Id
                int? id = null;
                string idEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 1].GetValue<string>(); 
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
                // read Title
                string title = this._excel.Workbook.Worksheets["Media item"].Cells[index, 2].GetValue<string>();
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
                // read Type
                ItemType type;
                string typeEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 3].GetValue<string>();
                if (!Enum.TryParse<ItemType>(typeEntry, out type) || typeEntry.Equals("Book"))
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
                // read Number
                long number;
                string numberEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 4].GetValue<string>();
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
                // read Running Time
                string runningTimeEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 5].GetValue<string>();
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
                // read Release Year
                int releaseYear;
                string releaseYearEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 6].GetValue<string>();
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
                // read Tags
                string tagsEntry = this._excel.Workbook.Worksheets["Media item"].Cells[index, 7].GetValue<string>();
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
                // read Notes
                string notes = this._excel.Workbook.Worksheets["Media item"].Cells[index, 8].GetValue<string>();

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
// MIT License

//Copyright (c) 2021

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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views.Excel;

namespace MyLibrary.Presenters.Excel
{
    public class MediaItemExcelPresenter : ExcelPresenterBase
    {
        protected readonly IMediaItemService _mediaItemService;

        protected const int ID_COL = 1;
        protected const int TITLE_COL = 2;
        protected const int TYPE_COL = 3;
        protected const int NUMBER_COL = 4;
        protected const int RUNNING_TIME_COL = 5;
        protected const int RELEASE_YEAR_COL = 6;
        protected const int TAGS_COL = 7;
        protected const int NOTES_COL = 8;

        public MediaItemExcelPresenter(IMediaItemService tagService, IExcelFile file, Views.IExportDialog dialog, Views.Excel.Excel excel)
            :base("Media item", file, dialog, excel)
        {
            this._mediaItemService = tagService;
            WriteHeaders();
        }

        public MediaItemExcelPresenter(Views.IExportDialog dialog, Views.Excel.Excel excel)
            : base("Media item", new ExcelFile(), dialog, excel)
        {
            this._mediaItemService = new MediaItemService();
            WriteHeaders();
        }

        protected override void WriteHeaders()
        {
            WriteHeaderCell("B", "Title");
            WriteHeaderCell("C", "Type");
            WriteHeaderCell("D", "Number");
            WriteHeaderCell("E", "Running Time");
            WriteHeaderCell("F", "Release Year");
            WriteHeaderCell("G", "Tags");
            WriteHeaderCell("H", "Notes");
        }

        protected async override Task RenderExcel(IProgress<int> numberExported, CancellationToken token)
        {
            // write data
            var allItems = await this._mediaItemService.GetAll();
            int count = 0;
            await Task.Run(() =>
            {
                foreach (var item in allItems)
                {
                    WriteEntityRow(new object[]
                    {
                        item.Id,
                        item.Title,
                        item.Type,
                        item.Number,
                        item.RunningTime,
                        item.ReleaseYear,
                        item.GetCommaDelimitedTags(),
                        item.Notes
                    });
                    this._excel.Worksheet.Cells[this._currRow, 4].Style.Numberformat.Format = "@";

                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            this._dialog.Label1 = "Formatting worksheet...";

            // autofit some columns
            AutoFitColumn(TITLE_COL);
            AutoFitColumn(NUMBER_COL);
            AutoFitColumn(RUNNING_TIME_COL);
            AutoFitColumn(RELEASE_YEAR_COL);
            AutoFitColumn(TAGS_COL);

            // wrap text and set width for Notes column
            WrapText(NOTES_COL);
            SetColumnWidth(NOTES_COL, 30);

            // unlock selected cells
            await Task.Run(() =>
            {
                SetWorksheetProtectionAttributes();
                for (int i = HEADER_ROW + 1; i <= 65000; i++)
                {
                    UnlockCell(i, NUMBER_COL);
                    UnlockCell(i, RUNNING_TIME_COL);
                    UnlockCell(i, TAGS_COL);
                    UnlockCell(i, NOTES_COL);

                    // allow adding new rows
                    if (i >= HEADER_ROW + count)
                    {
                        UnlockCell(i, TITLE_COL);
                        UnlockCell(i, TYPE_COL);
                        UnlockCell(i, RELEASE_YEAR_COL);
                    }
                }
            });

            await this._excel.SaveAsync(this._file, this._dialog.Path);
        }//RenderExcel
    }//class
}
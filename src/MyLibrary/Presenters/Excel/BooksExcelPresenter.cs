//MIT License

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
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views.Excel;

namespace MyLibrary.Presenters.Excel
{
    public class BookExcelPresenter : ExcelPresenterBase
    {
        protected readonly IBookService _bookService;

        public BookExcelPresenter(IBookService bookService, IExcelFile file, Views.IExportDialog dialog)
            :base("Book", file, dialog)
        {
            this._bookService = bookService;
            WriteHeaders();
        }

        public BookExcelPresenter(Views.IExportDialog dialog)
            : base("Book", new ExcelFile(), dialog)
        {
            this._bookService = new BookService();
            WriteHeaders();
        }

        protected override void WriteHeaders()
        {
            WriteHeaderCell("B", "Title");
            WriteHeaderCell("C", "Long Title");
            WriteHeaderCell("D", "ISBN");
            WriteHeaderCell("E", "ISBN13");
            WriteHeaderCell("F", "Authors");
            WriteHeaderCell("G", "Language");
            WriteHeaderCell("H", "Tags");
            WriteHeaderCell("I", "Publisher");
            WriteHeaderCell("J", "Format");
            WriteHeaderCell("K", "Date Published");
            WriteHeaderCell("L", "Place of Publication");
            WriteHeaderCell("M", "Edition");
            WriteHeaderCell("N", "Pages");
            WriteHeaderCell("O", "Dimensions");
            WriteHeaderCell("P", "Overview");
            WriteHeaderCell("Q", "Excerpt");
            WriteHeaderCell("R", "Synopsys");
            WriteHeaderCell("S", "Notes");
        }

        protected async override Task RenderExcel(IProgress<int> numberExported)
        {
            // write data
            var allItems = await this._bookService.GetAll();
            await Task.Run(() =>
            {
                int count = 0;
                foreach (var item in allItems)
                {
                    WriteEntityRow(new object[]
                    {
                        item.Id,
                        item.Title,
                        item.TitleLong,
                        item.Isbn,
                        item.Isbn13,
                        item.GetAuthorList(),
                        item.Language,
                        item.GetCommaDelimitedTags(),
                        item.Publisher.Name,
                        item.Format,
                        item.DatePublished,
                        item.PlaceOfPublication,
                        item.Edition,
                        item.Pages,
                        item.Dimensions,
                        item.Overview,
                        item.Excerpt,
                        item.Synopsys,
                        item.Notes
                    });
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            this._dialog.Label1 = "Formatting worksheet...";

            // autofit some columns
            int col = 2;
            AutoFitColumn(col);
            while (col <= 15)
            {
                AutoFitColumn(col);
                col++;
            }

            // wrap text and set column width for some columns
            // Dimensions, Overview, Excerpt, Synopsys, Notes
            col = 16;
            while (col <= 19)
            {
                WrapText(col);
                SetColumnWidth(col, 30);

                col++;
            }

            // lock selected cells
            await Task.Run(() =>
            {
                SetWorksheetProtectionAttributes();
                for (int i = HEADER_ROW + 1; i <= 65000; i++)
                {
                    UnlockCell(i, 2);
                    UnlockCell(i, 3);
                    for (int j = 6; j <= 19; j++)
                    {
                        UnlockCell(i, j);
                    }
                }
            });
            
            await this._excel.SaveAsync(this._file, this._dialog.Path);
        }
    }//class
}
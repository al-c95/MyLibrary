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
using System.Threading;
using System.Threading.Tasks;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views.Excel;

namespace MyLibrary.Presenters.Excel
{
    public class BookExcelPresenter : ExcelPresenterBase
    {
        protected readonly IBookService _bookService;

        protected const int ID_COL =1;
        protected const int TITLE_COL = 2;
        protected const int TITLE_LONG_COL = 3;
        protected const int ISBN_COL = 4;
        protected const int ISBN13_COL = 5;
        protected const int AUTHORS_COL = 6;
        protected const int LANGUAGE_COL = 7;
        protected const int TAGS_COL = 8;
        protected const int DEWEY_DECIMAL_COL = 9;
        protected const int MSRP_COL = 10;
        protected const int PUBLISHER_COL = 11;
        protected const int FORMAT_COL = 12;
        protected const int DATE_PUBLISHED_COL = 13;
        protected const int PLACE_OF_PUBLICATION_COL = 14;
        protected const int EDITION_COL = 15;
        protected const int PAGES_COL = 16;
        protected const int DIMENSIONS_COL = 17;
        protected const int OVERVIEW_COL = 18;
        protected const int EXCERPT_COL = 19;
        protected const int SYNOPSYS_COL = 20;
        protected const int NOTES_COL = 21;

        public BookExcelPresenter(IBookService bookService, IExcelFile file, Views.IExportDialog dialog, Views.Excel.Excel excel)
            :base("Book", file, dialog, excel)
        {
            this._bookService = bookService;
            WriteHeaders();
        }

        public BookExcelPresenter(Views.IExportDialog dialog, Views.Excel.Excel excel)
            : base("Book", new ExcelFile(), dialog, excel)
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
            WriteHeaderCell("I", "Dewey Decimal");
            WriteHeaderCell("J", "MSRP");
            WriteHeaderCell("K", "Publisher");
            WriteHeaderCell("L", "Format");
            WriteHeaderCell("M", "Date Published");
            WriteHeaderCell("N", "Place of Publication");
            WriteHeaderCell("O", "Edition");
            WriteHeaderCell("P", "Pages");
            WriteHeaderCell("Q", "Dimensions");
            WriteHeaderCell("R", "Overview");
            WriteHeaderCell("S", "Excerpt");
            WriteHeaderCell("T", "Synopsys");
            WriteHeaderCell("U", "Notes");
        }

        protected async override Task RenderExcel(IProgress<int> numberExported, CancellationToken token)
        {
            // write data
            int count = 0;
            var allItems = await this._bookService.GetAllAsync();
            await Task.Run(() =>
            {
                foreach (var item in allItems)
                {
                    // check for cancellation
                    if (token.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }

                    // now write the data
                    WriteEntityRow(new object[]
                    {
                        item.Id,
                        item.Title,
                        item.TitleLong,
                        item.Isbn,
                        item.Isbn13,
                        item.GetAuthorListFullNamesGiven(),
                        item.Language,
                        item.GetCommaDelimitedTags(),
                        item.DeweyDecimal,
                        item.Msrp,
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
            int col = TITLE_COL;
            AutoFitColumn(col);
            while (col <= DIMENSIONS_COL)
            {
                AutoFitColumn(col);
                col++;
            }
            
            // wrap text and set column width for some columns
            // Dimensions, Overview, Excerpt, Synopsys, Notes
            col = OVERVIEW_COL;
            while (col <= NOTES_COL)
            {
                WrapText(col);
                SetColumnWidth(col, 30);

                col++;
            }

            // unlock selected cells
            await Task.Run(() =>
            {
                SetWorksheetProtectionAttributes();
                for (int i = HEADER_ROW + 1; i <= 65000; i++)
                {
                    // check for cancellation
                    if (token.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }

                    UnlockCell(i, DEWEY_DECIMAL_COL);
                    UnlockCell(i, TAGS_COL);
                    UnlockCell(i, FORMAT_COL);
                    UnlockCell(i, DATE_PUBLISHED_COL);
                    UnlockCell(i, PLACE_OF_PUBLICATION_COL);
                    UnlockCell(i, EDITION_COL);
                    UnlockCell(i, DIMENSIONS_COL);
                    UnlockCell(i, OVERVIEW_COL);
                    UnlockCell(i, MSRP_COL);
                    UnlockCell(i, EXCERPT_COL);
                    UnlockCell(i, SYNOPSYS_COL);
                    UnlockCell(i, NOTES_COL);
                    UnlockCell(i, TITLE_LONG_COL);

                    // allow adding new rows
                    if (i >= HEADER_ROW + count + 1)
                    {
                        UnlockCell(i, TITLE_COL);
                        UnlockCell(i, TITLE_LONG_COL);
                        UnlockCell(i, ISBN_COL);
                        UnlockCell(i, ISBN13_COL);
                        UnlockCell(i, PUBLISHER_COL);
                        UnlockCell(i, LANGUAGE_COL);
                        UnlockCell(i, PAGES_COL);
                        UnlockCell(i, AUTHORS_COL);
                    }
                }
            });
            
            await this._excel.SaveAsync(this._file, this._dialog.Path);
        }
    }//class
}
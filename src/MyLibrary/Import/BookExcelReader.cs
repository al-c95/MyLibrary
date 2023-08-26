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
using System.Net.Http.Headers;
using System.Runtime.Remoting;

namespace MyLibrary.Import
{
    public class BookExcelReader : ExcelReaderBase<Book>
    {
        public BookExcelReader(ExcelPackage excel, string worksheet, AppVersion runningVersion)
            :base(excel, worksheet, runningVersion)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // validate worksheet
            bool sane = true;
            sane = sane && ReadCellAsString(this._excel, "Book", "B2").Equals("Books");
            sane = sane && ReadCellAsString(this._excel, "Book", "A6").Equals("Id");
            sane = sane && ReadCellAsString(this._excel, "Book", "B6").Equals("Title");
            sane = sane && ReadCellAsString(this._excel, "Book", "C6").Equals("Long Title");
            sane = sane && ReadCellAsString(this._excel, "Book", "D6").Equals("ISBN");
            sane = sane && ReadCellAsString(this._excel, "Book", "E6").Equals("ISBN13");
            sane = sane && ReadCellAsString(this._excel, "Book", "F6").Equals("Authors");
            sane = sane && ReadCellAsString(this._excel, "Book", "G6").Equals("Language");
            sane = sane && ReadCellAsString(this._excel, "Book", "H6").Equals("Tags");
            sane = sane && ReadCellAsString(this._excel, "Book", "I6").Equals("Dewey Decimal");
            sane = sane && ReadCellAsString(this._excel, "Book", "J6").Equals("MSRP");
            sane = sane && ReadCellAsString(this._excel, "Book", "K6").Equals("Publisher");
            sane = sane && ReadCellAsString(this._excel, "Book", "L6").Equals("Format");
            sane = sane && ReadCellAsString(this._excel, "Book", "M6").Equals("Date Published");
            sane = sane && ReadCellAsString(this._excel, "Book", "N6").Equals("Place of Publication");
            sane = sane && ReadCellAsString(this._excel, "Book", "O6").Equals("Edition");
            sane = sane && ReadCellAsString(this._excel, "Book", "P6").Equals("Pages");
            sane = sane && ReadCellAsString(this._excel, "Book", "Q6").Equals("Dimensions");
            sane = sane && ReadCellAsString(this._excel, "Book", "R6").Equals("Overview");
            sane = sane && ReadCellAsString(this._excel, "Book", "S6").Equals("Excerpt");
            sane = sane && ReadCellAsString(this._excel, "Book", "T6").Equals("Synopsys");
            sane = sane && ReadCellAsString(this._excel, "Book", "U6").Equals("Notes");
            if (!sane)
            {
                throw new FormatException("Provided Excel is not a valid books export from MyLibrary");
            }
        }//ctor

        public override IEnumerable<Book> Read(Action<int, int> progressCallback)
        {
            int importedCount = 0;
            int skippedCount = 0;

            ExcelAddressBase usedRange = this._excel.Workbook.Worksheets["Book"].Dimension;
            for (int index = HEADER_ROW + 1; index <= usedRange.End.Row; index++)
            {
                string idEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 1].GetValue<string>();
                string title = this._excel.Workbook.Worksheets["Book"].Cells[index, 2].GetValue<string>();
                string longTitle = this._excel.Workbook.Worksheets["Book"].Cells[index, 3].GetValue<string>();
                string isbnEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 4].GetValue<string>();
                string isbn13Entry = this._excel.Workbook.Worksheets["Book"].Cells[index, 5].GetValue<string>();
                string authorsEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 6].GetValue<string>();
                string languageEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 7].GetValue<string>();
                string tagsEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 8].GetValue<string>();
                string deweyDecimalEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 9].GetValue<string>();
                string msrpEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 10].GetValue<string>();
                string publisherEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 11].GetValue<string>();
                string formatEnry = this._excel.Workbook.Worksheets["Book"].Cells[index, 12].GetValue<string>();
                string datePublishedEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 13].GetValue<string>();
                string placeOfPublicationEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 14].GetValue<string>();
                string editionEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 15].GetValue <string>();
                string pagesEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 16].GetValue <string>();
                string dimensionsEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 17].GetValue<string>();
                string overviewEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 18].GetValue<string>();
                string excerptEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 19].GetValue<string>();
                string synopsysEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 20].GetValue<string>();
                string notesEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 21].GetValue<string>();

                Book book = new Book();
                BookBuilder builder = new BookBuilder();
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

                    List<string> authors = new List<string>();
                    if (!string.IsNullOrWhiteSpace(authorsEntry))
                    {
                        Regex authorSplitRegex = new Regex("; ");
                        string[] authorNames = authorSplitRegex.Split(authorsEntry);
                        foreach (var authorName in authorNames)
                        {
                            authors.Add(authorName);
                        }
                    }

                    book = builder
                        .WithTitles(title, longTitle)
                        .WithIsbns(isbnEntry, isbn13Entry)
                        .InLanguage(languageEntry)
                        .WithDeweyDecimal(deweyDecimalEntry)
                        .PublishedBy(publisherEntry)
                        .WithPages(pagesEntry)
                        .WithTags(tags)
                        .WithId(idEntry)
                        .Build();

                    book.Msrp = msrpEntry;
                    book.Format = formatEnry;
                    book.PlaceOfPublication = placeOfPublicationEntry;
                    book.DatePublished = datePublishedEntry;
                    book.Edition = editionEntry;
                    book.Dimensions = dimensionsEntry;
                    book.Overview = overviewEntry;
                    book.Excerpt = excerptEntry;
                    book.Synopsys = synopsysEntry;
                    book.Notes = notesEntry;

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

                yield return book;
            }
        }//Read
    }//class
}
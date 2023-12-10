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
    public class BookExcelReader : ExcelReaderBase<Book>, IBookExcelReader
    {
        public BookExcelReader(ExcelPackage excel, string worksheet, AppVersion runningVersion)
            : base(excel, worksheet, runningVersion)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // validate worksheet
            bool sane = true;
            sane = sane && Read("B2").Equals("Books");
            sane = sane && Read("A6").Equals("Id");
            sane = sane && Read("B6").Equals("Title");
            sane = sane && Read("C6").Equals("Long Title");
            sane = sane && Read("D6").Equals("ISBN");
            sane = sane && Read("E6").Equals("ISBN13");
            sane = sane && Read("F6").Equals("Authors");
            sane = sane && Read("G6").Equals("Language");
            sane = sane && Read("H6").Equals("Tags");
            sane = sane && Read("I6").Equals("Dewey Decimal");
            sane = sane && Read("J6").Equals("MSRP");
            sane = sane && Read("K6").Equals("Publisher");
            sane = sane && Read("L6").Equals("Format");
            sane = sane && Read("M6").Equals("Date Published");
            sane = sane && Read("N6").Equals("Place of Publication");
            sane = sane && Read("O6").Equals("Edition");
            sane = sane && Read("P6").Equals("Pages");
            sane = sane && Read("Q6").Equals("Dimensions");
            sane = sane && Read("R6").Equals("Overview");
            sane = sane && Read("S6").Equals("Excerpt");
            sane = sane && Read("T6").Equals("Synopsys");
            sane = sane && Read("U6").Equals("Notes");
            if (!sane)
            {
                throw new FormatException("Provided Excel is not a valid books export from MyLibrary");
            }
        }//ctor

        private string Read(int row, int column)
        {
            return ReadCellAsString(this._excel, "Book", row, column);
        }

        private string Read(string address)
        {
            return ReadCellAsString(this._excel, "Book", address);
        }

        public override IEnumerable<Book> Read(Action<int, int> progressCallback)
        {
            int parsedCount = 0;
            int skippedCount = 0;

            ExcelAddressBase usedRange = this._excel.Workbook.Worksheets["Book"].Dimension;
            for (int index = HEADER_ROW + 1; index <= usedRange.End.Row; index++)
            {
                string idEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 1].GetValue<string>();
                string title = this._excel.Workbook.Worksheets["Book"].Cells[index, 2].GetValue<string>();
                if (string.IsNullOrWhiteSpace(title))
                {
                    // stop at an empty row
                    break;
                }
                string longTitle = Read(index, 3);
                string isbnEntry = Read(index, 4);
                string isbn13Entry = Read(index, 5);
                string authorsEntry = Read(index, 6);
                string languageEntry = Read(index, 7);
                string tagsEntry = Read(index, 8);
                string deweyDecimalEntry = Read(index, 9);
                string msrpEntry = Read(index, 10);
                string publisherEntry = Read(index, 11);
                string formatEnry = Read(index, 12);
                string datePublishedEntry = Read(index, 13);
                string placeOfPublicationEntry = Read(index, 14);
                string editionEntry = Read(index, 15);
                string pagesEntry = Read(index, 16);
                string dimensionsEntry = Read(index, 17);
                string overviewEntry = Read(index, 18);
                string excerptEntry = Read(index, 19);
                string synopsysEntry = Read(index, 20);
                string notesEntry = Read(index, 21);

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

                yield return book;
            }
        }//Read
    }//class
}
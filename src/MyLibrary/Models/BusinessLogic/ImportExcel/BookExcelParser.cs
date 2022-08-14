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
    public class BookExcelParser : ExcelParserBase<Book>
    {
        public BookExcelParser(ExcelPackage excel, AppVersion runningVersion)
            :base(excel, "Book", runningVersion)
        {
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

        public override IEnumerable<ExcelRowResult> Run()
        {
            ExcelAddressBase usedRange = this._excel.Workbook.Worksheets["Book"].Dimension;
            for (int index = HEADER_ROW + 1; index <= usedRange.End.Row; index++)
            {
                // read Id
                int? id = null;
                string idEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 1].GetValue<string>();
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
                string title = this._excel.Workbook.Worksheets["Book"].Cells[index, 2].GetValue<string>();
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
                // read Long Title
                string longTitle = this._excel.Workbook.Worksheets["Book"].Cells[index, 3].GetValue<string>();
                // read ISBN10
                string isbnEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 4].GetValue<string>();
                if (!string.IsNullOrWhiteSpace(isbnEntry))
                {
                    if (!(Regex.IsMatch(isbnEntry, Book.ISBN_10_PATTERN)))
                    {
                        yield return new ExcelRowResult
                        {
                            Row = index,
                            Item = null,
                            Status = ExcelRowResultStatus.Error,
                            Message = "Invalid ISBN: " + isbnEntry
                        };

                        continue;
                    }
                }
                // read ISBN13
                string isbn13Entry = this._excel.Workbook.Worksheets["Book"].Cells[index, 5].GetValue<string>();
                if (!string.IsNullOrWhiteSpace(isbn13Entry))
                {
                    if (!(Regex.IsMatch(isbn13Entry, Book.ISBN_13_PATTERN)))
                    {
                        yield return new ExcelRowResult
                        {
                            Row = index,
                            Item = null,
                            Status = ExcelRowResultStatus.Error,
                            Message = "Invalid ISBN13: " + isbn13Entry
                        };

                        continue;
                    }
                }
                // read Authors
                string authorsEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 6].GetValue<string>();
                List<Author> authors = new List<Author>();
                if (!(string.IsNullOrWhiteSpace(authorsEntry)))
                {
                    // if author field not blank, must be in "Firstname Lastname; Firstname lastname; ... Firstname Lastname " format
                    const string AUTHORS_ENTRY_PATTERN = @"^([a-zA-Z]+ ([a-zA-Z]. )?[a-zA-Z]+; )*[a-zA-Z]+ ([a-zA-Z]. )?[a-zA-Z]+$";
                    if (!Regex.IsMatch(authorsEntry, AUTHORS_ENTRY_PATTERN))
                    {
                        yield return new ExcelRowResult
                        {
                            Row = index,
                            Item = null,
                            Status = ExcelRowResultStatus.Error,
                            Message = "Invalid Authors entry: " + authorsEntry
                        };

                        continue;
                    }
                    Regex authorsListSplitRegex = new Regex("; ");
                    string[] authorNames = authorsListSplitRegex.Split(authorsEntry);
                    {
                        foreach (var authorName in authorNames)
                        {
                            authors.Add(new Author(authorName));
                        }
                    }
                }
                // read Language
                string language = this._excel.Workbook.Worksheets["Book"].Cells[index, 7].GetValue<string>();
                if (string.IsNullOrWhiteSpace(language))
                {
                    yield return new ExcelRowResult
                    {
                        Row = index,
                        Item = null,
                        Status = ExcelRowResultStatus.Error,
                        Message = "Language cannot be empty"
                    };

                    continue;
                }
                // read Tags
                string tagsEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 8].GetValue<string>();
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
                // read Dewey Decimal
                string deweyDecimalEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 9].GetValue<string>();
                decimal? deweyDecimal = null;
                if (!(string.IsNullOrWhiteSpace(deweyDecimalEntry)))
                {
                    if (!(Regex.IsMatch(deweyDecimalEntry, Book.DEWEY_DECIMAL_PATTERN)))
                    {
                        yield return new ExcelRowResult
                        {
                            Row = index,
                            Item = null,
                            Status = ExcelRowResultStatus.Error,
                            Message = "Invalid Dewey Decimal: " + deweyDecimalEntry
                        };

                        continue;
                    }
                    else
                    {
                        deweyDecimal = decimal.Parse(deweyDecimalEntry);
                    }
                }
                // read MSRP
                string msrp = this._excel.Workbook.Worksheets["Book"].Cells[index, 10].GetValue<string>();
                // read Publisher
                string publisher = this._excel.Workbook.Worksheets["Book"].Cells[index, 11].GetValue<string>();
                if (string.IsNullOrWhiteSpace(publisher))
                {
                    yield return new ExcelRowResult
                    {
                        Row = index,
                        Item = null,
                        Status = ExcelRowResultStatus.Error,
                        Message = "Publisher cannot be empty"
                    };

                    continue;
                }
                // read Format
                string format = this._excel.Workbook.Worksheets["Book"].Cells[index, 12].GetValue<string>();
                // read Date Published
                string datePublished = this._excel.Workbook.Worksheets["Book"].Cells[index, 13].GetValue<string>();
                // read Place of Publication
                string placeOfPublication = this._excel.Workbook.Worksheets["Book"].Cells[index, 14].GetValue<string>();
                // read Edition
                string edition = this._excel.Workbook.Worksheets["Book"].Cells[index, 15].GetValue<string>();
                // read Pages
                string pagesEntry = this._excel.Workbook.Worksheets["Book"].Cells[index, 16].GetValue<string>();
                int pages;
                if (!int.TryParse(pagesEntry, out pages))
                {
                    yield return new ExcelRowResult
                    {
                        Row = index,
                        Item = null,
                        Status = ExcelRowResultStatus.Error,
                        Message = "Invalid Pages: " + pagesEntry
                    };

                    continue;
                }
                // read dimensions
                string dimensions = this._excel.Workbook.Worksheets["Book"].Cells[index, 17].GetValue<string>();
                // read overview
                string overview = this._excel.Workbook.Worksheets["Book"].Cells[index, 18].GetValue<string>();
                // read excerpt
                string excerpt = this._excel.Workbook.Worksheets["Book"].Cells[index, 19].GetValue<string>();
                // read synopsys
                string synopsys = this._excel.Workbook.Worksheets["Book"].Cells[index, 20].GetValue<string>();
                // read notes
                string notes = this._excel.Workbook.Worksheets["Book"].Cells[index, 21].GetValue<string>();

                // all ok
                // create the object
                Book book = new Book();
                if (id != null)
                    book.Id = (int)id;
                book.Title = title;
                book.TitleLong = longTitle;
                book.Isbn = isbnEntry;
                book.Isbn13 = isbn13Entry;
                book.Authors = authors;
                book.Language = language;
                book.Tags = tags;
                book.DeweyDecimal = deweyDecimal;
                book.Msrp = msrp;
                book.Publisher = new Publisher { Name = publisher };
                book.Format = format;
                book.DatePublished = datePublished;
                book.PlaceOfPublication = placeOfPublication;
                book.Edition = edition;
                book.Pages = pages;
                book.Dimensions = dimensions;
                book.Overview = overview;
                book.Excerpt = excerpt;
                book.Synopsys = synopsys;
                book.Notes = notes;
                yield return new ExcelRowResult
                {
                    Row = index,
                    Item = book,
                    Status = ExcelRowResultStatus.Success
                };
            }
        }//Run
    }//class
}
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

namespace MyLibrary.Views.Excel
{
    public class BooksExcel : ExcelBase<Book>
    {
        public BooksExcel(IExcelFile file)
            :base("Books", file)
        {
            WriteHeaderCell("Title", "B");
            WriteHeaderCell("Long Title", "C");
            WriteHeaderCell("ISBN", "D");
            WriteHeaderCell("ISBN13", "E");
            WriteHeaderCell("Authors", "F");
            WriteHeaderCell("Language", "G");
            WriteHeaderCell("Tags", "H");
            WriteHeaderCell("Publisher", "I");
            WriteHeaderCell("Format", "J");
            WriteHeaderCell("Date Published", "K");
            WriteHeaderCell("Place of Publication", "L");
            WriteHeaderCell("Edition", "M");
            WriteHeaderCell("Pages", "N");
            WriteHeaderCell("Dimensions", "O");
            WriteHeaderCell("Overview", "P");
            WriteHeaderCell("Excerpt", "Q");
            WriteHeaderCell("Synopsys", "R");
            WriteHeaderCell("Notes", "S");
        }

        public override void WriteEntity(Book entity)
        {
            object[] values = new object[]
            {
                entity.Id,
                entity.Title,
                entity.TitleLong,
                entity.Isbn,
                entity.Isbn13,
                entity.GetAuthorList(),
                entity.Language,
                entity.GetCommaDelimitedTags(),
                entity.Publisher.Name,
                entity.Format,
                entity.DatePublished,
                entity.PlaceOfPublication,
                entity.Edition,
                entity.Pages,
                entity.Dimensions,
                entity.Overview,
                entity.Excerpt,
                entity.Synopsys,
                entity.Notes
            };
            if (this._currRow % 2 == 0)
            {
                WriteEvenRow(this._currRow, values);
            }
            else
            {
                WriteOddRow(this._currRow, values);
            }
        }
    }
}

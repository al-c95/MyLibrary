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
                throw new FormatException("Provided Excel is not a valid export from MyLibrary");
            }
        }//ctor

        public override IEnumerable<ExcelRowResult> Run()
        {
            throw new NotImplementedException();
        }
    }//class
}

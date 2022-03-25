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
            // write headers
            this._ws.Cells["B6"].Value = "Title";
            this._ws.Cells["C6"].Value = "Long Title";
            this._ws.Cells["D6"].Value = "ISBN";
            this._ws.Cells["E6"].Value = "ISBN13";
            this._ws.Cells["F6"].Value = "Authors";
            this._ws.Cells["G6"].Value = "Language";
            this._ws.Cells["H6"].Value = "Tags";
            this._ws.Cells["I6"].Value = "Publisher";
            this._ws.Cells["J6"].Value = "Format";
            this._ws.Cells["K6"].Value = "Date Published";
            this._ws.Cells["L6"].Value = "Place of Publication";
            this._ws.Cells["M6"].Value = "Edition";
            this._ws.Cells["N6"].Value = "Pages";
            this._ws.Cells["O6"].Value = "Dimensions";
            this._ws.Cells["P6"].Value = "Overview";
            this._ws.Cells["Q6"].Value = "Excerpt";
            this._ws.Cells["R6"].Value = "Synopsys";
            this._ws.Cells["S6"].Value = "Notes";
        }

        public override void WriteEntity(Book entity)
        {
            this._ws.Cells[this._currRow, 1].Value = entity.Id;
            this._ws.Cells[this._currRow, 2].Value = entity.Title;
            this._ws.Cells[this._currRow, 3].Value = entity.TitleLong;
            this._ws.Cells[this._currRow, 4].Value = entity.Isbn;
            this._ws.Cells[this._currRow, 5].Value = entity.Isbn13;
            this._ws.Cells[this._currRow, 6].Value = entity.GetAuthorList();
            this._ws.Cells[this._currRow, 7].Value = entity.Language;
            this._ws.Cells[this._currRow, 8].Value = entity.GetCommaDelimitedTags();
            this._ws.Cells[this._currRow, 9].Value = entity.Publisher;
            this._ws.Cells[this._currRow, 10].Value = entity.Format;
            this._ws.Cells[this._currRow, 11].Value = entity.DatePublished;
            this._ws.Cells[this._currRow, 12].Value = entity.PlaceOfPublication;
            this._ws.Cells[this._currRow, 13].Value = entity.Edition;
            this._ws.Cells[this._currRow, 14].Value = entity.Pages;
            this._ws.Cells[this._currRow, 15].Value = entity.Dimensions;
            this._ws.Cells[this._currRow, 16].Value = entity.Overview;
            this._ws.Cells[this._currRow, 17].Value = entity.Excerpt;
            this._ws.Cells[this._currRow, 18].Value = entity.Synopsys;
            this._ws.Cells[this._currRow, 19].Value = entity.Notes;

            this._currRow++;
        }
    }
}

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
    public class MediaItemsExcel : ExcelBase<MediaItem>
    {
        public MediaItemsExcel(IExcelFile file)
            :base("Media Items", file)
        {
            // write headers
            this._ws.Cells["B6"].Value = "Title";
            this._ws.Cells["C6"].Value = "Type";
            this._ws.Cells["D6"].Value = "Number";
            this._ws.Cells["E6"].Value = "Running Time";
            this._ws.Cells["F6"].Value = "Release Year";
            this._ws.Cells["G6"].Value = "Tags";
            this._ws.Cells["H6"].Value = "Notes";
        }

        public override void WriteEntity(MediaItem entity)
        {
            this._ws.Cells[this._currRow, 1].Value = entity.Id;
            this._ws.Cells[this._currRow, 2].Value = entity.Title;
            this._ws.Cells[this._currRow, 3].Value = entity.Type.ToString();
            this._ws.Cells[this._currRow, 4].Value = entity.Number;
            this._ws.Cells[this._currRow, 5].Value = entity.RunningTime;
            this._ws.Cells[this._currRow, 6].Value = entity.ReleaseYear;
            this._ws.Cells[this._currRow, 7].Value = entity.GetCommaDelimitedTags();
            this._ws.Cells[this._currRow, 8].Value = entity.Notes;

            this._currRow++;
        }
    }
}

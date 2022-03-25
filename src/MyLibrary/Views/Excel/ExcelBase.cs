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
using OfficeOpenXml;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary.Views.Excel
{
    public abstract class ExcelBase<T> : IDisposable where T : Entity
    {
        protected ExcelPackage _pck;
        protected ExcelWorksheet _ws;
        protected int _currRow;

        protected readonly IExcelFile _file;

        public int CurrentRow => this._currRow;

        /// <summary>
        /// Constructor. Writes metadata.
        /// </summary>
        public ExcelBase(string type, IExcelFile file)
        {
            this._file = file;

            // create worksheet
            this._pck = new ExcelPackage();
            this._ws = this._pck.Workbook.Worksheets.Add(type);

            // write metadata
            this._ws.Cells["A1"].Value = "MyLibrary";
            this._ws.Cells[1, 1, 1, 2].Merge = true;
            this._ws.Cells["A2"].Value = "Type";
            this._ws.Cells["B2"].Value = type;
            this._ws.Cells["A3"].Value = "App Version:";
            this._ws.Cells["B3"].Value = Configuration.APP_VERSION.ToString();
            this._ws.Cells["A4"].Value = "Extracted At:";
            this._ws.Cells["B4"].Value = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            // write Id col header
            this._ws.Cells["A6"].Value = "Id";

            this._currRow = 7;
        }

        /// <summary>
        /// Writes a single entity as a new row in the worksheet.
        /// </summary>
        /// <param name="entity"></param>
        public abstract void WriteEntity(T entity);

        /// <summary>
        /// Performs final formatting and saves the file.
        /// </summary>
        public async Task SaveAsync()
        {
            await this._file.SaveAsAsync(this._pck);
            this.Dispose();
        }

        public void Dispose()
        {
            this._pck.Dispose();
        }
    }//class
}
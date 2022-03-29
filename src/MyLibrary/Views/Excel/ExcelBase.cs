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
using System.Drawing;
using OfficeOpenXml;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary.Views.Excel
{
    public abstract class ExcelBase<T> : IDisposable where T : Entity
    {
        protected ExcelPackage _pck;
        protected ExcelWorksheet _ws;
        protected readonly int HEADER_ROW = 6;
        protected int _currRow;

        protected readonly IExcelFile _file;

        protected readonly string HEADER_AND_META_STYLE = "Header";
        protected readonly string EVEN_ROW_STYLE = "Even";
        protected readonly string ODD_ROW_STYLE = "Odd";

        public int CurrentRow => this._currRow;

        /// <summary>
        /// Writes metadata and Id column header and prepares styles.
        /// </summary>
        public ExcelBase(string type, IExcelFile file)
        {
            this._file = file;

            // create worksheet
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            this._pck = new ExcelPackage();
            this._ws = this._pck.Workbook.Worksheets.Add(type);

            // create styles
            // headers and metadata
            var namedStyle = this._pck.Workbook.Styles.CreateNamedStyle(HEADER_AND_META_STYLE);
            namedStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            namedStyle.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 0, 0, 170));
            namedStyle.Style.Font.Bold = true;
            namedStyle.Style.Font.Color.SetColor(Color.FromArgb(255, 255, 255, 255));
            namedStyle.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            // even data row
            namedStyle = this._pck.Workbook.Styles.CreateNamedStyle(EVEN_ROW_STYLE);
            namedStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            namedStyle.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 239, 239, 255));
            // odd data row
            namedStyle = this._pck.Workbook.Styles.CreateNamedStyle(ODD_ROW_STYLE);
            namedStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            namedStyle.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 207, 207, 255));

            // write metadata
            this._ws.Cells["A1"].Value = "MyLibrary";
            this._ws.Cells[1, 1, 1, 2].Merge = true;
            this._ws.Cells["A2"].Value = "Type";
            this._ws.Cells["B2"].Value = type;
            this._ws.Cells["A3"].Value = "App Version:";
            this._ws.Cells["B3"].Value = Configuration.APP_VERSION.ToString();
            this._ws.Cells["A4"].Value = "Extracted At:";
            this._ws.Cells["B4"].Value = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            this._ws.Cells["A1:B4"].StyleName = HEADER_AND_META_STYLE;
            this._ws.Cells["A1:B4"].AutoFitColumns();
            // write Id col header
            WriteHeaderCell("Id", "A");

            // set current row to the first data row
            this._currRow = HEADER_ROW + 1;
        }//ctor

        /// <summary>
        /// Writes a single entity as a new row in the worksheet.
        /// </summary>
        /// <param name="entity"></param>
        public abstract void WriteEntity(T entity);

        protected void WriteHeaderCell(string text, string colLetter)
        {
            this._ws.Cells[colLetter + HEADER_ROW].Value = text;
            this._ws.Cells[colLetter + HEADER_ROW].StyleName = HEADER_AND_META_STYLE;
        }

        protected void WriteEvenRow(int row, object[] values)
        {
            WriteRow(row, values, EVEN_ROW_STYLE);
        }

        protected void WriteOddRow(int row, object[] values)
        {
            WriteRow(row, values, ODD_ROW_STYLE);
        }

        private void WriteRow(int row, object[] values, string styleName)
        {
            for (int i = 1; i <= values.Length; i++)
            {
                this._ws.Cells[row, i].Value = values[i - 1];
                this._ws.Cells[row, i].StyleName = styleName;
            }

            this._currRow++;
        }

        public void AutofitColumn(int col)
        {
            ExcelRange range = this._ws.Cells[1, col, this._currRow, col];
            range.AutoFitColumns();
        }

        public void WrapText(int col)
        {
            ExcelRange range = this._ws.Cells[1, col, this._currRow, col];
            range.Style.WrapText = true;
        }

        public void SetColumnWidth(int col, int width)
        {
            this._ws.Column(col).Width = width;
        }

        /// <summary>
        /// Performs final housekeeping and saves the file, then disposes the object.
        /// </summary>
        public async Task SaveAsync()
        {
            // TODO: protect sheet
            
            await this._file.SaveAsAsync(this._pck);
            this.Dispose();
        }

        public void Dispose()
        {
            this._pck.Dispose();
        }
    }//class
}
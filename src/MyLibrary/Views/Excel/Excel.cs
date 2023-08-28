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
using System.Threading.Tasks;
using OfficeOpenXml;

namespace MyLibrary.Views.Excel
{
    public class Excel : IDisposable
    {
        protected ExcelPackage _pck;
        protected ExcelWorksheet _ws;

        public ExcelPackage Package => this._pck;

        public ExcelWorksheet Worksheet
        {
            get => this._ws;
            set => this._ws = value;
        }

        /// <summary>
        /// Constructor. Creates worksheet and adds named styles.
        /// </summary>
        public Excel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            this._pck = new ExcelPackage();
        }//ctor

        public async Task SaveAsync(IExcelFile file, string path)
        {
            await file.SaveAsAsync(this._pck, path);
        }

        public void Dispose()
        {
            this._ws?.Dispose();
            this._pck?.Dispose();
        }
    }//class
}

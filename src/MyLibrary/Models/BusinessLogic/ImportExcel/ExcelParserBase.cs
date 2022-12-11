//MIT License

//Copyright (c) 2021-2022

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
using OfficeOpenXml;
using MyLibrary.Models.Entities;
using MyLibrary.Models.ValueObjects;

namespace MyLibrary.Models.BusinessLogic.ImportExcel
{
    /// <summary>
    /// Performs logic of parsing MyLibrary exports in Excel format.
    /// </summary>
    public abstract class ExcelParserBase<T> where T : Item
    {
        protected ExcelPackage _excel;

        protected const int HEADER_ROW = 6;
        protected readonly AppVersion VERSION_LIMIT = new AppVersion(2, 0, 0);

        /// <summary>
        /// </summary>
        /// <param name="excel"></param>
        /// <param name="runningVersion"></param>
        /// <param name="unitOfWorkProvider"</param>
        /// <throws>FormatException when metadata structure in worksheet incorrect, or app version mismatch.</throws>
        public ExcelParserBase(ExcelPackage excel, string worksheet, AppVersion runningVersion)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            this._excel = excel;

            // validate
            bool sane = true;
            sane = sane && ReadCellAsString(this._excel, worksheet, "A1").Equals("MyLibrary");
            sane = sane && ReadCellAsString(this._excel, worksheet, "A2").Equals("Type");
            sane = sane && ReadCellAsString(this._excel, worksheet, "A3").Equals("App Version:");
            AppVersion excelVersion = AppVersion.Parse(excel.Workbook.Worksheets[worksheet].Cells["B3"].GetValue<string>());
            if (!((excelVersion >= runningVersion) && (excelVersion <= VERSION_LIMIT)))
            {
                throw new FormatException("Version mismatch. Version " + excelVersion + " not supported.");
            }
            sane = sane && ReadCellAsString(this._excel, worksheet, "A4").Equals("Extracted At:");
            if (!sane)
            {
                throw new FormatException("Provided Excel is not a valid export from MyLibrary");
            }
        }//ctor

        public abstract IEnumerable<ExcelRowResult> Run();

        protected string ReadCellAsString(ExcelPackage pck, string worksheet, string address)
        {
            if (!pck.Workbook.Worksheets.Any(ws => ws.Name.Equals(worksheet)))
                throw new FormatException("Expected worksheet not found: " + worksheet);

            return pck.Workbook.Worksheets[worksheet].Cells[address].GetValue<string>();
        }
    }//class
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using OfficeOpenXml;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.BusinessLogic.ImportExcel;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;
namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests.ImportExcel_Tests
{
    public static class Utils
    {
        public static ExcelPackage MediaItemWorksheetFactory(string versionEntry, string type)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(type);
            // metadata
            ws.Cells["A1"].Value = "MyLibrary";
            ws.Cells["A2"].Value = "Type";
            ws.Cells["B2"].Value = type + "s";
            ws.Cells["A3"].Value = "App Version:";
            ws.Cells["A4"].Value = "Extracted At:";
            ws.Cells["B3"].Value = versionEntry;
            ws.Cells["B4"].Value = "Wednesday, 20 July 2022 23:06:34";
            // headers
            ws.Cells["A6"].Value = "Id";
            ws.Cells["B6"].Value = "Title";
            ws.Cells["C6"].Value = "Type";
            ws.Cells["D6"].Value = "Number";
            ws.Cells["E6"].Value = "Running Time";
            ws.Cells["F6"].Value = "Release Year";
            ws.Cells["G6"].Value = "Tags";
            ws.Cells["H6"].Value = "Notes";

            return pck;
        }

        public static ExcelPackage BookWorksheetFactory(string versionEntry)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Book");
            // metadata
            ws.Cells["A1"].Value = "MyLibrary";
            ws.Cells["A2"].Value = "Type";
            ws.Cells["B2"].Value = "Books";
            ws.Cells["A3"].Value = "App Version:";
            ws.Cells["A4"].Value = "Extracted At:";
            ws.Cells["B3"].Value = versionEntry;
            ws.Cells["B4"].Value = "Wednesday, 20 July 2022 23:06:34";
            // headers
            ws.Cells["A6"].Value = "Id";
            ws.Cells["B6"].Value = "Title";
            ws.Cells["C6"].Value = "Long Title";
            ws.Cells["D6"].Value = "ISBN";
            ws.Cells["E6"].Value = "ISBN13";
            ws.Cells["F6"].Value = "Authors";
            ws.Cells["G6"].Value = "Language";
            ws.Cells["H6"].Value = "Tags";
            ws.Cells["I6"].Value = "Dewey Decimal";
            ws.Cells["J6"].Value = "MSRP";
            ws.Cells["K6"].Value = "Publisher";
            ws.Cells["L6"].Value = "Format";
            ws.Cells["M6"].Value = "Date Published";
            ws.Cells["N6"].Value = "Place of Publication";
            ws.Cells["O6"].Value = "Edition";
            ws.Cells["P6"].Value = "Pages";
            ws.Cells["Q6"].Value = "Dimensions";
            ws.Cells["R6"].Value = "Overview";
            ws.Cells["S6"].Value = "Excerpt";
            ws.Cells["T6"].Value = "Synopsys";
            ws.Cells["U6"].Value = "Notes";

            return pck;
        }
    }//class
}
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
    }//class
}
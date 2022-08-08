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
    internal static class Utils
    {
        internal static ExcelPackage WorksheetFactory(string versionEntry, string type)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(type);
            ws.Cells["A1"].Value = "MyLibrary";
            ws.Cells["A2"].Value = "Type";
            ws.Cells["B2"].Value = type + "s";
            ws.Cells["A3"].Value = "App Version:";
            ws.Cells["A4"].Value = "Extracted At:";
            ws.Cells["B3"].Value = versionEntry;
            ws.Cells["B4"].Value = "Wednesday, 20 July 2022 23:06:34";

            return pck;
        }
    }//class
}
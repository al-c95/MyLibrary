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
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using OfficeOpenXml;
using MyLibrary.Views.Excel;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary.Presenters.Excel
{
    // https://stackoverflow.com/questions/31647268/strategy-pattern-with-strategies-contains-similar-code
    public abstract class ExcelPresenterBase : IDisposable
    {
        protected Views.Excel.Excel _excel;
        protected Views.IExportDialog _dialog;
        protected IExcelFile _file;

        public readonly string HEADER_AND_META_STYLE = "Header";
        public readonly string EVEN_ROW_STYLE = "Even";
        public readonly string ODD_ROW_STYLE = "Odd";
        public readonly int HEADER_ROW = 6;

        protected int _currRow;
        public int CurrentRow => this._currRow;

        protected readonly string _type;

        public bool IsRunning { get; set; }

        protected CancellationTokenSource _cts = null;

        /// <summary>
        /// Constructor. Writes metadata and Id column header to Excel.
        /// </summary>
        public ExcelPresenterBase(string type, IExcelFile file, Views.IExportDialog dialog)
        {
            Views.Excel.Excel excel = new Views.Excel.Excel();
            this._dialog = dialog;
            this._file = file;

            this._type = type;

            this.IsRunning = false;

            this._dialog.Title = "Export " + this._type + "s";
            this._dialog.Label1 = string.Empty;
            this._dialog.Label2 = string.Empty;
            this._dialog.StartButtonEnabled = false;

            // create styles
            // headers and metadata
            var namedStyle = excel.Package.Workbook.Styles.CreateNamedStyle(HEADER_AND_META_STYLE);
            namedStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            namedStyle.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 0, 0, 170));
            namedStyle.Style.Font.Bold = true;
            namedStyle.Style.Font.Color.SetColor(Color.FromArgb(255, 255, 255, 255));
            namedStyle.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            // even data row
            namedStyle = excel.Package.Workbook.Styles.CreateNamedStyle(EVEN_ROW_STYLE);
            namedStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            namedStyle.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 239, 239, 255));
            // odd data row
            namedStyle = excel.Package.Workbook.Styles.CreateNamedStyle(ODD_ROW_STYLE);
            namedStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            namedStyle.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 207, 207, 255));

            // write metadata
            excel.Worksheet = excel.Package.Workbook.Worksheets.Add(type);
            excel.Worksheet.Cells["A1"].Value = "MyLibrary";
            excel.Worksheet.Cells[1, 1, 1, 2].Merge = true;
            excel.Worksheet.Cells["A2"].Value = "Type";
            excel.Worksheet.Cells["B2"].Value = this._type + "s";
            excel.Worksheet.Cells["A3"].Value = "App Version:";
            excel.Worksheet.Cells["B3"].Value = Configuration.APP_VERSION.ToString();
            excel.Worksheet.Cells["A4"].Value = "Extracted At:";
            excel.Worksheet.Cells["B4"].Value = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            excel.Worksheet.Cells["A1:B4"].StyleName = HEADER_AND_META_STYLE;
            excel.Worksheet.Cells["A1:B4"].AutoFitColumns();
            this._excel = excel;
            // write Id column header
            WriteHeaderCell("A", "Id");
            // set the current row to the first data row, ready for writing entities
            this._currRow = HEADER_ROW + 1;

            // subscribe to dialog view's events
            this._dialog.BrowseButtonClicked += BrowseButtonClicked;
            this._dialog.StartButtonClicked += (async (sender, args) =>
            {
                await HandleStartButtonClicked(sender, args);
            });
            this._dialog.Cancelled += CancelButtonClicked;
        }

        public void BrowseButtonClicked(object sender, EventArgs e)
        {
            string exportPath = this._dialog.ShowFolderBrowserDialog(this._type);
            if (!string.IsNullOrWhiteSpace(exportPath))
            {
                this._dialog.Path = exportPath;
                this._dialog.StartButtonEnabled = true;
            }
            else
            {
                this._dialog.Path = string.Empty;
                this._dialog.StartButtonEnabled = false;
            }
        }

        #region UI view event handlers
        public async Task HandleStartButtonClicked(object sender, EventArgs args)
        {
            this._cts = new CancellationTokenSource();
            var token = this._cts.Token;

            this.IsRunning = true;
            this._dialog.CloseButtonEnabled = false;

            string path = this._dialog.Path;

            this._dialog.Label1 = "Exporting...";
            this._dialog.StartButtonEnabled = false;
            this._dialog.BrowseButtonEnabled = false;
            this._dialog.PathFieldEnabled = false;

            // do the work and save the file
            try
            {
                var numberExported = new Progress<int>(p =>
                {
                    this._dialog.Label2 = p + " rows exported";
                });

                await this.RenderExcel(numberExported, token);
            }
            catch (Exception ex)
            {
                this._dialog.Label1 = "Task aborted.";
                this._dialog.Label2 = "";

                if (!(ex is OperationCanceledException))
                {
                    // something bad happened
                    // tell the user
                    this._dialog.ShowErrorDialog(ex.Message);
                }
                // nothing more to do
                return;
            }
            finally
            {
                this.IsRunning = false;
                this._cts.Dispose();

                this._dialog.CloseButtonEnabled = true;
            }

            // finished
            this._dialog.Label1 = "Task complete.";
        }

        public void CancelButtonClicked(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                this._cts.Cancel();
            }
            else
            {
                this._dialog.CloseDialog();
                this.Dispose();
            }
        }
        #endregion

        protected abstract void WriteHeaders();

        protected void WriteHeaderCell(string col, string text)
        {
            this._excel.Worksheet.Cells[col + HEADER_ROW].Value = text;
            this._excel.Worksheet.Cells[col + HEADER_ROW].StyleName = HEADER_AND_META_STYLE;
        }

        protected void WriteEntityRow(object[] values)
        {
            for (int i = 1; i <= values.Length; i++)
            {
                this._excel.Worksheet.Cells[this._currRow, i].Value = values[i - 1];
                if (this._currRow % 2 == 0)
                {
                    this._excel.Worksheet.Cells[this._currRow, i].StyleName = EVEN_ROW_STYLE;
                }
                else
                {
                    this._excel.Worksheet.Cells[this._currRow, i].StyleName = ODD_ROW_STYLE;
                }
            }

            this._currRow++;
        }

        protected void AutoFitColumn(int col)
        {
            ExcelRange range = this._excel.Worksheet.Cells[1, col, this._currRow, col];
            range.AutoFitColumns();
        }

        protected void WrapText(int col)
        {
            ExcelRange range = this._excel.Worksheet.Cells[1, col, this._currRow, col];
            range.Style.WrapText = true;
        }

        protected void SetColumnWidth(int col, int width)
        {
            this._excel.Worksheet.Column(col).Width = width;
        }

        protected void SetWorksheetProtectionAttributes()
        {
            this._excel.Worksheet.Protection.IsProtected = true;
            this._excel.Worksheet.Protection.AllowAutoFilter = true;
            this._excel.Worksheet.Protection.AllowDeleteColumns = false;
            this._excel.Worksheet.Protection.AllowDeleteRows = true;
            this._excel.Worksheet.Protection.AllowEditObject = true;
            this._excel.Worksheet.Protection.AllowEditScenarios = true;
            this._excel.Worksheet.Protection.AllowFormatCells = true;
            this._excel.Worksheet.Protection.AllowFormatColumns = true;
            this._excel.Worksheet.Protection.AllowFormatRows = true;
            this._excel.Worksheet.Protection.AllowInsertColumns = false;
            this._excel.Worksheet.Protection.AllowInsertRows = true;
            this._excel.Worksheet.Protection.AllowPivotTables = false;
            this._excel.Worksheet.Protection.AllowSelectLockedCells = true;
            this._excel.Worksheet.Protection.AllowSelectUnlockedCells = true;
            this._excel.Worksheet.Protection.AllowSort = true;
            // additionally, don't allow the user to change sheet names
            this._excel.Package.Workbook.Protection.LockStructure = true;

            // set a random password so that *nobody* can edit the protected cells
            byte[] passwordBytes = new byte[32];
            System.Random rng = new Random();
            rng.NextBytes(passwordBytes);
            string passwordString = Convert.ToBase64String(passwordBytes);
            this._excel.Worksheet.Protection.SetPassword(passwordString);
        }

        protected void UnlockCell(int row, int col)
        {
            this._excel.Worksheet.Cells[row, col].Style.Locked = false;
        }

        /// <summary>
        /// Writes entities as rows to the Excel, then saves the file and disposes of the view.
        /// </summary>
        /// <param name="numberExported"></param>
        /// <returns></returns>
        protected abstract Task RenderExcel(IProgress<int> numberExported, CancellationToken token);

        public void Dispose()
        {
            this._excel?.Dispose();
        }
    }//class
}
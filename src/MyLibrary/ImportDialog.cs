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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.Models.BusinessLogic.ImportCsv;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ImportDialog : Form
    {
        private string _type;

        private CancellationTokenSource _cts = null;
        private bool _isRunning;

        public ImportDialog(string type)
        {
            InitializeComponent();

            this.CenterToParent();

            this.Text = "Import " + type + "s";
            this._type = type;

            this.label1.Text = "";
            this.label2.Text = "";

            this._isRunning = false;

            // prepare list column
            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Item";
            columnHeader1.Width = 435;
            this.listView.Columns.AddRange(new ColumnHeader[] { columnHeader1 });

            // register event handlers
            this.startButton.Click += (async (sender, args) =>
            {
                string filePath = null;
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "CSV files (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;

                    this.startButton.Enabled = false;
                    this.listView.Items.Clear();

                    dialog.Dispose();
                }
                else
                {
                    dialog.Dispose();
                    return;
                }

                this._cts = new CancellationTokenSource();
                var token = this._cts.Token;

                this._isRunning = true;

                try
                {
                    await Process(filePath, type, token);
                }
                catch (Exception ex)
                {
                    this.label1.Text = "Task aborted.";
                    this.label2.Text = "";

                    if (!(ex is OperationCanceledException))
                    {
                        // something bad happened
                        // tell the user
                        MessageBox.Show(ex.Message, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    this._isRunning = false;
                    this._cts.Dispose();

                    this.startButton.Enabled = false;
                }
            });
            this.cancelButton.Click += ((sender, args) =>
            {
                if (_isRunning)
                {
                    this._cts.Cancel();
                }
                else
                {
                    this.Close();
                }
            });
        }

        public async Task Process(string filePath, string type, CancellationToken token)
        {
            // read file
            CsvImport import = null;
            try
            {
                import = await CsvImport.ImportFactory(type, filePath);

                // check for cancellation
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error importing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.label1.Text = "Importing...";
            this.label2.Text = filePath;

            int warnCount = 0;
            int errorCount = 0;
            int successCount = 0;

            foreach (var row in import)
            {
                // check for cancellation
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                // process row
                int currRow = row.Row;
                if (row.RowStatus == CsvRowResult.Status.SUCCESS)
                {
                    try
                    {
                        if (await import.AddIfNotExists(row))
                        {
                            successCount++;
                        }
                        else
                        {
                            RegisterWarning(currRow, row.EntityName, import.GetTypeName);

                            warnCount++;
                        }
                    }
                    catch
                    {
                        // something bad happened while accessing the database
                        // register it as an error
                        // TODO: provide error message?
                        RegisterError(currRow);

                        errorCount++;
                    }
                }
                else if (row.RowStatus == CsvRowResult.Status.ERROR)
                {
                    // failed to parse row as the proper entity
                    // register it in the list
                    RegisterError(currRow);

                    errorCount++;
                }
            }
            
            // finished
            // display summary
            this.label1.Text = "Task Complete.";
            this.label2.Text = errorCount + " errors, " + warnCount + " warnings. " + successCount + " " + type + "s added.";
        }

        private void RegisterWarning(int row, string name, string type)
        {
            string message = "WARNING: " + type + " \"" + name + "\" in row " + row + " already exists.";
            AddListViewRow(message);
        }

        private void RegisterError(int row)
        {
            string message = "ERROR: Could not import row " + row;
            AddListViewRow(message);
        }

        private void AddListViewRow(string message)
        {
            string[] row = { message };
            ListViewItem listItem = new ListViewItem(row);
            this.listView.Items.Add(listItem);
        }
    }//class
}
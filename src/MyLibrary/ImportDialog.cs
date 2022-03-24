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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Csv;
using MyLibrary.Models.Entities;
using MyLibrary.Models.ValueObjects;

namespace MyLibrary
{
    public partial class ImportDialog : Form
    {
        public ImportDialog(string type)
        {
            InitializeComponent();

            this.CenterToParent();

            this.Text = "Import " + type;

            this.label1.Text = "";
            this.label2.Text = "";

            // register event handlers
            this.startButton.Click += (async (sender, args) =>
            {
                string filePath = null;
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "CSV files (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                }
                else
                {
                    return;
                }

                await Process(filePath);
            });
            this.cancelButton.Click += ((sender, args) =>
            {
                this.Close();
            });
        }

        public async Task Process(string filePath)
        {
            // read file
            CsvImport import = null;
            try
            {
                import = await TagCsvImport.BuildAsync(new CsvFile(filePath));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error importing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.startButton.Enabled = true;

            this.label1.Text = "Importing...";
            this.label2.Text = filePath;

            // prepare list column
            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Item";
            columnHeader1.Width = 435;
            this.listView.Columns.AddRange(new ColumnHeader[] { columnHeader1 });

            int warnCount = 0;
            int errorCount = 0;
            int successCount = 0;
            foreach (var row in import)
            {
                // process row
                int currRow = row.Row;
                if (row.RowStatus == CsvRowResult.Status.SUCCESS)
                {
                    try
                    {
                        if (await import.AddIfNotExists(row))
                        {
                            RegisterWarning(currRow, import.GetTypeName);

                            warnCount++;
                        }
                        else
                        {
                            successCount++;
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
                    // failed to parse row
                    // register it in the list
                    RegisterError(currRow);

                    errorCount++;
                }
            }

            // display summary
            this.label1.Text = "Task Complete.";
            this.label2.Text = errorCount + " errors, " + warnCount + " warnings. " + successCount + " tags added.";
        }

        private void RegisterWarning(int row, string name)
        {
            string message = "WARNING: \"" + name + "\" in row " + row + " already exists.";
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

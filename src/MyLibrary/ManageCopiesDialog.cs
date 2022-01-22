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
using MyLibrary.Views;
using MyLibrary.Models.Entities;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ManageCopiesDialog : Form
    {
        // TODO: factor logic out to presenter

        private Item _item;

        public ManageCopiesDialog(Item item)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            this._item = item;
            this.itemTitleLabel.Text = this._item.Title;

            this.saveChangesButton.Enabled = false;
            this.discardChangesButton.Enabled = false;
            this.saveNewCopyChangesButton.Enabled = false;
            this.deleteButton.Enabled = false;

            // register event handlers
            this.Load += (async (sender, args) =>
            {
                toolStripStatusLabel.Text = "Please Wait...";

                await LoadData(item);

                toolStripStatusLabel.Text = "Ready.";
            });
            this.dataGrid.SelectionChanged += ((sender, args) =>
            {
                if (dataGrid.SelectedRows.Count != 0)
                {
                    DataGridViewRow selectedRow = dataGrid.SelectedRows[0];

                    selectedCopyDescriptionField.Text = (string)selectedRow.Cells["Description"].Value;
                    selectedCopyNotesBox.Text = (string)selectedRow.Cells["Notes"].Value;

                    this.deleteButton.Enabled = true;
                }
                else
                {
                    selectedCopyDescriptionField.Clear();
                    selectedCopyNotesBox.Clear();

                    this.deleteButton.Enabled = false;
                }
            });
            this.saveNewCopyChangesButton.Click += (async (sender, args) =>
            {
                toolStripStatusLabel.Text = "Please Wait...";

                if (item.GetType() == typeof(Book))
                {
                    var copyService = new BookCopyService();

                    BookCopy copy = new BookCopy
                    {
                        BookId = this._item.Id,
                        Description = this.newCopyDescriptionField.Text,
                        Notes = this.newCopyNotesBox.Text
                    };

                    await copyService.Create(copy);
                }
                else if (item.GetType() == typeof(MediaItem))
                {
                    var copyService = new MediaItemCopyService();

                    MediaItemCopy copy = new MediaItemCopy
                    {
                        MediaItemId = this._item.Id,
                        Description = this.newCopyDescriptionField.Text,
                        Notes = this.newCopyNotesBox.Text
                    };

                    await copyService.Create(copy);
                }

                await LoadData(this._item);

                this.newCopyDescriptionField.Clear();
                this.newCopyNotesBox.Clear();

                toolStripStatusLabel.Text = "Ready.";
            });
            this.deleteButton.Click += (async (sender, args) =>
            {
                toolStripStatusLabel.Text = "Please Wait...";

                DataGridViewRow selectedRow = dataGrid.SelectedRows[0];
                int selectedId = int.Parse(selectedRow.Cells["Id"].Value.ToString());
                if (item.GetType() == typeof(Book))
                {
                    var copyService = new BookCopyService();
                    await copyService.DeleteById(selectedId);
                }
                else if (item.GetType() == typeof(MediaItem))
                {
                    var copyService = new MediaItemCopyService();
                    await copyService.DeleteById(selectedId);
                }

                await LoadData(this._item);

                toolStripStatusLabel.Text = "Ready.";
            });
            this.saveChangesButton.Click += (async (sender, args) => 
            {
                toolStripStatusLabel.Text = "Please Wait...";

                DataGridViewRow selectedRow = dataGrid.SelectedRows[0];
                int selectedId = int.Parse(selectedRow.Cells["Id"].Value.ToString());
                if (item.GetType() == typeof(Book))
                {
                    var copyService = new BookCopyService();

                    BookCopy copy = new BookCopy
                    {
                        Id = selectedId,
                        BookId = this._item.Id,
                        Description = this.selectedCopyDescriptionField.Text,
                        Notes = this.selectedCopyNotesBox.Text
                    };

                    await copyService.Update(copy);
                }
                else if (item.GetType() == typeof(MediaItem))
                {
                    var copyService = new MediaItemCopyService();

                    MediaItemCopy copy = new MediaItemCopy
                    {
                        Id = selectedId,
                        MediaItemId = this._item.Id,
                        Description = this.selectedCopyDescriptionField.Text,
                        Notes = this.selectedCopyNotesBox.Text
                    };

                    await copyService.Update(copy);
                }

                await LoadData(this._item);

                toolStripStatusLabel.Text = "Ready.";
            });
            this.discardChangesButton.Click += ((sender, args) =>
            {
                // TODO
                DataGridViewRow selectedRow = dataGrid.SelectedRows[0];
                int selectedId = int.Parse(selectedRow.Cells["Id"].Value.ToString());
                this.selectedCopyDescriptionField.Text = selectedRow.Cells["Description"].Value.ToString();
                this.selectedCopyNotesBox.Text = selectedRow.Cells["Notes"].Value.ToString();
            });
            this.newCopyNotesBox.TextChanged += NewCopyFieldsUpdated;
            this.newCopyDescriptionField.TextChanged += NewCopyFieldsUpdated;
            this.selectedCopyNotesBox.TextChanged += SelectedCopyFieldsUpdated;
            this.selectedCopyDescriptionField.TextChanged += SelectedCopyFieldsUpdated;
        }//ctor

        private void ResizeColumns()
        {
            this.dataGrid.Columns[0].Width = dataGrid.Width / 40;
        }

        private async Task LoadData(Item item)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Description");
            dt.Columns.Add("Notes");
            if (item.GetType() == typeof(Book))
            {
                var copyService = new BookCopyService();
                var allCopies = await copyService.GetByItemId(this._item.Id);

                foreach (var copy in allCopies)
                {
                    dt.Rows.Add(
                        copy.Id,
                        copy.Description,
                        copy.Notes
                        );
                }
            }
            else if (item.GetType() == typeof(MediaItem))
            {
                var copyService = new MediaItemCopyService();
                var allCopies = await copyService.GetByItemId(this._item.Id);

                foreach (var copy in allCopies)
                {
                    dt.Rows.Add(
                        copy.Id,
                        copy.Description,
                        copy.Notes
                        );
                }
            }
            this.dataGrid.DataSource = dt;
            this.dataGrid.Columns["Notes"].Visible = false;
            //ResizeColumns();
        }

        #region event handlers
        private void NewCopyFieldsUpdated(object sender, EventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(this.newCopyDescriptionField.Text))
            {
                this.saveNewCopyChangesButton.Enabled = true;
            }
            else
            {
                this.saveNewCopyChangesButton.Enabled = false;
            }
        }

        private void SelectedCopyFieldsUpdated(object sender, EventArgs args)
        {
            if (dataGrid.SelectedRows.Count != 0)
            {
                this.saveChangesButton.Enabled = true;
                this.discardChangesButton.Enabled = true;
            }
            else
            {
                this.saveChangesButton.Enabled = false;
                this.discardChangesButton.Enabled = false;
            }
        }
        #endregion
    }//class
}
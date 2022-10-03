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
    public partial class ManageCopiesDialog : Form, IManageCopiesForm
    {
        private readonly Item _item;

        public event EventHandler CopySelected;
        public event EventHandler SaveSelectedClicked;
        public event EventHandler DiscardChangesClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler SaveNewClicked;
        public event EventHandler NewCopyFieldsUpdated;
        public event EventHandler SelectedCopyFieldsUpdated;

        public string ItemTitleText
        {
            get => this.itemTitleLabel.Text;
            set => this.itemTitleLabel.Text = value;
        }

        public string SelectedDescription
        {
            get => this.selectedCopyDescriptionField.Text;
            set => this.selectedCopyDescriptionField.Text = value;
        }

        public string SelectedNotes
        {
            get => this.selectedCopyNotesBox.Text;
            set => this.selectedCopyNotesBox.Text = value; 
        }

        public string NewDescription 
        {
            get => this.newCopyDescriptionField.Text;
            set => this.newCopyDescriptionField.Text = value; 
        }

        public string NewNotes
        {
            get => this.newCopyNotesBox.Text;
            set => this.newCopyNotesBox.Text = value; 
        }

        public string StatusText
        {
            get => this.toolStripStatusLabel.Text;
            set => this.toolStripStatusLabel.Text = value; 
        }

        public Copy SelectedCopy
        {
            get
            {
                if (dataGrid.SelectedRows.Count == 0)
                    return null;

                DataGridViewRow selectedRow = dataGrid.SelectedRows[0];
                if (this._item.GetType() == typeof(Book))
                {
                    return new BookCopy { 
                        Id = int.Parse(selectedRow.Cells[0].Value.ToString()),
                        BookId = this._item.Id,
                        Description = selectedRow.Cells[1].Value.ToString(), 
                        Notes = selectedRow.Cells[2].Value.ToString()
                    };
                }
                else //if (this._item.GetType() == typeof(MediaItem))
                {
                    return new MediaItemCopy { 
                        Id = int.Parse(selectedRow.Cells[0].Value.ToString()),
                        MediaItemId = this._item.Id,
                        Description = selectedRow.Cells[1].Value.ToString(),
                        Notes = selectedRow.Cells[2].Value.ToString()
                    };
                }
            }
        }

        public Copy ModifiedSelectedCopy
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.selectedCopyDescriptionField.Text))
                    return null;

                DataGridViewRow selectedRow = dataGrid.SelectedRows[0];
                if (this._item.GetType() == typeof(Book))
                {
                    return new BookCopy
                    {
                        Id = int.Parse(selectedRow.Cells[0].Value.ToString()),
                        BookId = this._item.Id,
                        Description = this.selectedCopyDescriptionField.Text,
                        Notes = this.selectedCopyNotesBox.Text
                    };
                }
                else //if (this._item.GetType() == typeof(MediaItem))
                {
                    return new MediaItemCopy
                    {
                        Id = int.Parse(selectedRow.Cells[0].Value.ToString()),
                        MediaItemId = this._item.Id,
                        Description = this.selectedCopyDescriptionField.Text,
                        Notes = this.selectedCopyNotesBox.Text
                    };
                }
            }
        }

        public Copy NewCopy 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.newCopyDescriptionField.Text))
                {
                    return null;
                }
                else
                {
                    if (this._item.GetType() == typeof(Book))
                    {
                        return new BookCopy { 
                            Description = this.newCopyDescriptionField.Text, 
                            Notes = this.newCopyNotesBox.Text, 
                            BookId=this._item.Id 
                        };
                    }
                    else //if (this._item.GetType() == typeof(MediaItem))
                    {
                        return new MediaItemCopy { 
                            Description = this.newCopyDescriptionField.Text, 
                            Notes = this.newCopyNotesBox.Text, 
                            MediaItemId=this._item.Id
                        };
                    }
                }
            }
        }

        public bool SaveSelectedButtonEnabled 
        {
            get => this.saveChangesButton.Enabled;
            set => this.saveChangesButton.Enabled = value; 
        }

        public bool DeleteSelectedButtonEnabled
        {
            get => this.deleteButton.Enabled;
            set => this.deleteButton.Enabled = value; 
        }

        public bool DiscardChangesButtonEnabled
        {
            get => this.discardChangesButton.Enabled;
            set => this.discardChangesButton.Enabled = value; 
        }

        public bool SaveNewButtonEnabled
        {
            get => this.saveNewCopyChangesButton.Enabled;
            set => this.saveNewCopyChangesButton.Enabled = value; 
        }

        public int NumberCopiesSelected
        {
            get
            {
                return this.dataGrid.SelectedRows.Count;
            }
        }

        public bool SelectedDescriptionFieldEnabled
        {
            get => this.selectedCopyDescriptionField.Enabled;
            set => this.selectedCopyDescriptionField.Enabled = value; 
        }

        public bool SelectedNotesFieldEnabled
        {
            get => this.selectedCopyNotesBox.Enabled;
            set => this.selectedCopyNotesBox.Enabled = value; 
        }

        public ManageCopiesDialog(Item item)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            this._item = item;

            // register event handlers
            this.saveChangesButton.Click += ((sender, args) => 
            { 
                this.SaveSelectedClicked?.Invoke(this, args); 
            });
            this.discardChangesButton.Click += ((sender, args) => 
            { 
                this.DiscardChangesClicked?.Invoke(this, args); 
            });
            this.deleteButton.Click += ((sender, args) => 
            { 
                this.DeleteClicked?.Invoke(this, args); 
            });
            this.saveNewCopyChangesButton.Click += ((sender, args) => 
            { 
                this.SaveNewClicked?.Invoke(this, args); 
            });
            this.selectedCopyDescriptionField.TextChanged += ((sender, args) => 
            { 
                this.SelectedCopyFieldsUpdated?.Invoke(this, args); 
            });
            this.selectedCopyNotesBox.TextChanged += ((sender, args) => 
            { 
                this.SelectedCopyFieldsUpdated?.Invoke(this, args); 
            });
            this.newCopyDescriptionField.TextChanged += ((sender, args) => 
            { 
                this.NewCopyFieldsUpdated?.Invoke(this, args); 
            });
            this.newCopyNotesBox.TextChanged += ((sender, args) => 
            { 
                this.NewCopyFieldsUpdated?.Invoke(this, args); 
            });
            this.dataGrid.SelectionChanged += ((sender, args) => 
            { 
                this.CopySelected?.Invoke(this, args); 
            });

            this.CenterToParent();
        }//ctor

        private void ResizeColumns()
        {
            this.dataGrid.Columns[0].Width = dataGrid.Width / 40;
        }

        public void DisplayCopies(IEnumerable<Copy> copies)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Description");
            dt.Columns.Add("Notes");
            foreach (var copy in copies)
            {
                dt.Rows.Add(
                    copy.Id,
                    copy.Description,
                    copy.Notes
                    );
            }
            this.dataGrid.DataSource = dt;
            this.dataGrid.Columns["Notes"].Visible = false;
            //ResizeColumns();
        }
    }//class
}
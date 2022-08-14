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
using MyLibrary.Models.Entities;
using MyLibrary.Views;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class WishlistDialog : Form, IWishlistForm
    {
        public WishlistDialog()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;

            // populate "Types" combo box
            this.typesDropDown.Items.Add(ItemType.Book);
            this.typesDropDown.Items.Add(ItemType.Cd);
            this.typesDropDown.Items.Add(ItemType.Dvd);
            this.typesDropDown.Items.Add(ItemType.BluRay);
            this.typesDropDown.Items.Add(ItemType.Vhs);
            this.typesDropDown.Items.Add(ItemType.Vinyl);
            this.typesDropDown.Items.Add(ItemType.Other);
            this.typesDropDown.SelectedIndex = 0;

            // register event handlers
            this.saveChangesButton.Click += ((sender, args) =>
            {
                this.SaveSelectedClicked?.Invoke(sender, args);
            });
            this.dataGrid.SelectionChanged += ((sender, args) =>
            {
                this.ItemSelected?.Invoke(sender, args);
            });
            this.discardChangesButton.Click += ((sender, args) =>
            {
                this.DiscardChangesClicked?.Invoke(sender, args);
            });
            this.deleteButton.Click += ((sender, args) =>
            {
                this.DeleteClicked?.Invoke(sender, args);
            });
            this.saveNewItemChangesButton.Click += ((sender, args) =>
            {
                this.SaveNewClicked?.Invoke(sender, args);
            });
            this.newItemTitleField.TextChanged += ((sender, args) =>
            {
                this.NewItemFieldsUpdated?.Invoke(sender, args);
            });
            this.newItemNotesBox.TextChanged += ((sender, args) =>
            {
                this.NewItemFieldsUpdated?.Invoke(sender, args);
            });
            this.typesDropDown.SelectedIndexChanged += ((sender, args) =>
            {
                this.NewItemFieldsUpdated?.Invoke(sender, args);
            });
            this.selectedItemNotesBox.TextChanged += ((sender, args) =>
            {
                this.SelectedItemFieldsUpdated?.Invoke(sender, args);
            });
            this.Resize += ((sender, args) =>
            {
                ResizeColumns();
            });
        }

        public string SelectedNotes
        {
            get => this.selectedItemNotesBox.Text;
            set => this.selectedItemNotesBox.Text = value;
        }

        public string NewNotes
        {
            get => this.newItemNotesBox.Text;
            set => this.newItemNotesBox.Text = value;
        }

        public string StatusText
        {
            get => this.toolStripStatusLabel.Text;
            set => this.toolStripStatusLabel.Text = value;
        }

        public WishlistItem SelectedItem
        {
            get => GetItemInList(false);
        }

        public WishlistItem ModifiedItem
        {
            get => GetItemInList(true);
        }

        private WishlistItem GetItemInList(bool updatedNotes)
        {
            if (dataGrid.SelectedRows.Count == 0)
                return null;

            WishlistItem item = new WishlistItem();
            DataGridViewRow selectedRow = dataGrid.SelectedRows[0];
            ItemType type;
            Enum.TryParse(selectedRow.Cells["Type"].Value.ToString(), out type);
            item.Type = type;
            item.Id = int.Parse(selectedRow.Cells["Id"].Value.ToString());
            item.Title = selectedRow.Cells["Title"].Value.ToString();
            if (updatedNotes)
            {
                item.Notes = this.selectedItemNotesBox.Text;
            }
            else
            {
                item.Notes = selectedRow.Cells["Notes"].Value.ToString();
            }

            return item;
        }

        public WishlistItem NewItem 
        { 
            get
            {
                WishlistItem item = new WishlistItem();
                item.Type = this.NewItemType;
                item.Title = this.newItemTitleField.Text;
                item.Notes = this.newItemNotesBox.Text;

                return item;
            }
        }

        public string NewItemTitle
        {
            get => this.newItemTitleField.Text;
            set => this.newItemTitleField.Text = value;
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
            get => this.saveNewItemChangesButton.Enabled;
            set => this.saveNewItemChangesButton.Enabled = value;
        }

        public int NumberItemsSelected
        {
            get => this.dataGrid.SelectedRows.Count;
        }

        public ItemType NewItemType
        {
            get
            {
                ItemType type;
                Enum.TryParse(this.typesDropDown.SelectedItem.ToString(), out type);

                return type;
            }
        }

        public event EventHandler ItemSelected;
        public event EventHandler SaveSelectedClicked;
        public event EventHandler DiscardChangesClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler SaveNewClicked;
        public event EventHandler NewItemFieldsUpdated;
        public event EventHandler SelectedItemFieldsUpdated;

        public void DisplayItems(IEnumerable<WishlistItem> items)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Type");
            dt.Columns.Add("Title");
            dt.Columns.Add("Notes");
            foreach (var item in items)
            {
                dt.Rows.Add(
                    item.Id,
                    item.Type,
                    item.Title,
                    item.Notes
                    );
            }
            this.dataGrid.DataSource = dt;
            this.dataGrid.Columns["Notes"].Visible = false;

            ResizeColumns();
        }

        public void ShowItemAlreadyExistsDialog(string title)
        {
            MessageBox.Show("Item already exists", "Wishlist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ResizeColumns()
        {
            this.dataGrid.Columns["Id"].Width = this.dataGrid.Width / 8;
            this.dataGrid.Columns["Type"].Width = this.dataGrid.Width / 8;

            this.dataGrid.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }//class
}

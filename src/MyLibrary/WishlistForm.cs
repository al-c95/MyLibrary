//MIT License

//Copyright (c) 2021-2023

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
using System.Data;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.Models.Entities;
using MyLibrary.Views;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class WishlistForm : Form, IWishlistForm
    {
        public WishlistForm()
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
            this.typesDropDown.Items.Add("Flash Drive");
            this.typesDropDown.Items.Add("Floppy Disk");
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
            this.addToLibraryButton.Click += ((sender, args) =>
            {
                this.AddToLibraryClicked?.Invoke(sender, args);
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
            this.titleFilterField.TextChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.bookCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.dvdCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.cdCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.blurayCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.vhsCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.vinylCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.flashDriveCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.floppyDiskCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.otherCheckBox.CheckedChanged += (async (sender, args) =>
            {
                await ApplyFiltersWithDelay(sender, args);
            });
            this.clearTitleFilterButton.Click += ((sender, args) =>
            {
                this.titleFilterField.Text = String.Empty;
            });
            this.applyFiltersButton.Click += ((sender, args) =>
            {
                this.ApplyFilters?.Invoke(sender, args);
            });
            this.Resize += ((sender, args) =>
            {
                ResizeColumns();
            });

            this.CenterToParent();

            // set tab order
            this.selectedItemNotesBox.TabIndex = 0;
            this.saveChangesButton.TabIndex = 1;
            this.discardChangesButton.TabIndex = 2;
            this.deleteButton.TabIndex = 3;
            this.addToLibraryButton.TabIndex = 4;
            this.newItemTitleField.TabIndex = 0;
            this.newItemNotesBox.TabIndex = 1;
            this.typesDropDown.TabIndex = 2;
            this.saveNewItemChangesButton.TabIndex = 3;

            this.TitleFilterText = String.Empty;
            // select all categories for filter by default
            this.BookFilterSelected = true;
            this.DvdFilterSelected = true;
            this.CdFilterSelected = true;
            this.BlurayFilterSelected = true;
            this.VhsFilterSelected = true;
            this.VinylFilterSelected = true;
            this.FlashDriveFilterSelected = true;
            this.FloppyDiskFilterSelected = true;
            this.OtherFilterSelected = true;

            this.WindowCreated?.Invoke(null,null);
        }

        private async Task ApplyFiltersWithDelay(object sender, EventArgs args)
        {
            await Task.Delay(MainWindow.FILTER_DELAY);

            this.ApplyFilters?.Invoke(sender, args);
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
            ItemType type = Item.ParseType(selectedRow.Cells["Type"].Value.ToString());
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
                ItemType type = Item.ParseType(this.typesDropDown.SelectedItem.ToString());

                return type;
            }
        }

        public bool AddToLibraryButtonEnabled 
        {
            get => this.addToLibraryButton.Enabled;
            set => this.addToLibraryButton.Enabled = value;
        }

        public string TitleFilterText 
        {
            get => this.titleFilterField.Text;
            set => this.titleFilterField.Text = value;
        }

        public bool BookFilterSelected 
        {
            get => this.bookCheckBox.Checked;
            set => this.bookCheckBox.Checked = value;
        }

        public bool CdFilterSelected 
        {
            get => this.cdCheckBox.Checked;
            set => this.cdCheckBox.Checked = value; 
        }

        public bool DvdFilterSelected 
        {
            get => this.dvdCheckBox.Checked;
            set => this.dvdCheckBox.Checked = value;
        }

        public bool BlurayFilterSelected 
        {
            get => this.blurayCheckBox.Checked;
            set => this.blurayCheckBox.Checked = value; 
        }

        public bool VhsFilterSelected 
        {
            get => this.vhsCheckBox.Checked;
            set => this.vhsCheckBox.Checked = value; 
        }

        public bool VinylFilterSelected 
        {
            get => this.vinylCheckBox.Checked;
            set => this.vinylCheckBox.Checked = value;
        }

        public bool FloppyDiskFilterSelected 
        {
            get => this.floppyDiskCheckBox.Checked;
            set => this.floppyDiskCheckBox.Checked = value;
        }

        public bool FlashDriveFilterSelected 
        {
            get => this.flashDriveCheckBox.Checked;
            set => this.flashDriveCheckBox.Checked = value; 
        }

        public bool OtherFilterSelected 
        {
            get => this.otherCheckBox.Checked;
            set => this.otherCheckBox.Checked = value; 
        }

        public event EventHandler WindowCreated;
        public event EventHandler ApplyFilters;
        public event EventHandler ClearTitleFilterButtonClicked;
        public event EventHandler ItemSelected;
        public event EventHandler SaveSelectedClicked;
        public event EventHandler DiscardChangesClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler SaveNewClicked;
        public event EventHandler NewItemFieldsUpdated;
        public event EventHandler SelectedItemFieldsUpdated;
        public event EventHandler AddToLibraryClicked;

        public DataTable DisplayedItems
        {
            get
            {
                return this.dataGrid.DataSource as DataTable;
            }

            set
            {
                this.dataGrid.DataSource = value;
                this.dataGrid.Columns["Notes"].Visible = false;

                ResizeColumns();
            }
        }

        public void ShowItemAlreadyExistsDialog(string title)
        {
            MessageBox.Show("Item already exists", "Wishlist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ResizeColumns()
        {
            this.dataGrid.Columns["Id"].Width = 40;
            this.dataGrid.Columns["Type"].Width = 90;

            this.dataGrid.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }//class
}
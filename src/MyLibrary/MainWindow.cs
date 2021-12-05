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
    public partial class MainWindow : Form, IItemView
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Text = Configuration.APP_NAME + " " + Configuration.APP_VERSION;

            // populate "Category" combo box
            this.categoryDropDown.Items.Add(ItemType.Book);
            this.categoryDropDown.Items.Add("Media Items (all types)");
            this.categoryDropDown.Items.Add(ItemType.Cd);
            this.categoryDropDown.Items.Add(ItemType.Dvd);
            this.categoryDropDown.Items.Add(ItemType.BluRay);
            this.categoryDropDown.Items.Add(ItemType.Vhs);
            this.categoryDropDown.Items.Add(ItemType.Vinyl);
            this.categoryDropDown.Items.Add(ItemType.Other);

            // register event handlers
            this.exitMenuItem.Click += ((sender, args) => Application.Exit());
            this.addButton.Click += ((sender, args) =>
            {
                switch (this.categoryDropDown.SelectedIndex)
                {
                    case 0:
                        new AddNewBookForm().ShowDialog();
                        break;
                    default:
                        new AddNewMediaItemForm().ShowDialog();
                        break;
                }
            });
            this.categoryDropDown.SelectedIndexChanged += ((sender, args) => 
            {
                // fire the public event so the subscribed presenter can react
                CategorySelectionChanged?.Invoke(sender, args);
            });
            this.titleFilterField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                FiltersUpdated?.Invoke(sender, args);
            });
            this.dataGrid.SelectionChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                ItemSelectionChanged?.Invoke(sender, args);
            });
            this.textBoxNotes.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                SelectedItemModified?.Invoke(sender, args);

                this._selectedItem.Notes = this.textBoxNotes.Text;
            });
            this.discardChangesButton.Click += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                DiscardSelectedItemChangesButtonClicked?.Invoke(sender, args);
            });
            this.deleteSelectedButton.Click += ((sender, args) =>
            {
                // user confirmation dialog
                var confirmResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.No)
                {
                    return;
                }

                // fire the public event so the subscribed presenter can react
                this.DeleteButtonClicked?.Invoke(this, args);
            });
            this.clearFilterButton.Click += ((sender, args) =>
            {
                this.TitleFilterText = null;
            });
            this.applyFilterButton.Click += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                this.ApplyFilterButtonClicked?.Invoke(this, args);
            });
            this.saveChangesButton.Click += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                this.UpdateSelectedItemButtonClicked?.Invoke(this, args);
            });

            // select viewing books by default
            CategorySelectionChanged?.Invoke(this, null);
        }//ctor

        public string TitleFilterText
        {
            get => this.titleFilterField.Text;
            set => this.titleFilterField.Text = value;
        }

        public int SelectedItemId
        {
            get
            {
                if (this.dataGrid.SelectedRows.Count == 0)
                    return 0;

                // this is always an integer in the first col
                return int.Parse(this.dataGrid.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get => this._selectedItem;
            set
            {
                this._selectedItem = value;

                this.textBoxNotes.Text = this._selectedItem.Notes;
            } 
        }

        public DataTable DisplayedItems
        {
            get => (DataTable)this.dataGrid.DataSource;
            set => this.dataGrid.DataSource = value;
        }

        public int CategoryDropDownSelectedIndex
        {
            get => this.categoryDropDown.SelectedIndex;
            set => this.categoryDropDown.SelectedIndex = value;
        }

        public string StatusText 
        {
            get => this.statusLabel.Text;
            set => this.statusLabel.Text = value; 
        }

        public string ItemsDisplayedText
        {
            get => this.itemsDisplayedLabel.Text;
            set => this.itemsDisplayedLabel.Text = value;
        }

        public bool UpdateSelectedItemButtonEnabled
        {
            get => this.saveChangesButton.Enabled;
            set => this.saveChangesButton.Enabled = value;
        }

        public bool DiscardSelectedItemChangesButtonEnabled
        {
            get => this.discardChangesButton.Enabled;
            set => this.discardChangesButton.Enabled = value;
        }

        #region events
        public event EventHandler ItemSelectionChanged;
        public event EventHandler CategorySelectionChanged;
        public event EventHandler FiltersUpdated;
        public event EventHandler ApplyFilterButtonClicked;
        public event EventHandler DeleteButtonClicked;
        public event EventHandler UpdateSelectedItemButtonClicked;
        public event EventHandler SelectedItemModified;
        public event EventHandler DiscardSelectedItemChangesButtonClicked;
        #endregion
    }//class
}
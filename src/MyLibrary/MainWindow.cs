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
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using MyLibrary.Presenters;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class MainWindow : Form, IItemView
    {
        public static readonly int FILTER_DELAY = 2000; // millis

        private bool Image;

        public MainWindow()
        {
            InitializeComponent();

            this.Text = Configuration.APP_NAME + " " + Configuration.APP_VERSION;

            Image = false;

            // populate "Category" combo box
            this.categoryDropDown.Items.Add(ItemType.Book);
            this.categoryDropDown.Items.Add("Media Items (all types)");
            this.categoryDropDown.Items.Add(ItemType.Cd);
            this.categoryDropDown.Items.Add(ItemType.Dvd);
            this.categoryDropDown.Items.Add(ItemType.BluRay);
            this.categoryDropDown.Items.Add(ItemType.Vhs);
            this.categoryDropDown.Items.Add(ItemType.Vinyl);
            this.categoryDropDown.Items.Add(ItemType.Other);

            this.tagsList.CheckOnClick = true;

            this.detailsBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            this.detailsBox.Text = "Item";

            // register event handlers
            this.exitMenuItem.Click += ((sender, args) => Application.Exit());
            this.searchBooksButton.Click += ((sender, args) =>
            {
                SearchByIsbnClicked?.Invoke(sender, args);
            });
            this.Resize += ((sender, args) =>
            {
                ResizeColumns();
            });
            this.aboutToolStripMenuItem.Click += ((sender, args) =>
            {
                new AboutBox().ShowDialog();
            });
            // fire the public event so the subscribed present can react
            this.databaseStatisticsToolStripMenuItem.Click += ((sender, args) =>
            {
                ShowStatsClicked?.Invoke(sender, args);
            });
            this.newBookToolStripMenuItem.Click += ((sender, args) =>
            {
                AddNewBookClicked?.Invoke(sender, args);
            });
            this.newMediaItemToolStripMenuItem.Click += ((sender, args) =>
            {
                AddNewMediaItemClicked?.Invoke(sender, args);
            });
            this.addButton.Click += ((sender, args) =>
            {
                if (this.categoryDropDown.SelectedIndex == 0)
                {
                    AddNewBookClicked?.Invoke(sender, args);
                }
                else
                {
                    AddNewMediaItemClicked?.Invoke(sender, args);
                }
            });
            this.categoryDropDown.SelectedIndexChanged += ((sender, args) =>
            {
                CategorySelectionChanged?.Invoke(sender, args);
            });
            this.titleFilterField.TextChanged += (async (sender, args) =>
            {
                await Task.Delay(FILTER_DELAY);

                FiltersUpdated?.Invoke(sender, args);
            });
            this.dataGrid.SelectionChanged += ((sender, args) =>
            {
                ItemSelectionChanged?.Invoke(sender, args);
            });
            this.textBoxNotes.TextChanged += ((sender, args) =>
            {
                SelectedItemModified?.Invoke(sender, args);

                this._selectedItem.Notes = this.textBoxNotes.Text;
            });
            this.discardChangesButton.Click += ((sender, args) =>
            {
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

                this.DeleteButtonClicked?.Invoke(this, args);
            });
            this.clearFilterButton.Click += ((sender, args) =>
            {
                // clear title filter field
                this.TitleFilterText = null;
                // uncheck filter tags
                while (this.tagsList.CheckedIndices.Count > 0)
                {
                    this.tagsList.SetItemChecked(this.tagsList.CheckedIndices[0], false);
                }
            });
            this.applyFilterButton.Click += ((sender, args) =>
            {
                this.ApplyFilterButtonClicked?.Invoke(this, args);
            });
            this.saveChangesButton.Click += ((sender, args) =>
            {
                this.UpdateSelectedItemButtonClicked?.Invoke(this, args);
            });
            this.selectImageButton.Click += ((sender, args) =>
            {
                // load image from file
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = LOAD_IMAGE_DIALOG_TITLE;
                    dialog.Filter = LOAD_IMAGE_DIALOG_FILTER;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        byte[] bytes = File.ReadAllBytes(dialog.FileName);
                        this.pictureBox.Image = ReadImage(bytes);
                        Image = true;

                        SelectedItemModified?.Invoke(sender, args);
                    }
                }
            });
            this.removeImageButton.Click += ((sender, args) =>
            {
                SelectedItemModified?.Invoke(sender, args);

                SetNoItemImage();
            });
            this.tagsButton.Click += (async (sender, args) =>
            {
                var form = await ManageTagsForm.CreateAsync();
                form.TagsUpdated += ((s, a) =>
                {
                    this.TagsUpdated?.Invoke(s, a);
                });
                form.ShowDialog();
            });
            this.manageItemTagsButton.Click += (async (sender, args) =>
            {
                var form = await ManageTagsForItemDialog.CreateAsync(this.SelectedItem);
                form.TagsUpdated += ((s, a) =>
                {
                    this.TagsUpdated?.Invoke(s, a);
                });
                form.ShowDialog();
            });
            this.manageItemCopiesButton.Click += (async (sender, args) =>
            {
                var form = new ManageCopiesDialog(this.SelectedItem);
                ManageCopiesPresenter presenter = new ManageCopiesPresenter(form, this._selectedItem, new CopyServiceFactory());
                await presenter.LoadData(sender, args);
                form.ShowDialog();
            });
            this.wishlistButton.Click += (async (sender, args) =>
            {
                var form = new WishlistDialog();
                WishlistPresenter presenter = new WishlistPresenter(form, new WishlistServiceProvider());
                await presenter.LoadData();
                form.ShowDialog();
            });
            this.tagsList.ItemCheck += (async (sender, args) =>
            {
                await Task.Delay(FILTER_DELAY);

                FiltersUpdated?.Invoke(sender, args);
            });

            LoadWindow();
        }//ctor

        public static readonly string LOAD_IMAGE_DIALOG_TITLE = "Load Image";
        public static readonly string LOAD_IMAGE_DIALOG_FILTER = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

        private void ResizeColumns()
        {
            // resize DataGridView columns nicely
            this.dataGrid.Columns[0].Width = this.dataGrid.Width / 20; // Id
            this.dataGrid.Columns[1].Width = this.dataGrid.Width / 4; // title
            if (this.categoryDropDown.SelectedIndex == 0)
            {
                // books displayed
                this.dataGrid.Columns[2].Width = this.dataGrid.Width / 9; // ISBN
            }
            else
            {
                // media items displayed
            }
        }

        public void LoadWindow()
        {
            // select viewing books by default
            this.CategoryDropDownSelectedIndex = 1;
            CategorySelectionChanged?.Invoke(this, null);
        }

        public void PopulateFilterTags(Dictionary<string,bool> tagNamesAndCheckedStatuses)
        {
            this.tagsList.Items.Clear();

            foreach (var kvp in tagNamesAndCheckedStatuses)
            {
                string tagName = kvp.Key;
                bool isChecked = kvp.Value;

                this.tagsList.Items.Add(tagName, isChecked);
            }
        }

        public int NumberOfItemsSelected => this.dataGrid.SelectedRows.Count;

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
            get
            {
                Item temp = _selectedItem;
                temp.Notes = this.textBoxNotes.Text;
                if (this.pictureBox.Image != null)
                {
                    if (Image)
                    {
                        temp.Image = WriteImage(this.pictureBox.Image, this.pictureBox.Image.RawFormat);
                    }
                    else
                    {
                        temp.Image = null;
                    }
                }
                else
                {
                    temp.Image = null;
                }
                
                return temp;
            }

            set
            {
                this.manageItemTagsButton.Enabled = !(value is null);
                this.selectImageButton.Enabled = !(value is null);
                this.removeImageButton.Enabled = !(value is null);
                this.saveChangesButton.Enabled = !(value is null);
                this.discardChangesButton.Enabled = !(value is null);
                this.notesLabel.Enabled = !(value is null);
                this.textBoxNotes.Enabled = !(value is null);
                this.manageItemCopiesButton.Enabled = !(value is null);

                if (value is null)
                {
                    this.textBoxNotes.Clear();
                    this.detailsBox.Clear();
                    this.pictureBox.Image = null;
                    Image = false;

                    return;
                }

                this._selectedItem = value;
                this.textBoxNotes.Text = this._selectedItem.Notes;
                Image itemImage = ReadImage(this._selectedItem.Image);
                if (itemImage is null)
                {
                    SetNoItemImage();
                }
                else
                {
                    this.pictureBox.Image = ReadImage(this._selectedItem.Image);
                    Image = true;
                }
            } 
        }

        private void SetNoItemImage()
        {
            this.pictureBox.Image = ReadImage(File.ReadAllBytes("no_image.png"));
            Image = false;
        }

        public DataTable DisplayedItems
        {
            get => (DataTable)this.dataGrid.DataSource;
            set
            {
                // handle DataGridView memory leak
                this.dataGrid.DataSource = null;
                this.dataGrid.Rows.Clear();
                GC.Collect();

                this.dataGrid.DataSource = value;

                ResizeColumns();
            }
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

        public IEnumerable<string> SelectedFilterTags
        {
            get
            {
                foreach (var tag in this.tagsList.CheckedItems)
                    yield return tag.ToString();
            }
        }

        public bool IsItemSelected
        {
            get => this.dataGrid.SelectedRows.Count != 0;
        }

        public bool DeleteItemButtonEnabled
        {
            get => this.deleteSelectedButton.Enabled;
            set => this.deleteSelectedButton.Enabled = value;
        }

        readonly string[] FIELD_NAME_PATTERNS = new string[]
                {
                    "Id: ",
                    "Title: ",
                    "Type: ",
                    "ISBN: ",
                    "ISBN 13: ",
                    "Long Title: ",
                    "Dewey Decimal: ",
                    "Format: ",
                    "Publisher: ",
                    "Date Published: ",
                    "Place of Publication: ",
                    "Edition: ",
                    "Pages: ",
                    "Dimensions: ",
                    "Overview: ",
                    "Language: ",
                    "MSRP: ",
                    "Excerpt: ",
                    "Synopsys: ",
                    "Authors: ",
                    "Number: ",
                    "Running Time: ",
                    "Release Year: "
                };
        public string SelectedItemDetailsBoxEntry
        {
            get => this.detailsBox.Text;

            set
            {
                // set text
                this.detailsBox.Text = value;

                // make field names bold
                Application.DoEvents();
                foreach (var pattern in FIELD_NAME_PATTERNS)
                {
                    foreach (string line in this.detailsBox.Lines)
                    {
                        int start = this.detailsBox.Find(pattern);
                        if (start >= 0)
                        {
                            this.detailsBox.Select(start, pattern.Length);
                            this.detailsBox.SelectionFont = new Font(this.detailsBox.Font, FontStyle.Bold);
                        }
                    }//foreach
                }//foreach
            }//set
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
        public event EventHandler TagsUpdated;
        public event EventHandler AddNewMediaItemClicked;
        public event EventHandler AddNewBookClicked;
        public event EventHandler SearchByIsbnClicked;
        public event EventHandler ShowStatsClicked;
        #endregion

        public void ShowErrorDialog(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Image ReadImage(byte[] bytes)
        {
            if (bytes is null)
                return null;

            MemoryStream stream = new MemoryStream(bytes, 0, bytes.Length);
            stream.Write(bytes, 0, bytes.Length);
            Image image = new Bitmap(stream);

            return image;
        }

        private byte[] WriteImage(Image image, ImageFormat format)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, format);
                byte[] bytes = stream.ToArray();

                return bytes;
            }
        }//WriteImage
    }//class
}
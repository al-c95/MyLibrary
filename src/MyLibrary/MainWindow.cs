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
    public partial class MainWindow : Form, IMainWindow
    {
        public static readonly int FILTER_DELAY = 2000; // millis

        private bool Image;

        public bool ItemDetailsSpinner
        {
            get => this.itemDetailsSpinner.Visible;
            set
            {
                this.itemDetailsSpinner.Visible = value;

                this.manageItemTagsButton.Visible = !value;
                this.manageItemCopiesButton.Visible = !value;
                this.pictureBox.Visible = !value;
                this.selectImageButton.Visible = !value;
                this.removeImageButton.Visible = !value;
                this.notesLabel.Visible = !value;
                this.textBoxNotes.Visible = !value;
                this.saveChangesButton.Visible = !value;
                this.discardChangesButton.Visible = !value;
                this.detailsBox.Visible = !value;
            }
        }

        public bool FilterGroupEnabled
        {
            get => this.filterGroup.Enabled;
            set => this.filterGroup.Enabled = value;
        }

        public bool CategoryGroupEnabled
        {
            get => this.categoryDropDown.Enabled && this.categoryLabel.Enabled;
            set
            {
                this.categoryDropDown.Enabled = value;
                this.categoryLabel.Enabled = value;
            }
        }

        public bool AddItemEnabled 
        {
            get => this.addButton.Enabled && this.newBookToolStripMenuItem.Enabled && this.newMediaItemToolStripMenuItem.Enabled;
            set 
            {
                this.addButton.Enabled = value;
                this.newBookToolStripMenuItem.Enabled = value;
                this.newMediaItemToolStripMenuItem.Enabled = value;
            }
        }

        public bool SearchBooksButtonEnabled 
        {
            get => this.searchBooksButton.Enabled;
            set => this.searchBooksButton.Enabled = value;
        }

        public bool TagsButtonEnabled 
        {
            get => this.tagsButton.Enabled;
            set => this.tagsButton.Enabled = value;
        }

        public bool WishlistButtonEnabled 
        {
            get => this.wishlistButton.Enabled;
            set => this.wishlistButton.Enabled = value;
        }

        public bool ViewStatisticsEnabled 
        {
            get => this.databaseStatisticsToolStripMenuItem.Enabled;
            set => this.databaseStatisticsToolStripMenuItem.Enabled = value;
        }

        public bool ShowAboutBoxEnabled 
        {
            get => this.aboutToolStripMenuItem.Enabled;
            set => this.aboutToolStripMenuItem.Enabled = value;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.Text = Configuration.APP_NAME + " " + Configuration.APP_VERSION;
            
            // show the spinner
            // but the other controls are visible while loading the form, so hide them for now
            this.ItemDetailsSpinner = false;
            this.manageItemTagsButton.Visible = false;
            this.manageItemCopiesButton.Visible = false;
            this.pictureBox.Visible = false;
            this.selectImageButton.Visible = false;
            this.removeImageButton.Visible = false;
            this.notesLabel.Visible = false;
            this.textBoxNotes.Visible = false;
            this.saveChangesButton.Visible = false;
            this.discardChangesButton.Visible = false;
            this.detailsBox.Visible = false;

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
            this.booksToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 0);
            this.mediaItemsAllCategoriesToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 1);
            this.cdsToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 2);
            this.dvdsToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 3);
            this.bluRaysToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 4);
            this.vhssToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 5);
            this.vinylsToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 6);
            this.otherToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 7);
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
                        SetItemImage(ReadImage(bytes));

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
            this.wishlistButton.Click += ((sender, args) =>
            {
                this.WishlistButtonClicked?.Invoke(sender, args);
            });
            this.tagsList.ItemCheck += (async (sender, args) =>
            {
                await Task.Delay(FILTER_DELAY);

                FiltersUpdated?.Invoke(sender, args);
            });
            // import CSV
            this.importTagsCsvToolStripMenuItem.Click += ((sender, args) =>
            {
                ImportDialog dialog = new ImportDialog("tag");
                dialog.ShowDialog();

                this.TagsUpdated?.Invoke(sender, args);
            });
            this.importAuthorsCsvToolStripMenuItem.Click += ((sender, args) =>
            {
                ImportDialog dialog = new ImportDialog("author");
                dialog.ShowDialog();
            });
            this.importPublishersCsvToolStripMenuItem.Click += ((sender, args) =>
            {
                ImportDialog dialog = new ImportDialog("publisher");
                dialog.ShowDialog();
            });
            // export XLSX
            this.tagsToolStripMenuItem.Click += ((sender, args) =>
            {
                ExportDialog dialog = new ExportDialog();
                Presenters.Excel.TagExcelPresenter presenter = new Presenters.Excel.TagExcelPresenter(dialog);
                dialog.ShowDialog();
            });
            this.booksToolStripMenuItem1.Click += ((sender, args) =>
            {
                ExportDialog dialog = new ExportDialog();
                Presenters.Excel.BookExcelPresenter presenter = new Presenters.Excel.BookExcelPresenter(dialog);
                dialog.ShowDialog();
            });
            this.authorsToolStripMenuItem.Click += ((sender, args) =>
            {
                ExportDialog dialog = new ExportDialog();
                Presenters.Excel.AuthorExcelPresenter presenter = new Presenters.Excel.AuthorExcelPresenter(dialog);
                dialog.ShowDialog();
            });
            this.mediaItemsToolStripMenuItem.Click += ((sender, args) =>
            {
                ExportDialog dialog = new ExportDialog();
                Presenters.Excel.MediaItemExcelPresenter presenter = new Presenters.Excel.MediaItemExcelPresenter(dialog);
                dialog.ShowDialog();
            });
            this.publishersToolStripMenuItem.Click += ((sender, args) =>
            {
                ExportDialog dialog = new ExportDialog();
                Presenters.Excel.PublisherExcelPresenter presenter = new Presenters.Excel.PublisherExcelPresenter(dialog);
                dialog.ShowDialog();
            });
            this.exportWishlistMenuItem.Click += ((sender, args) =>
            {
                ExportDialog dialog = new ExportDialog();
                Presenters.Excel.WishlistExcelPresenter presenter = new Presenters.Excel.WishlistExcelPresenter(dialog);
                dialog.ShowDialog();
            });
            // select viewing books by default
            this.categoryDropDown.SelectedIndex = 0;

            SetItemDetailsSpinnerPos();
            this.itemDetailsSpinner.OuterMargin = 0;
            this.itemDetailsSpinner.OuterWidth = 6;
            this.itemDetailsSpinner.ProgressWidth = 10;
            this.itemDetailsSpinner.InnerWidth = 0;
            this.itemDetailsSpinner.InnerMargin = 2;
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
                this.dataGrid.Enabled = false;

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
                    Application.DoEvents();

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
                    SetItemImage(ReadImage(this._selectedItem.Image));
                }

                Task.Delay(100);
                this.dataGrid.Enabled = true;
            } 
        }

        private void SetNoItemImage()
        {
            this.pictureBox.Image = ReadImage(File.ReadAllBytes("no_image.png"));
            Application.DoEvents();

            Image = false;
        }

        private void SetItemImage(Image image)
        {
            if (this.pictureBox.Image != null)
            {
                this.pictureBox.Image.Dispose();
            }
            this.pictureBox.Image = image;
            Application.DoEvents();

            Image = true;
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
                // assign the new data
                this.dataGrid.DataSource = value;

                ResizeColumns();

                // item details spinner inner width not set correctly at startup
                // set correctly after vertical window resizing
                // workaround - TODO: find a better solution
                this.Height++;
                this.Height--;
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
                        Application.DoEvents();
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
        public event EventHandler WishlistButtonClicked;
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
            Application.DoEvents();
            Image image = new Bitmap(stream);
            Application.DoEvents();

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

        private void SetItemDetailsSpinnerPos()
        {
            this.itemDetailsSpinner.Location = new Point((this.pictureBox.Location.X + this.detailsGroup.Width / 4) + 15, this.pictureBox.Location.Y+10);
        }

        #region UI event handlers
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            SetItemDetailsSpinnerPos();
        }
        #endregion
    }//class
}
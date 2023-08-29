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
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using MyLibrary.Presenters;
using MyLibrary.Presenters.Excel;
using MyLibrary.Models.Entities;
using MyLibrary.Views;
using MyLibrary.Events;

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
            this.categoryDropDown.Items.Add("Cassette");
            this.categoryDropDown.Items.Add(ItemType.Cd);
            this.categoryDropDown.Items.Add(ItemType.Dvd);
            this.categoryDropDown.Items.Add(ItemType.BluRay);
            this.categoryDropDown.Items.Add("4k BluRay");
            this.categoryDropDown.Items.Add(ItemType.Vhs);
            this.categoryDropDown.Items.Add(ItemType.Vinyl);
            this.categoryDropDown.Items.Add("Flash Drive");
            this.categoryDropDown.Items.Add("Floppy Disk");
            this.categoryDropDown.Items.Add(ItemType.Other);

            this.addFilterTagField.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.addFilterTagField.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.addTagFilterButton.Enabled = false;
            this.removeFilterTagButton.Enabled = false;
            this.filterTagsList.GridLines = true;
            this.filterTagsList.Columns.Add("Tags");
            this.filterTagsList.Scrollable = true;
            this.filterTagsList.Columns[0].Width = this.filterTagsList.Width;

            this.detailsBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            this.detailsBox.Text = "Item";

            this.pictureBox.Cursor = Cursors.Hand;

            this.MinimumSize = new Size(800, 700);

            // register event handlers
            this.exitMenuItem.Click += ((sender, args) => Application.Exit());
            this.Resize += ((sender, args) =>
            {
                ResizeColumns();
            });
            this.aboutToolStripMenuItem.Click += ((sender, args) =>
            {
                new AboutBox().ShowDialog();
            });
            this.manualToolStripMenuItem.Click += ((sender, args) =>
            {
                System.Diagnostics.Process.Start("MyLibrary.pdf");
            });
            this.booksToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 0);
            this.mediaItemsAllCategoriesToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 1);
            this.cassettesToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 2);
            this.cdsToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 3);
            this.dvdsToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 4);
            this.bluRaysToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 5);
            this.uhdBluRayToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 6);
            this.vhssToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 7);
            this.vinylsToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 8);
            this.flashDriveToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 9);
            this.floppyDiskStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 10);
            this.otherToolStripMenuItem.Click += ((sender, args) => this.categoryDropDown.SelectedIndex = 11);
            this.XLSXtoolStripMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ExcelImportDialog())
                {
                    ExcelImportPresenter presenter = new ExcelImportPresenter(dialog);
                    dialog.ShowDialog();
                }
            });
            this.Shown += ((sender, args) =>
            {
                if (ConfigurationManager.AppSettings.Get("showTipsOnStartup").Equals("true"))
                {
                    using (var tipsDialog = new TipOfTheDayDialog())
                    {
                        TipOfTheDayPresenter tipsPresenter = new TipOfTheDayPresenter(tipsDialog, Configuration.TIPS);
                        tipsDialog.ShowDialog();
                    }
                }
            });
            // import CSV
            this.importTagsCsvToolStripMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ImportDialog("tag"))
                {
                    dialog.ShowDialog();
                }

                EventAggregator.GetInstance().Publish(new TagsUpdatedEvent());
            });
            this.importAuthorsCsvToolStripMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ImportDialog("author"))
                {
                    dialog.ShowDialog();
                }
            });
            this.importPublishersCsvToolStripMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ImportDialog("publisher"))
                {
                    dialog.ShowDialog();
                }
            });
            // export XLSX
            this.tagsToolStripMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ExportDialog())
                {
                    using (var presenter = new TagExcelPresenter(dialog, new Views.Excel.Excel()))
                    {
                        dialog.ShowDialog();
                    }
                }
            });
            this.booksToolStripMenuItem1.Click += ((sender, args) =>
            {
                using (var dialog = new ExportDialog())
                {
                    using (var presenter = new BookExcelPresenter(dialog, new Views.Excel.Excel()))
                    {
                        dialog.ShowDialog();
                    }
                }
            });
            this.authorsToolStripMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ExportDialog())
                {
                    using (var presenter = new AuthorExcelPresenter(dialog, new Views.Excel.Excel()))
                    {
                        dialog.ShowDialog();
                    }
                }
            });
            this.mediaItemsToolStripMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ExportDialog())
                {
                    using (var presenter = new MediaItemExcelPresenter(dialog, new Views.Excel.Excel()))
                    {
                        dialog.ShowDialog();
                    }
                }
            });
            this.publishersToolStripMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ExportDialog())
                {
                    using (var presenter = new PublisherExcelPresenter(dialog, new Views.Excel.Excel()))
                    {
                        dialog.ShowDialog();
                    }
                }
            });
            this.exportWishlistMenuItem.Click += ((sender, args) =>
            {
                using (var dialog = new ExportDialog())
                {
                    using (var presenter = new WishlistExcelPresenter(dialog, new Views.Excel.Excel()))
                    {
                        dialog.ShowDialog();
                    }
                }
            });
            this.filterTagsList.SelectedIndexChanged += ((sender, args) =>
            {
                if (this.filterTagsList.SelectedIndices.Count != 0)
                {
                    this.removeFilterTagButton.Enabled = true;
                }
                else
                {
                    this.removeFilterTagButton.Enabled = false;
                }
            });
            this.addFilterTagField.TextChanged += ((sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(this.addFilterTagField.Text))
                {
                    this.addTagFilterButton.Enabled = true;
                }
                else
                {
                    this.addTagFilterButton.Enabled = false;
                }
            });
            // fire the public event so the subscribed present can react
            this.searchBooksButton.Click += ((sender, args) =>
            {
                SearchByIsbnClicked?.Invoke(sender, args);
            });
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
                AddNewItemButtonClicked?.Invoke(sender, args);
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
                this.DeleteButtonClicked?.Invoke(this, args);
            });
            this.clearFilterButton.Click += ((sender, args) =>
            {
                this.TitleFilterText = " ";
                this.TitleFilterText = null;
                this.filterTagsList.Items.Clear();
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
                using (var tagsDialog = await ManageTagsForm.CreateAsync())
                {
                    tagsDialog.ShowDialog();
                }

                EventAggregator.GetInstance().Publish(new TagsUpdatedEvent());
            });
            this.manageItemTagsButton.Click += (async (sender, args) =>
            {
                using (var tagsDialog = await ManageTagsForItemDialog.CreateAsync(this.SelectedItem))
                {
                    tagsDialog.ShowDialog();
                }

                EventAggregator.GetInstance().Publish(new TagsUpdatedEvent());
            });
            this.manageItemCopiesButton.Click += ((sender, args) =>
            {
                ManageCopiesButtonClicked?.Invoke(sender, args);
            });
            this.wishlistButton.Click += ((sender, args) =>
            {
                this.WishlistButtonClicked?.Invoke(sender, args);
            });
            this.addTagFilterButton.Click += (async (sender, args) =>
            {
                if (this.filterTagsList.FindItemWithText(this.addFilterTagField.Text) == null)
                {
                    this.filterTagsList.Items.Add(this.addFilterTagField.Text);
                }

                await Task.Delay(FILTER_DELAY);

                FiltersUpdated?.Invoke(sender, args);
            });
            this.removeFilterTagButton.Click += (async (sender, args) =>
            {
                var selectedItems = this.filterTagsList.SelectedItems;
                if (this.filterTagsList.SelectedIndices.Count != 0)
                {
                    for (int index = selectedItems.Count - 1; index >= 0; index--)
                    {
                        this.filterTagsList.Items.Remove(selectedItems[index]);
                    }
                }

                await Task.Delay(FILTER_DELAY);

                FiltersUpdated?.Invoke(sender, args);
            });

            // select viewing books by default
            this.categoryDropDown.SelectedIndex = 0;

            SetItemDetailsSpinnerPos();
            this.itemDetailsSpinner.OuterMargin = 0;
            this.itemDetailsSpinner.OuterWidth = 6;
            this.itemDetailsSpinner.ProgressWidth = 10;
            this.itemDetailsSpinner.InnerWidth = 0;
            this.itemDetailsSpinner.InnerMargin = 2;

            // set tab order
            this.titleFilterField.TabIndex = 0;
            this.addFilterTagField.TabIndex = 1;
            this.filterTagsList.TabIndex = 2;
            this.addTagFilterButton.TabStop = false;
            this.removeFilterTagButton.TabStop = false;
            this.applyFilterButton.TabStop = false;
            this.clearFilterButton.TabStop = false;
            this.manageItemTagsButton.TabIndex = 0;
            this.manageItemCopiesButton.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.detailsBox.TabStop = false;
            this.selectImageButton.TabIndex = 2;
            this.removeImageButton.TabIndex = 3;
            this.textBoxNotes.TabIndex = 4;
            this.saveChangesButton.TabIndex = 5;
            this.discardChangesButton.TabIndex = 6;
        }//ctor

        public static readonly string LOAD_IMAGE_DIALOG_TITLE = "Load Image";
        public static readonly string LOAD_IMAGE_DIALOG_FILTER = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

        private void ResizeColumns()
        {
            // resize DataGridView columns nicely
            this.dataGrid.Columns[1].Width = this.dataGrid.Width / 4; // title
            this.dataGrid.Columns[0].Width = 40; // Id
            if (this.categoryDropDown.SelectedIndex == 0)
            {
                // books displayed
                this.dataGrid.Columns[2].Width = 120; // ISBN
            }
            else
            {
                // media items displayed
                this.dataGrid.Columns[2].Width = 90; // type
                this.dataGrid.Columns[3].Width = 120; // number
                this.dataGrid.Columns[4].Width = 90; // run time
                this.dataGrid.Columns[5].Width = 100; // release year
            }
        }

        public bool ShowDeleteConfirmationDialog(string title)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to delete this item? \r\n " + title, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadWindow()
        {
            WindowCreated?.Invoke(this, null);
        }

        public void LoadFilterTags(IEnumerable<string> tags)
        {
            this.addFilterTagField.Items.Clear();
            foreach (var tag in tags)
            {
                this.addFilterTagField.Items.Add(tag);
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
                for (int i = 0; i < this.filterTagsList.Items.Count; i++)
                {
                    yield return this.filterTagsList.Items[i].Text;
                }
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
        public event EventHandler ManageCopiesButtonClicked;
        public event EventHandler WindowCreated;
        public event EventHandler AddNewItemButtonClicked;
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
            this.itemDetailsSpinner.Location = new Point((this.pictureBox.Location.X + this.detailsGroup.Width / 4) + 15, this.pictureBox.Location.Y + 10);
        }

        #region UI event handlers
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            SetItemDetailsSpinnerPos();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            Image toDisplay = this.pictureBox.Image;
            using (ImageWindow imageWindow = new ImageWindow(toDisplay))
            {
                imageWindow.ShowDialog();
            }
        }
        #endregion
    }//class
}
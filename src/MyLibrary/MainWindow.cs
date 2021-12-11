using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using MyLibrary.Models.Entities;
using MyLibrary.Views;
using MyLibrary.BusinessLogic;

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

            this.tagsList.CheckOnClick = true;

            // register event handlers
            this.exitMenuItem.Click += ((sender, args) => Application.Exit());
            this.addButton.Click += (async (sender, args) =>
            {
                switch (this.categoryDropDown.SelectedIndex)
                {
                    case 0:
                        new AddNewBookForm().ShowDialog();
                        break;
                    default:
                        var addItemDialog = new AddNewMediaItemForm();
                        var addItemPresenter = new Presenters.AddMediaItemPresenter(new BusinessLogic.Repositories.MediaItemRepository(), new BusinessLogic.Repositories.TagRepository(),
                            addItemDialog);
                        await addItemPresenter.PopulateTagsList();
                        addItemDialog.ShowDialog();
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
                while (this.tagsList.CheckedIndices.Count > 0)
                {
                    this.tagsList.SetItemChecked(this.tagsList.CheckedIndices[0], false);
                }
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
            this.selectImageButton.Click += ((sender, args) =>
            {
                // load image from file
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Load Image";
                    dialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        byte[] bytes = File.ReadAllBytes(dialog.FileName);
                        this.pictureBox.Image = ReadImage(bytes);

                        // fire the public event so the subscribed presenter can react
                        SelectedItemModified?.Invoke(sender, args);
                    }
                }
            });
            this.removeImageButton.Click += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                SelectedItemModified?.Invoke(sender, args);

                this.pictureBox.Image = null;
            });
            this.tagsButton.Click += (async (sender, args) =>
            {
                var form = await ManageTagsForm.CreateAsync(this);
                form.TagsUpdated += ((s, a) =>
                {
                    // fire the public event so the subscribed presenter can react
                    this.TagsUpdated?.Invoke(s, a);
                });
                form.ShowDialog();
            });
            this.tagsList.ItemCheck += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                FiltersUpdated?.Invoke(sender, args);
            });

            LoadWindow();
        }//ctor

        public void LoadWindow()
        {
            // select viewing books by default
            this.CategoryDropDownSelectedIndex = 0;
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
                    temp.Image = WriteImage(this.pictureBox.Image, this.pictureBox.Image.RawFormat);
                }
                else
                {
                    temp.Image = null;
                }

                return temp;
            }

            set
            {
                this._selectedItem = value;

                this.textBoxNotes.Text = this._selectedItem.Notes;
                this.pictureBox.Image = ReadImage(this._selectedItem.Image);
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

        public IEnumerable<string> SelectedFilterTags
        {
            get
            {
                foreach (var tag in this.tagsList.CheckedItems)
                    yield return tag.ToString();
            }
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
        #endregion

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
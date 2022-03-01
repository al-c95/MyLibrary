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
    public partial class AddNewMediaItemForm : Form, IAddMediaItemForm
    {
        // ctor
        public AddNewMediaItemForm()
        {
            InitializeComponent();

            // populate item types drop-down
            this.mediaTypesOptions.Items.Add(ItemType.Cd);
            this.mediaTypesOptions.Items.Add(ItemType.Dvd);
            this.mediaTypesOptions.Items.Add(ItemType.BluRay);
            this.mediaTypesOptions.Items.Add(ItemType.Vhs);
            this.mediaTypesOptions.Items.Add(ItemType.Vinyl);
            this.mediaTypesOptions.Items.Add(ItemType.Other);
            this.mediaTypesOptions.SelectedIndex = 0;

            this.tagsList.CheckOnClick = true;

            this.CenterToParent();

            // set tab order of controls
            this.titleField.TabIndex = 0;
            this.numberField.TabIndex = 1;
            this.runningTimeField.TabIndex = 2;
            this.yearField.TabIndex = 3;
            this.notesField.TabIndex = 4;

            this.addNewTagButton.Enabled = true;

            // register event handlers
            // fire the public event so the subscribed presenter can react
            this.titleField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.numberField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.runningTimeField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.yearField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.imageFilePathField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.saveButton.Click += ((sender, args) =>
            {
                SaveButtonClicked?.Invoke(sender, args);
            });
            this.filterTagField.TextChanged += (async (sender, args) =>
            {
                await Task.Delay(MainWindow.FILTER_DELAY);
                this.FilterTagsFieldUpdated?.Invoke(sender, args);
            });
            this.addNewTagButton.Click += ((sender, args) =>
            {
                this.AddNewTagButtonClicked?.Invoke(sender, args);
            });
            this.tagsList.MouseUp += ((sender, args) =>
            {
                this.TagCheckedChanged?.Invoke(sender, args);
            });
            // handle the event here
            this.applyFilterButton.Click += ((sender, args) =>
            {
                this.FilterTagsFieldUpdated?.Invoke(sender, args);
            });
            this.clearFilterButton.Click += ((sender, args) =>
            {
                this.filterTagField.Text = string.Empty;
            });
            this.browseImageButton.Click += ((sender, args) =>
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = MainWindow.LOAD_IMAGE_DIALOG_TITLE;
                    dialog.Filter = MainWindow.LOAD_IMAGE_DIALOG_FILTER;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.imageFilePathField.Text = dialog.FileName;
                    }
                }
            });
        }

        public string ImageFilePathFieldText
        {
            get => this.imageFilePathField.Text;
            set => this.imageFilePathField.Text = value;
        }

        public string SelectedCategory
        {
            get => this.mediaTypesOptions.Text;
        }

        public int SelectedCategoryIndex
        {
            get => this.mediaTypesOptions.SelectedIndex;
            set => this.mediaTypesOptions.SelectedIndex = value;
        }

        public string TitleFieldText
        {
            get => this.titleField.Text;
            set => this.titleField.Text = value;
        }

        public string NumberFieldText
        {
            get => this.numberField.Text;
            set => this.numberField.Text = value; 
        }

        public string RunningTimeFieldEntry
        {
            get => this.runningTimeField.Text;
            set => this.runningTimeField.Text = value; 
        }

        public string YearFieldEntry
        {
            get => this.yearField.Text;
            set => this.yearField.Text = value; 
        }

        public string NotesFieldText
        {
            get => this.notesField.Text;
            set => this.notesField.Text = value; 
        }

        public string NewTagFieldText
        {
            get => this.filterTagField.Text;
            set => this.filterTagField.Text = value;
        }

        public bool AddNewTagButtonEnabled
        {
            get => this.addNewTagButton.Enabled;
            set => this.addNewTagButton.Enabled = value;
        }

        public IEnumerable<string> SelectedTags
        {
            get
            {
                for (int i = 0; i <= this.tagsList.Items.Count - 1; i++)
                {
                    if (this.tagsList.GetItemChecked(i))
                        yield return this.tagsList.Items[i].ToString();
                }
            }
        }

        public IEnumerable<string> UnselectedTags
        {
            get
            {
                for (int i = 0; i <= this.tagsList.Items.Count-1; i++)
                {
                    if (!this.tagsList.GetItemChecked(i))
                        yield return this.tagsList.Items[i].ToString();
                }
            }
        }

        public bool SaveButtonEnabled
        {
            get => this.saveButton.Enabled;
            set => this.saveButton.Enabled = value; 
        }

        public bool CancelButtonEnabled
        {
            get => this.cancelButton.Enabled;
            set => this.cancelButton.Enabled = value; 
        }

        public string FilterTagsFieldEntry
        {
            get => this.filterTagField.Text;
            set => this.filterTagField.Text = value;
        }

        public void PopulateTagsList(IEnumerable<string> tagNames)
        {
            this.tagsList.Items.Clear();

            foreach (var tagName in tagNames)
            {
                this.tagsList.Items.Add(tagName, false);
            }
        }

        public void CloseDialog()
        {
            this.Close();
        }

        public void ShowItemAlreadyExistsDialog(string title)
        {
            MessageBox.Show("Item with title: " + title + " already exists.", "Add item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ItemAddedFinished()
        {
            ItemAdded?.Invoke(this, null);
        }

        public event EventHandler InputFieldsUpdated;
        public event EventHandler SaveButtonClicked;
        public event EventHandler ItemAdded;
        public event EventHandler FilterTagsFieldUpdated;
        public event EventHandler AddNewTagButtonClicked;
        public event EventHandler TagCheckedChanged;

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowErrorDialog(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AddTags(Dictionary<string, bool> tags)
        {
            this.tagsList.Items.Clear();

            foreach (var kvp in tags)
            {
                this.tagsList.Items.Add(kvp.Key, kvp.Value);
            }
        }

        public string ShowNewTagDialog()
        {
            NewTagInputBox dialog = new NewTagInputBox();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.TagName;
            }
            else
            {
                return null;
            }
        }

        public void ShowTagAlreadyExistsDialog(string tag)
        {
            MessageBox.Show("Tag: " + tag + " already exists.", "Add Tag", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }//class
}

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
    public partial class AddNewBookForm : Form, IAddBookForm
    {
        public AddNewBookForm()
        {
            InitializeComponent();

            // disable some buttons initially
            this.saveButton.Enabled = false;
            this.addNewAuthorButton.Enabled = false;
            this.addNewPublisherButton.Enabled = false;
            this.addNewTagButton.Enabled = false;

            this.tagsList.CheckOnClick = true;
            this.authorsList.CheckOnClick = true;

            this.CenterToParent();

            this.addNewTagButton.Enabled = true;
            this.addNewPublisherButton.Enabled = true;
            this.addNewAuthorButton.Enabled = true;

            // set tab order of controls
            this.titleField.TabIndex = 0;
            this.longTitleField.TabIndex = 1;
            this.IsbnField.TabIndex = 2;
            this.Isbn13Field.TabIndex = 3;
            this.overviewField.TabIndex = 4;
            this.MsrpField.TabIndex = 5;
            this.pagesField.TabIndex = 6;
            this.synopsisField.TabIndex = 7;
            this.excerptField.TabIndex = 8;
            this.languageField.TabIndex = 9;
            this.datePublishedField.TabIndex = 10;
            this.placeOfPublicationField.TabIndex = 11;
            this.editionField.TabIndex = 12;
            this.deweyDecimalField.TabIndex = 13;
            this.formatField.TabIndex = 14;
            this.dimensionsField.TabIndex = 15;
            this.notesField.TabIndex = 16;
            this.browseImageButton.TabIndex = 17;
            this.imageFilePathField.TabStop = false;
            this.saveButton.TabIndex = 18;
            this.cancelButton.TabIndex = 19;
            this.authorsGroup.TabStop = false;
            this.authorsList.TabStop = false;
            this.filterAuthorsField.TabStop = false;
            this.applyAuthorFilterButton.TabStop = false;
            this.clearAuthorFilterButton.TabStop = false;
            this.addNewAuthorButton.TabStop = false;
            this.tagsGroup.TabStop = false;
            this.tagsList.TabStop = false;
            this.filterTagField.TabStop = false;
            this.applyTagFilterButton.TabStop = false;
            this.clearTagFilterButton.TabStop = false;
            this.addNewTagButton.TabStop = false;
            this.publishersGroup.TabStop = false;
            this.publishersList.TabStop = false;
            this.filterPublishersField.TabStop = false;
            this.applyPublisherFilterButton.TabStop = false;
            this.clearPublisherFilterButton.TabStop = false;
            this.addNewPublisherButton.TabStop = false;

            // register event handlers
            // fire the public event so the subscribed presenter can react
            this.titleField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.longTitleField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.IsbnField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.Isbn13Field.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.overviewField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.MsrpField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.pagesField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.synopsisField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.excerptField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.datePublishedField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.editionField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.deweyDecimalField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.formatField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.dimensionsField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.languageField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.notesField.TextChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.saveButton.Click += ((sender, args) =>
            {
                SaveButtonClicked?.Invoke(sender, args);
            });
            this.addNewTagButton.Click += ((sender, args) =>
            {
                this.AddNewTagButtonClicked?.Invoke(sender, args);
            });
            this.tagsList.MouseUp += ((sender, args) =>
            {
                this.TagCheckedChanged?.Invoke(sender, args);
            });
            this.authorsList.MouseUp += ((sender, args) =>
            {
                this.AuthorCheckedChanged?.Invoke(sender, args);
            });
            this.publishersList.SelectedIndexChanged += ((sender, args) =>
            {
                this.InputFieldsUpdated?.Invoke(sender, args);
            });
            this.filterPublishersField.TextChanged += (async (sender, args) =>
            {
                await Task.Delay(MainWindow.FILTER_DELAY);
                this.FilterPublishersFieldUpdated?.Invoke(sender, args);
            });
            this.applyPublisherFilterButton.Click += ((sender, args) =>
            {
                this.FilterPublishersFieldUpdated?.Invoke(sender, args);
            });
            this.filterAuthorsField.TextChanged += (async (sender, args) =>
            {
                await Task.Delay(MainWindow.FILTER_DELAY);
                this.FilterAuthorsFieldUpdated?.Invoke(sender, args);
            });
            this.applyAuthorFilterButton.Click += ((sender, args) =>
            {
                this.FilterAuthorsFieldUpdated?.Invoke(sender, args);
            });
            this.filterTagField.TextChanged += (async (sender, args) =>
            {
                await Task.Delay(MainWindow.FILTER_DELAY);
                this.FilterTagsFieldUpdated?.Invoke(sender, args);
            });
            this.applyTagFilterButton.Click += ((sender, args) =>
            {
                this.FilterTagsFieldUpdated?.Invoke(sender, args);
            });
            this.addNewPublisherButton.Click += ((sender, args) =>
            {
                this.AddNewPublisherButtonClicked?.Invoke(sender, args);
            });
            this.addNewAuthorButton.Click += ((sender, args) =>
            {
                this.AddNewAuthorButtonClicked?.Invoke(sender, args);
            });
            // handle the event here
            this.clearPublisherFilterButton.Click += ((sender, args) =>
            {
                this.filterPublishersField.Text = string.Empty;
            });
            this.clearAuthorFilterButton.Click += ((sender, args) =>
            {
                this.filterAuthorsField.Text = string.Empty;
            });
            this.clearTagFilterButton.Click += ((sender, args) =>
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

        public string TitleFieldText
        {
            get => this.titleField.Text;
            set => this.titleField.Text = value;
        }

        public string NotesFieldText
        {
            get => this.notesField.Text;
            set => this.notesField.Text = value;
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
                for (int i = 0; i <= this.tagsList.Items.Count - 1; i++)
                {
                    if (!this.tagsList.GetItemChecked(i))
                        yield return this.tagsList.Items[i].ToString();
                }
            }
        }

        public IEnumerable<string> SelectedAuthors
        {
            get
            {
                for (int i = 0; i <= this.authorsList.Items.Count - 1; i++)
                {
                    if (this.authorsList.GetItemChecked(i))
                        yield return this.authorsList.Items[i].ToString();
                }
            }
        }

        public IEnumerable<string> UnselectedAuthors
        {
            get
            {
                for (int i = 0; i <= this.authorsList.Items.Count-1; i++)
                {
                    if (!this.authorsList.GetItemChecked(i))
                        yield return this.authorsList.Items[i].ToString();
                }
            }
        }

        /// <summary>
        /// Returns name of the selected publisher. If none selected, returns null.
        /// </summary>
        public string SelectedPublisher
        {
            get 
            { 
                if (this.publishersList.SelectedItems.Count==0)
                {
                    return null;
                }
                else
                {
                    return this.publishersList.SelectedItem.ToString();
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
        public string LongTitleFieldText
        {
            get => this.longTitleField.Text;
            set => this.longTitleField.Text = value;
        }

        public string IsbnFieldText
        {
            get => this.IsbnField.Text;
            set => this.IsbnField.Text = value; 
        }

        public string Isbn13FieldText
        {
            get => this.Isbn13Field.Text;
            set => this.Isbn13Field.Text = value;
        }

        public string DeweyDecimalFieldText
        {
            get => this.deweyDecimalField.Text;
            set => this.deweyDecimalField.Text = value; 
        }

        public string FormatFieldText
        {
            get => this.formatField.Text;
            set => this.formatField.Text = value; 
        }

        public string LanguageFieldText
        {
            get => this.languageField.Text;
            set => this.languageField.Text = value; 
        }

        public string DatePublishedFieldText
        {
            get => this.datePublishedField.Text;
            set => this.datePublishedField.Text = value; 
        }

        public string EditionFieldText
        {
            get => this.editionField.Text;
            set => this.editionField.Text = value; 
        }

        public string PagesFieldText 
        {
            get => this.pagesField.Text;
            set => this.pagesField.Text = value; 
        }

        public string DimensionsFieldText
        {
            get => this.dimensionsField.Text;
            set => this.dimensionsField.Text = value;
        }

        public string OverviewFieldText
        {
            get => this.overviewField.Text;
            set => this.overviewField.Text = value; 
        }

        public string MsrpFieldText 
        {
            get => this.MsrpField.Text;
            set => this.MsrpField.Text = value;
        }

        public string ExcerptFieldText 
        {
            get => this.excerptField.Text;
            set => this.excerptField.Text = value; 
        }

        public string SynopsysFieldText
        {
            get => this.synopsisField.Text;
            set => this.synopsisField.Text = value; 
        }

        public string PlaceOfPublicationFieldText
        {
            get => this.placeOfPublicationField.Text;
            set => this.placeOfPublicationField.Text = value;
        }

        public bool AddNewTagButtonEnabled
        {
            get => this.addNewTagButton.Enabled;
            set => this.addNewTagButton.Enabled = value;
        }

        public string FilterTagsFieldEntry 
        {
            get => this.filterTagField.Text;
            set => this.filterTagField.Text = value; 
        }

        public string FilterAuthorsFieldEntry 
        {
            get => this.filterAuthorsField.Text;
            set => this.filterAuthorsField.Text = value;
        }

        public string FilterPublishersFieldEntry 
        {
            get => this.filterPublishersField.Text;
            set => this.filterPublishersField.Text = value; 
        }

        public event EventHandler InputFieldsUpdated;
        public event EventHandler SaveButtonClicked;
        public event EventHandler ItemAdded;
        public event EventHandler FilterTagsFieldUpdated;
        public event EventHandler AddNewTagButtonClicked;
        public event EventHandler TagCheckedChanged;
        public event EventHandler FilterPublishersFieldUpdated;
        public event EventHandler AddNewPublisherButtonClicked;
        public event EventHandler FilterAuthorsFieldUpdated;
        public event EventHandler AuthorCheckedChanged;
        public event EventHandler AddNewAuthorButtonClicked;

        public void CloseDialog()
        {
            this.Close();
        }

        public void ItemAddedFinished()
        {
            ItemAdded?.Invoke(this, null);
        }

        private void UncheckAll(CheckedListBox list)
        {
            while (list.CheckedIndices.Count > 0)
            {
                list.SetItemChecked(list.CheckedIndices[0], false);
            }
        }

        public void UncheckAllTags()
        {
            UncheckAll(this.tagsList);
        }

        public void UncheckAllAuthors()
        {
            UncheckAll(this.authorsList);
        }

        public void PopulateTagsList(IEnumerable<string> tagNames)
        {
            this.tagsList.Items.Clear();

            foreach (var tagName in tagNames)
            {
                this.tagsList.Items.Add(tagName, false);
            }
        }

        public void PopulateAuthorList(IEnumerable<string> names)
        {
            this.authorsList.Items.Clear();

            foreach (var name in names)
            {
                this.authorsList.Items.Add(name, false);
            }
        }

        public void PopulatePublisherList(IEnumerable<string> publisherNames)
        {
            this.publishersList.BeginUpdate();
            this.publishersList.Items.Clear();

            foreach (var publisherName in publisherNames)
            {
                this.publishersList.Items.Add(publisherName);
            }
            this.publishersList.EndUpdate();
        }

        public void ShowItemAlreadyExistsDialog(string title)
        {
            MessageBox.Show("Item with title: " + title + " already exists.", "Add item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ShowIsbnAlreadyExistsDialog(string isbn)
        {
            MessageBox.Show("Item with ISBN10 or ISBN13: " + isbn + " already exists.", "Add item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowErrorDialog(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowAsDialog()
        {
            this.ShowDialog();
        }

        public void SetPublisher(Publisher publisher, bool selected)
        {
            if (!(this.publishersList.Items.Cast<Object>().Any(p => p.ToString() == publisher.Name)))
            {
                // publisher does not exist
                // add it to the list
                this.publishersList.Items.Add(publisher.Name);
            }
            // select this publisher
            this.publishersList.SetSelected(this.publishersList.FindString(publisher.Name), true);
        }

        public void AddTags(Dictionary<string, bool> tags)
        {
            this.tagsList.Items.Clear();

            foreach (var kvp in tags)
            {
                this.tagsList.Items.Add(kvp.Key, kvp.Value);
            }
        }

        public void AddAuthors(Dictionary<string, bool> authors)
        {
            this.authorsList.Items.Clear();

            foreach (var kvp in authors)
            {
                this.authorsList.Items.Add(kvp.Key, kvp.Value);
            }
        }

        public void AddPublishers(List<string> publishers)
        {
            this.publishersList.BeginUpdate();
            this.publishersList.ClearSelected();
            this.publishersList.Items.Clear();

            foreach (var publisher in publishers)
            {
                this.publishersList.Items.Add(publisher);
            }
            this.publishersList.EndUpdate();
        }

        public string ShowNewAuthorDialog()
        {
            NewAuthorInputBox dialog = new NewAuthorInputBox();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.AuthorName;
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

        public void ShowPublisherAlreadyExistsDialog(string newPublisher)
        {
            MessageBox.Show("Publisher: " + newPublisher + " already exists.", "Add Publisher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void ShowAuthorAlreadyExistsDialog(string author)
        {
            MessageBox.Show("Author: " + author + " already exists.", "Add Tag", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }//class
}

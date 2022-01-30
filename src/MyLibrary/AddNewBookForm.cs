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
using System.Text.RegularExpressions;
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
            this.datePublishedField.TabIndex = 9;
            this.editionField.TabIndex = 10;
            this.deweyDecimalField.TabIndex = 11;
            this.formatField.TabIndex = 12;
            this.dimensionsField.TabIndex = 13;
            this.languageField.TabIndex = 14;
            this.notesField.TabIndex = 15;

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
            this.authorsList.ItemCheck += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.publishersList.SelectedIndexChanged += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.tagsList.ItemCheck += ((sender, args) =>
            {
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.saveButton.Click += ((sender, args) =>
            {
                SaveButtonClicked?.Invoke(sender, args);
            });
            this.newAuthorFirstNameField.TextChanged += ((sender, args) =>
            {
                this.NewAuthorFieldsUpdated?.Invoke(sender, args);
            });
            this.newAuthorLastNameField.TextChanged += ((sender, args) =>
            {
                this.NewAuthorFieldsUpdated?.Invoke(sender, args);
            });
            this.newPublisherField.TextChanged += ((sender, args) =>
            {
                this.NewPublisherFieldUpdated?.Invoke(sender, args);
            });
            this.newTagField.TextChanged += ((sender, args) =>
            {
                this.NewTagFieldUpdated?.Invoke(sender, args);
            });
            // handle the event here
            this.addNewTagButton.Click += ((sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(this.newTagField.Text))
                {
                    if (this.tagsList.Items.Cast<Object>().Any(t => t.ToString() == this.newTagField.Text))
                        return;

                    this.tagsList.Items.Add(this.newTagField.Text, true);

                    this.newTagField.Clear();
                }
            });
            this.addNewPublisherButton.Click += ((sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(this.newPublisherField.Text))
                {
                    if (this.publishersList.Items.Cast<Object>().Any(p => p.ToString() == this.newPublisherField.Text))
                        return;

                    this.publishersList.Items.Add(this.newPublisherField.Text);

                    this.publishersList.SelectedItem = this.publishersList.Items.IndexOf(this.newPublisherField.Text);

                    int newPublisherIndex = this.publishersList.Items.IndexOf(this.newPublisherField.Text);
                    this.publishersList.SelectedIndex = newPublisherIndex;

                    this.newPublisherField.Clear();
                }
            });
            this.addNewAuthorButton.Click += ((sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(this.newAuthorFirstNameField.Text) &&
                    !string.IsNullOrWhiteSpace(this.newAuthorLastNameField.Text))
                {
                    if (this.authorsList.Items.Cast<Object>().Any(a => a.ToString() == this.newAuthorLastNameField.Text + ", " + this.newAuthorFirstNameField.Text))
                        return;

                    this.authorsList.Items.Add(this.newAuthorLastNameField.Text + ", " + this.newAuthorFirstNameField.Text, true);

                    this.newAuthorFirstNameField.Clear();
                    this.newAuthorLastNameField.Clear();
                }
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
                foreach (var tag in this.tagsList.CheckedItems)
                    yield return tag.ToString();
            }
        }

        public IEnumerable<string> SelectedAuthors
        {
            get
            {
                foreach (var author in this.authorsList.CheckedItems)
                    yield return author.ToString();
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

        public string NewAuthorFirstNameFieldText
        {
            get => this.newAuthorFirstNameField.Text;
            set => this.newAuthorFirstNameField.Text=value;
        }

        public string NewAuthorLastNameFieldText
        {
            get => this.newAuthorLastNameField.Text;
            set => this.newAuthorLastNameField.Text = value;
        }

        public bool AddNewAuthorButtonEnabled
        {
            get => this.addNewAuthorButton.Enabled;
            set => this.addNewAuthorButton.Enabled = value;
        }

        public string NewPublisherFieldText 
        {
            get => this.newPublisherField.Text;
            set => this.newPublisherField.Text = value;
        }

        public bool AddNewPublisherButtonEnabled
        {
            get => this.addNewPublisherButton.Enabled;
            set => this.addNewPublisherButton.Enabled = value;
        }

        public string NewTagFieldText
        {
            get => this.newTagField.Text;
            set => this.newTagField.Text = value;
        }

        public bool AddNewTagButtonEnabled
        {
            get => this.addNewTagButton.Enabled;
            set => this.addNewTagButton.Enabled = value;
        }

        public event EventHandler InputFieldsUpdated;
        public event EventHandler SaveButtonClicked;
        public event EventHandler ItemAdded;
        public event EventHandler NewAuthorFieldsUpdated;
        public event EventHandler NewPublisherFieldUpdated;
        public event EventHandler NewTagFieldUpdated;

        public void CloseDialog()
        {
            this.Close();
        }

        public void ItemAddedFinished()
        {
            ItemAdded?.Invoke(this, null);
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
            this.publishersList.Items.Clear();

            foreach (var publisherName in publisherNames)
            {
                this.publishersList.Items.Add(publisherName);
            }
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

        public void SetAuthor(Author author, bool selected)
        {
            if (!(this.authorsList.Items.Cast<Object>().Any(a => a.ToString() == author.LastName + ", " + author.FirstName)))
            {
                // author does not exist
                // add it to the list
                this.authorsList.Items.Add(author.LastName + ", " + author.FirstName, selected);
            }
            // select this author
            this.authorsList.SetItemChecked(this.authorsList.Items.IndexOf(author.LastName + ", " + author.FirstName), true);
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
    }//class
}

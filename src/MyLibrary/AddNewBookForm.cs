using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.Views;

namespace MyLibrary
{
    public partial class AddNewBookForm : Form, IAddBookForm
    {
        public AddNewBookForm()
        {
            InitializeComponent();

            this.saveButton.Enabled = false;

            this.tagsList.CheckOnClick = true;
            this.authorsList.CheckOnClick = true;

            // register event handlers
            this.titleField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.longTitleField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.IsbnField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.Isbn13Field.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.overviewField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.MsrpField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.pagesField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.synopsisField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.excerptField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.datePublishedField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.editionField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.deweyDecimalField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.formatField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.dimensionsField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.languageField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.notesField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.authorsList.ItemCheck += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.publishersList.SelectedIndexChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.tagsList.ItemCheck += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.saveButton.Click += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                SaveButtonClicked?.Invoke(sender, args);
            });
            this.addNewTagButton.Click += ((sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(this.newTagField.Text))
                {
                    if (this.tagsList.Items.Cast<Object>().Any(t => t.ToString() == this.newTagField.Text))
                        return;

                    this.tagsList.Items.Add(this.newTagField.Text, true);
                }
            });
            this.addNewPublisherButton.Click += ((sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(this.newPublisherField.Text))
                {
                    if (this.publishersList.Items.Cast<Object>().Any(p => p.ToString() == this.newPublisherField.Text))
                        return;

                    this.publishersList.Items.Add(this.newPublisherField.Text);
                }
            });
            this.addNewAuthorButton.Click += ((sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(this.newAuthorFirstNameField.Text) &&
                    !string.IsNullOrWhiteSpace(this.newAuthorLastNameField.Text))
                {
                    if (this.authorsList.Items.Cast<Object>().Any(a => a.ToString() == this.newAuthorLastNameField.Text + ", " + this.newAuthorFirstNameField.Text))
                        return;

                    this.authorsList.Items.Add(this.newAuthorLastNameField.Text + ", " + this.newAuthorFirstNameField.Text);
                }
            });
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

        public event EventHandler InputFieldsUpdated;
        public event EventHandler SaveButtonClicked;
        public event EventHandler ItemAdded;

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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }//class
}

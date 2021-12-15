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
    public partial class AddNewMediaItemForm : Form, IAddMediaItemForm
    {
        // ctor
        public AddNewMediaItemForm()
        {
            InitializeComponent();

            this.mediaTypesOptions.Items.Add(ItemType.Cd);
            this.mediaTypesOptions.Items.Add(ItemType.Dvd);
            this.mediaTypesOptions.Items.Add(ItemType.BluRay);
            this.mediaTypesOptions.Items.Add(ItemType.Vhs);
            this.mediaTypesOptions.Items.Add(ItemType.Vinyl);
            this.mediaTypesOptions.Items.Add(ItemType.Other);
            this.mediaTypesOptions.SelectedIndex = 0;

            this.tagsList.CheckOnClick = true;

            // register event handlers
            this.titleField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.numberField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.runningTimeField.TextChanged += ((sender, args) =>
            {
                // fire the public event so the subscribed presenter can react
                InputFieldsUpdated?.Invoke(sender, args);
            });
            this.yearField.TextChanged += ((sender, args) =>
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
                    this.tagsList.Items.Add(this.newTagField.Text, true);
                }
            });
        }

        public string SelectedCategory
        {
            get => this.mediaTypesOptions.Text;
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

        public IEnumerable<string> SelectedTags
        {
            get
            {
                foreach (var tag in this.tagsList.CheckedItems)
                    yield return tag.ToString();              
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowErrorDialog(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }//class
}

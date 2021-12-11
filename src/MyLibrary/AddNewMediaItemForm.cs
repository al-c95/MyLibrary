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
    public partial class AddNewMediaItemForm : Form, IAddMediaItemForm
    {
        // ctor
        public AddNewMediaItemForm()
        {
            InitializeComponent();

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
        }

        public int CategoryDropDownSelectedIndex
        {
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
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

        public IEnumerable<string> SelectedFilterTags => throw new NotImplementedException();

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

        public event EventHandler InputFieldsUpdated;
        public event EventHandler SaveButtonClicked;

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }//class
}

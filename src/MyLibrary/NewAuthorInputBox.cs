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
    public partial class NewAuthorInputBox : Form, INewAuthor
    {
        public string AuthorName;

        public NewAuthorInputBox()
        {
            InitializeComponent();

            this.okButton.Enabled = false;
            this.AuthorName = null;

            this.MaximumSize = new Size(452, 205);
            this.CenterToParent();

            // register event handlers
            this.okButton.Click += ((sender, args) =>
            {
                this.DialogResult = DialogResult.OK;
            });
            this.cancelButton.Click += ((sender, args) =>
            {
                this.DialogResult = DialogResult.Cancel;
            });
            this.firstNameField.TextChanged += ((sender, args) =>
            {
                this.InputChanged?.Invoke(sender, args);
            });
            this.lastNameField.TextChanged += ((sender, args) =>
            {
                this.InputChanged?.Invoke(sender, args);
            });
        }//ctor

        public bool OkButtonEnabled
        {
            get => this.okButton.Enabled;
            set => this.okButton.Enabled = value;
        }

        public string DialogTitle 
        {
            get => this.Text;
            set => this.Text = value;
        }

        public string FirstNameEntry
        {
            get => this.firstNameField.Text;
            set => this.firstNameField.Text = value;
        }

        public string LastNameEntry
        {
            get => this.lastNameField.Text;
            set => this.lastNameField.Text = value;
        }

        public event EventHandler InputChanged;

        public AuthorName ShowAsDialog()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                return new AuthorName
                {
                    FirstName = this.firstNameField.Text,
                    LastName = this.lastNameField.Text
                };
            }
            else
            {
                return null;
            }
        }

        public interface INewAuthorInputBoxProvider
        {
            INewAuthor Get();
        }

        public class NewAuthorInputBoxProvider : INewAuthorInputBoxProvider
        {
            public INewAuthor Get()
            {
                return new NewAuthorInputBox();
            }
        }//class
    }//class
}
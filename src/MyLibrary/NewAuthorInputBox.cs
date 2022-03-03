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

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class NewAuthorInputBox : Form
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
                ValidateInput();
            });
            this.lastNameField.TextChanged += ((sender, args) =>
            {
                ValidateInput();
            });
        }//ctor

        private void ValidateInput()
        {
            bool sane = true;
            string firstName = this.firstNameField.Text;
            string lastName = this.lastNameField.Text;
            sane = sane && (Regex.IsMatch(firstName, Author.NAME_PATTERN) || Regex.IsMatch(firstName, Author.WITH_MIDDLE_NAME_PATTERN));
            sane = sane && Regex.IsMatch(lastName, Author.NAME_PATTERN);

            this.okButton.Enabled = sane;

            if (sane)
            {
                Author author = new Author();
                author.FirstName = firstName;
                author.LastName = lastName;

                this.AuthorName = author.GetFullNameLastNameCommaFirstName();
            }
            else
            {
                this.AuthorName = null;
            }
        }
    }//class
}

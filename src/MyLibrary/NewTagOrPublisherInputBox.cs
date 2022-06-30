﻿//MIT License

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

namespace MyLibrary
{
    // TODO: unit tests
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class NewTagOrPublisherInputBox : Form
    {
        private string _entry;
        public string Entry { get => this._entry; }

        public NewTagOrPublisherInputBox(InputBoxMode mode)
        {
            InitializeComponent();

            switch (mode)
            {
                case InputBoxMode.Tag:
                    this.label.Text = "New Tag:";
                    this.Text = "New Tag";
                    break;
                case InputBoxMode.Publisher:
                    this.label.Text = "New Publisher:";
                    this.Text = "New Publisher";
                    break;
            }

            this.okButton.Enabled = false;

            // register event handlers
            this.okButton.Click += ((sender, args) =>
            {
                this._entry = this.textBox1.Text;
                this.DialogResult = DialogResult.OK;
            });
            this.cancelButton.Click += ((sender, args) =>
            {
                this.DialogResult = DialogResult.Cancel;
            });
            this.textBox1.TextChanged += ((sender, args) =>
            {
                // empty entries are not valid
                if (string.IsNullOrWhiteSpace(this.textBox1.Text))
                {
                    this.okButton.Enabled = false;
                }
                else
                {
                    switch (mode)
                    {
                        case InputBoxMode.Tag:
                            this.okButton.Enabled = (Models.Entities.Tag.Validate(this.textBox1.Text));
                            break;
                        case InputBoxMode.Publisher:
                            this.okButton.Enabled = (Models.Entities.Publisher.ValidateName(this.textBox1.Text));
                            break;
                    }
                }
            });
        }//ctor

        public enum InputBoxMode
        {
            Tag,
            Publisher
        }
    }//class
}

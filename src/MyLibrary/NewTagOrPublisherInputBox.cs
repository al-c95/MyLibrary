//MIT License

//Copyright (c) 2021-2022

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
using System.Windows.Forms;
using MyLibrary.Views;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class NewTagOrPublisherInputBox : Form, INewTagOrPublisher
    {
        private string _entry;

        public event EventHandler InputChanged;

        public string Entry
        {
            get => this.textBox1.Text;
            set => this.textBox1.Text = value;
        }

        public bool OkButtonEnabled
        {
            get => this.okButton.Enabled;
            set => this.okButton.Enabled = value; 
        }

        public string Label 
        {
            get => this.label.Text;
            set => this.label.Text = value;
        }

        public string DialogTitle 
        {
            get => this.Text;
            set => this.Text = value; 
        }

        public NewTagOrPublisherInputBox()
        {
            InitializeComponent();

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
                this.InputChanged?.Invoke(sender, args);
            });
        }//ctor

        public string ShowAsDialog()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                return this.Entry;
            }
            else
            {
                return null;
            }
        }
    }//class

    public interface INewTagOrPublisherInputBoxProvider
    {
        INewTagOrPublisher Get();
    }

    public class NewTagOrPublisherInputBoxProvider : INewTagOrPublisherInputBoxProvider
    {
        public INewTagOrPublisher Get()
        {
            return new NewTagOrPublisherInputBox();
        }
    }//class
}
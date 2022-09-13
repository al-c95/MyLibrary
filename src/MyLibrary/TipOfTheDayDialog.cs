//MIT License

//Copyright (c) 2022

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
using System.Configuration;
using System.Windows.Forms;
using MyLibrary.Views;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class TipOfTheDayDialog : Form, ITipOfTheDay
    {
        public TipOfTheDayDialog()
        {
            InitializeComponent();

            this.TopMost = true;
            this.BringToFront();
            this.CenterToParent();
            this.Activate();

            this.showTipsCheckBox.Checked = true;
        }

        public string TipsText 
        {
            get => this.tipsTextBox.Text;
            set => this.tipsTextBox.Text = value;
        }

        public string ShowAllButtonText
        {
            get => this.showAllButton.Text;
            set => this.showAllButton.Text = value;
        }

        public bool ShowTipsAtStartupChecked
        {
            get => this.showTipsCheckBox.Checked;
            set => this.showTipsCheckBox.Checked = value; 
        }

        public bool ShowNextButtonEnabled
        {
            get => this.nextButton.Enabled;
            set => this.nextButton.Enabled = value;
        }

        public event EventHandler NextClicked;
        public event EventHandler ShowAllClicked;

        #region UI event handlers
        private void okButton_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (this.showTipsCheckBox.Checked)
            {
                config.AppSettings.Settings["showTipsOnStartup"].Value = "true";
            }
            else
            {
                config.AppSettings.Settings["showTipsOnStartup"].Value = "false";
            }
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");

            this.Close();
        }
        
        private void nextButton_Click(object sender, EventArgs e)
        {
            this.NextClicked?.Invoke(sender, e);
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            this.ShowAllClicked?.Invoke(sender, e);

            if (this.showAllButton.Text.Equals("Show All"))
            {
                this.showAllButton.Text = "Show One";
            }
            else if (this.showAllButton.Text.Equals("Show One"))
            {
                this.showAllButton.Text = "Show All";
            }
        }
        #endregion
    }
}
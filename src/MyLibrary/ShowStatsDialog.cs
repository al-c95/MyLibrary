﻿using System;
using System.Windows.Forms;
using MyLibrary.Views;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ShowStatsDialog : Form, IShowStats
    {
        public ShowStatsDialog()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
        }

        public string StatsBoxTest { get => this.statsBox.Text; set => this.statsBox.Text=value; }

        public string StatusLabelText { get => this.statusLabel.Text; set => this.statusLabel.Text=value; }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }//class
}

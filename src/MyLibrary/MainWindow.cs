using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyLibrary
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            // register event handlers
            this.exitToolStripMenuItem.Click += (sender, args) =>
            {
                Application.Exit();
            };
        }
    }
}
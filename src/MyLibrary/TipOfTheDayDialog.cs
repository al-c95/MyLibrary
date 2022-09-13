using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyLibrary
{
    public partial class TipOfTheDayDialog : Form
    {
        public TipOfTheDayDialog()
        {
            InitializeComponent();

            this.TopMost = true;
            this.BringToFront();
            this.CenterToParent();
            this.Activate();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

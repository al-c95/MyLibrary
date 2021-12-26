using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyLibrary.ApiService
{
    public partial class SearchByIsbnDialog : Form
    {
        public SearchByIsbnDialog()
        {
            InitializeComponent();

            // register event handlers
            this.cancelButton.Click += ((sender, args) =>
            {
                this.Close();
            });

            this.StartPosition = FormStartPosition.CenterParent;
        }
    }//class
}

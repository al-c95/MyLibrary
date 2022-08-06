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
    public partial class ImportExcelDialog : Form
    {
        public ImportExcelDialog()
        {
            InitializeComponent();

            this.itemsList.View = View.Details;
        }
    }
}

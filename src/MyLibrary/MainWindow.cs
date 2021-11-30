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
using MyLibrary.DataAccessLayer;

namespace MyLibrary
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Text = Configuration.APP_NAME + " " + Configuration.APP_VERSION;

            // register event handlers
            this.exitMenuItem.Click += ((sender, args) => Application.Exit());
            this.addButton.Click += ((sender, args) => new AddNewMediaItemForm().ShowDialog());
        }//ctor

        private async void button1_Click(object sender, EventArgs e)
        {
            // get data
            BookDataAccessor bookDataAccessor = new BookDataAccessor();
            var allBooks = await bookDataAccessor.ReadAll();

            // populate the data grid
            this.dataGrid.Rows.Clear();
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add("idCol", "Id");
            this.dataGrid.Columns.Add("titleCol", "Title");
            foreach (var book in allBooks)
            {
                object[] newRow = { book.Id, book.Title };
                this.dataGrid.Rows.Add(newRow);
            }
        }
    }//class
}

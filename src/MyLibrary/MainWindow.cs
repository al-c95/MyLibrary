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
using MyLibrary.Models.Entities.Builders;
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
            this.dataGrid.Columns.Add("titleLongCol", "Long Title");
            foreach (var book in allBooks)
            {
                object[] newRow = { book.Id, book.Title, book.TitleLong };
                this.dataGrid.Rows.Add(newRow);
            }
        }

        private async void getAllMediaItemsButton_Click(object sender, EventArgs e)
        {
            // get data
            MediaItemDataAccessor mediaItemDataAccessor = new MediaItemDataAccessor();
            var allItems = await mediaItemDataAccessor.ReadAll();

            // populate the data grid
            this.dataGrid.Rows.Clear();
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add("idCol", "Id");
            this.dataGrid.Columns.Add("titleCol", "Title");
            this.dataGrid.Columns.Add("typeCol", "Type");
            this.dataGrid.Columns.Add("numCol", "Number");
            this.dataGrid.Columns.Add("runTimeCol", "Running Time");
            this.dataGrid.Columns.Add("releaseYearCol", "Release Year");
            this.dataGrid.Columns.Add("notesCol", "Notes");
            //this.dataGrid.Columns.Add("tagIdsCol", "Tag Ids");
            this.dataGrid.Columns.Add("tagNamesCol", "Tag names");
            foreach (var item in allItems)
            {
                object[] newRow = { item.Id, item.Title, item.Type, item.Number, item.RunningTime, item.ReleaseYear, item.Notes,
                item.GetCommaDelimitedTags()};
                this.dataGrid.Rows.Add(newRow);
            }
        }
    }//class
}

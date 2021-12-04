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
using MyLibrary.BusinessLogic;
using MyLibrary.DataAccessLayer;

namespace MyLibrary
{
    public partial class MainWindow : Form
    {
        private MediaItemRepository _mediaItemsRepo = new MediaItemRepository(new MediaItemDataAccessor());
        private BookRepository _bookRepo = new BookRepository(new BookDataAccessor());

        public MainWindow()
        {
            InitializeComponent();

            this.Text = Configuration.APP_NAME + " " + Configuration.APP_VERSION;

            // register event handlers
            this.exitMenuItem.Click += ((sender, args) => Application.Exit());
            this.addButton.Click += ((sender, args) =>
            {
                switch (this.categoryDropDown.SelectedIndex)
                {
                    case 0:
                        new AddNewBookForm().ShowDialog();
                        break;
                    default:
                        new AddNewMediaItemForm().ShowDialog();
                        break;
                }
            });
            this.categoryDropDown.SelectedIndexChanged += ((sender, args) => 
            { 
                // I'm violating the open/closed principle here,
                // but I don't care because I don't anticipate adding any more types.
                switch (this.categoryDropDown.SelectedIndex)
                {
                    case 0:
                        DisplayBooks();
                        break;
                    case 1:
                        DisplayMediaItems();
                        break;
                    case 2:
                        DisplayMediaItems(ItemType.Cd);
                        break;
                    case 3:
                        DisplayMediaItems(ItemType.Dvd);
                        break;
                    case 4:
                        DisplayMediaItems(ItemType.BluRay);
                        break;
                    case 5:
                        DisplayMediaItems(ItemType.Vhs);
                        break;
                    case 6:
                        DisplayMediaItems(ItemType.Vinyl);
                        break;
                    case 7:
                        DisplayMediaItems(ItemType.Other);
                        break;
                    default:
                        DisplayMediaItems();
                        break;
                }
            });

            // populate "Category" combo box
            this.categoryDropDown.Items.Add(ItemType.Book);
            this.categoryDropDown.Items.Add("Media Items (all types)");
            this.categoryDropDown.Items.Add(ItemType.Cd);
            this.categoryDropDown.Items.Add(ItemType.Dvd);
            this.categoryDropDown.Items.Add(ItemType.BluRay);
            this.categoryDropDown.Items.Add(ItemType.Vhs);
            this.categoryDropDown.Items.Add(ItemType.Vinyl);
            this.categoryDropDown.Items.Add(ItemType.Other);

            // select viewing books by default
            this.categoryDropDown.SelectedIndex = 0;
        }//ctor

        private async void DisplayBooks()
        {
            // get data
            var allBooks = await this._bookRepo.GetAll();

            // populate the data grid
            this.dataGrid.Rows.Clear();
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add("idCol", "Id");
            this.dataGrid.Columns.Add("titleCol", "Title");
            this.dataGrid.Columns.Add("titleLongCol", "Long Title");
            this.dataGrid.Columns.Add("isbnCol", "ISBN");
            this.dataGrid.Columns.Add("pubCol", "Publisher");
            this.dataGrid.Columns.Add("authorsCol", "Authors");
            this.dataGrid.Columns.Add("tagsCol", "Tags");
            foreach (var book in allBooks)
            {
                object[] newRow = 
                { 
                    book.Id, 
                    book.Title, 
                    book.TitleLong,
                    book.Isbn,
                    book.Publisher.Name,
                    book.GetAuthorList(),
                    book.GetCommaDelimitedTags()
                };
                this.dataGrid.Rows.Add(newRow);
            }            
        }

        private async void DisplayMediaItems()
        {
            // get data
            var allItems = await this._mediaItemsRepo.GetAll();

            // populate the data grid
            this.dataGrid.Rows.Clear();
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add("idCol", "Id");
            this.dataGrid.Columns.Add("titleCol", "Title");
            this.dataGrid.Columns.Add("typeCol", "Type");
            this.dataGrid.Columns.Add("numCol", "Number");
            this.dataGrid.Columns.Add("runTimeCol", "Running Time");
            this.dataGrid.Columns.Add("releaseYearCol", "Release Year");
            this.dataGrid.Columns.Add("tagNamesCol", "Tags");
            foreach (var item in allItems)
            {
                object[] newRow = 
                { 
                    item.Id, 
                    item.Title, 
                    item.Type, 
                    item.Number, 
                    item.RunningTime, 
                    item.ReleaseYear, 
                    item.Notes,
                    item.GetCommaDelimitedTags()
                };
                this.dataGrid.Rows.Add(newRow);
            }
        }

        private async void DisplayMediaItems(ItemType type)
        {
            // get data
            var allItems = await this._mediaItemsRepo.GetByType(type);

            // populate the data grid
            this.dataGrid.Rows.Clear();
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add("idCol", "Id");
            this.dataGrid.Columns.Add("titleCol", "Title");
            this.dataGrid.Columns.Add("typeCol", "Type");
            this.dataGrid.Columns.Add("numCol", "Number");
            this.dataGrid.Columns.Add("runTimeCol", "Running Time");
            this.dataGrid.Columns.Add("releaseYearCol", "Release Year");
            this.dataGrid.Columns.Add("tagNamesCol", "Tags");
            foreach (var item in allItems)
            {
                object[] newRow = 
                { 
                    item.Id, 
                    item.Title, 
                    item.Type, 
                    item.Number, 
                    item.RunningTime, 
                    item.ReleaseYear,
                    item.GetCommaDelimitedTags()
                };
                this.dataGrid.Rows.Add(newRow);
            }
        }
    }//class
}
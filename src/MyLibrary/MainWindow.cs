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
using MyLibrary.Views;

namespace MyLibrary
{
    public partial class MainWindow : Form, IItemView
    {
        private MediaItemRepository _mediaItemsRepo = new MediaItemRepository();
        private BookRepository _bookRepo = new BookRepository();

        public MainWindow()
        {
            InitializeComponent();

            this.Text = Configuration.APP_NAME + " " + Configuration.APP_VERSION;

            // populate "Category" combo box
            this.categoryDropDown.Items.Add(ItemType.Book);
            this.categoryDropDown.Items.Add("Media Items (all types)");
            this.categoryDropDown.Items.Add(ItemType.Cd);
            this.categoryDropDown.Items.Add(ItemType.Dvd);
            this.categoryDropDown.Items.Add(ItemType.BluRay);
            this.categoryDropDown.Items.Add(ItemType.Vhs);
            this.categoryDropDown.Items.Add(ItemType.Vinyl);
            this.categoryDropDown.Items.Add(ItemType.Other);

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
                // fire the public event so the subscribed presenter can react
                CategorySelectionChanged?.Invoke(sender, args);
            });

            // select viewing books by default
            CategorySelectionChanged?.Invoke(this, null);
        }//ctor

        public Item SelectedItem
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public DataTable DisplayedItems
        { 
            get => throw new NotImplementedException();
            set => this.dataGrid.DataSource = value;
        }

        public int CategoryDropDownSelectedIndex
        {
            get => this.categoryDropDown.SelectedIndex;
            set => this.categoryDropDown.SelectedIndex = value;
        }

        public string StatusBarText 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        #region events
        public event EventHandler ItemSelectionChanged;
        public event EventHandler CategorySelectionChanged;
        #endregion
    }//class
}
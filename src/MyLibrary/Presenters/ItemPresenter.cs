using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using MyLibrary.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class ItemPresenter
    {
        private IBookRepository _bookRepo;
        private IMediaItemRepository _mediaItemRepo;

        private IItemView _view;
        private DataTable _allItems;

        // filter constants
        private const int FILTER_DELAY = 2000; // millis
        private const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        public ItemPresenter(IBookRepository bookRepository, IMediaItemRepository mediaItemRepository,
            IItemView view)
        {
            this._bookRepo = bookRepository;
            this._mediaItemRepo = mediaItemRepository;

            this._view = view;

            // subscribe to the view's events
            this._view.CategorySelectionChanged += CategorySelectionChanged;
            this._view.ItemSelectionChanged += ItemSelectionChanged;
            this._view.FiltersUpdated += FiltersUpdated;
            this._view.ApplyFilterButtonClicked += ApplyFilterButtonClicked;
            this._view.DeleteButtonClicked += DeleteButtonClicked;

            this._view.CategoryDropDownSelectedIndex = 0;
        }

        private async Task DisplayBooks()
        {
            // fetch the data
            var allBooks = await this._bookRepo.GetAll();

            // create DataTable to display and assign to the view
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Title");
            dt.Columns.Add("Long Title");
            dt.Columns.Add("ISBN");
            dt.Columns.Add("Publisher");
            dt.Columns.Add("Authors");
            dt.Columns.Add("Tags");
            foreach (var book in allBooks)
            {
                dt.Rows.Add(
                    book.Id, 
                    book.Title, 
                    book.TitleLong, 
                    book.Isbn, 
                    book.Publisher.Name, 
                    book.GetAuthorList(), 
                    book.GetCommaDelimitedTags()
                    );
            }
            this._view.DisplayedItems = dt;
            this._allItems = dt;

            PerformFilter();
        }

        private async Task DisplayMediaItems()
        {
            // fetch the data
            var allItems = await this._mediaItemRepo.GetAll();

            // create DataTable to display and assign to the view
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Title");
            dt.Columns.Add("Type");
            dt.Columns.Add("Number");
            dt.Columns.Add("Run Time");
            dt.Columns.Add("Release Year");
            dt.Columns.Add("Tags");
            foreach (var item in allItems)
            {
                dt.Rows.Add(
                    item.Id,
                    item.Title,
                    item.Type,
                    item.Number,
                    item.RunningTime,
                    item.ReleaseYear,
                    item.GetCommaDelimitedTags()
                    );
            }
            this._view.DisplayedItems = dt;
            this._allItems = dt;

            PerformFilter();
        }

        private async Task DisplayMediaItems(ItemType type)
        {
            // fetch the data
            var items = await this._mediaItemRepo.GetByType(type);

            // create DataTable to display and assign to the view
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Title");
            dt.Columns.Add("Type");
            dt.Columns.Add("Number");
            dt.Columns.Add("Run Time");
            dt.Columns.Add("Release Year");
            dt.Columns.Add("Tags");
            foreach (var item in items)
            {
                dt.Rows.Add(
                    item.Id,
                    item.Title,
                    item.Type,
                    item.Number,
                    item.RunningTime,
                    item.ReleaseYear,
                    item.GetCommaDelimitedTags()
                    );
            }
            this._view.DisplayedItems = dt;
            this._allItems = dt;

            PerformFilter();
        }

        #region View event handlers
        public async void DeleteButtonClicked(object sender, EventArgs args)
        {
            // delete the item
            if (this._view.CategoryDropDownSelectedIndex == 0)
            {
                // book
                BookDataAccessor dao = new BookDataAccessor();
                await dao.DeleteById(this._view.SelectedItemId);
            }
            else
            {
                // media item
                MediaItemDataAccessor dao = new MediaItemDataAccessor();
                await dao.DeleteById(this._view.SelectedItemId);
            }

            // update the view
            await DisplayItems();
        }

        /// <summary>
        /// Apply filter button clicked. No time delay in filtering.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ApplyFilterButtonClicked(object sender, EventArgs e)
        {
            PerformFilter();
        }

        /// <summary>
        /// Filter has been updated. Include a time delay in filtering.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void FiltersUpdated(object sender, EventArgs e)
        {
            await Task.Delay(FILTER_DELAY);
            PerformFilter();
        }

        public void PerformFilter()
        {
            // grab the data to filter
            // and determine whether we need to filter
            DataTable oldDt = this._view.DisplayedItems.Copy();
            string filterBy = this._view.TitleFilterText;
            if (string.IsNullOrWhiteSpace(filterBy))
            {
                // not filtering

                this._view.DisplayedItems = this._allItems.Copy();

                // update status bar
                this._view.StatusText = "Ready.";
                this._view.ItemsDisplayedText = SetItemsDisplayedStatusText(1, this._view.DisplayedItems.Rows.Count, this._allItems.Rows.Count);

                return;
            }

            // apply filter and display
            // currently only filtering by title is supported
            Regex filterPattern = new Regex(filterBy, REGEX_OPTIONS);
            DataTable filteredDt = this._allItems.Clone();
            var rows = this._allItems.AsEnumerable()
                .Where(row => filterPattern.IsMatch(row.Field<string>("Title")));
            foreach (var row in rows)
            {
                filteredDt.ImportRow(row);
            }
            this._view.DisplayedItems = filteredDt;

            // update status bar
            this._view.StatusText = "Ready.";
            this._view.ItemsDisplayedText = SetItemsDisplayedStatusText(1, this._view.DisplayedItems.Rows.Count, this._allItems.Rows.Count);
        }

        public async void ItemSelectionChanged(object sender, EventArgs e)
        {
            if (this._view.SelectedItemId == 0)
                return;

            if (this._view.CategoryDropDownSelectedIndex == 0)
            {
                // book
                this._view.SelectedItem = await this._bookRepo.GetById(this._view.SelectedItemId);
            }
            else
            {
                // media item
                this._view.SelectedItem = await this._mediaItemRepo.GetById(this._view.SelectedItemId);
            }
        }

        public async void CategorySelectionChanged(object sender, EventArgs e)
        {
            await DisplayItems();
        }
        #endregion

        private async Task DisplayItems()
        {
            // update status bar
            this._view.StatusText = "Updating...";

            // update the view
            switch (this._view.CategoryDropDownSelectedIndex)
            {
                case 0:
                    await DisplayBooks();
                    break;
                case 1:
                    await DisplayMediaItems();
                    break;
                default:
                    await DisplayMediaItems((ItemType)this._view.CategoryDropDownSelectedIndex-1);
                    break;
            }

            // update status bar
            this._view.StatusText = "Ready.";
            this._view.ItemsDisplayedText = SetItemsDisplayedStatusText(1, this._view.DisplayedItems.Rows.Count, this._allItems.Rows.Count);
        }

        private string SetItemsDisplayedStatusText(int numberSelected, int numberDisplayed, int total)
        {
            return (numberSelected + " items selected. " + numberDisplayed + " of " + total + " items displayed.");
        }
    }//class
}
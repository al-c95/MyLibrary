using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using MyLibrary.BusinessLogic;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class ItemPresenter
    {
        private BookRepository _bookRepo;
        private MediaItemRepository _mediaItemRepo;

        private TagRepository _tagRepo;

        private IItemView _view;
        protected DataTable _allItems;

        private ItemMemento _selectedItemMemento;

        // filter constants
        private const int FILTER_DELAY = 2000; // millis
        private const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        public ItemPresenter(BookRepository bookRepository, MediaItemRepository mediaItemRepository,
            IItemView view)
        {
            this._bookRepo = bookRepository;
            this._mediaItemRepo = mediaItemRepository;

            this._tagRepo = new TagRepository();

            this._view = view;

            // subscribe to the view's events
            this._view.CategorySelectionChanged += CategorySelectionChanged;
            this._view.ItemSelectionChanged += ItemSelectionChanged;
            this._view.FiltersUpdated += FiltersUpdated; //
            this._view.ApplyFilterButtonClicked += ApplyFilterButtonClicked; //
            this._view.DeleteButtonClicked += DeleteButtonClicked;
            this._view.UpdateSelectedItemButtonClicked += UpdateSelectedItemButtonClicked;
            this._view.SelectedItemModified += SelectedItemModified; //
            this._view.DiscardSelectedItemChangesButtonClicked += DiscardSelectedItemChangesButtonClicked;
            this._view.TagsUpdated += TagsUpdated;
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
            // grab the filters
            string filterByTitle = this._view.TitleFilterText;
            IEnumerable<string> filterByTags = this._view.SelectedFilterTags;

            // perform filtering
            DataTable filteredTable = this._allItems.Copy();
            if (!string.IsNullOrWhiteSpace(filterByTitle))
            {
                filteredTable = FilterByTitle(filteredTable, filterByTitle);
            }
            if (filterByTags.Count() > 0)
            {
                filteredTable = FilterByTags(filteredTable, filterByTags);
            }

            // update the view
            this._view.DisplayedItems = filteredTable;

            // update status bar
            this._view.StatusText = "Ready.";
            this._view.ItemsDisplayedText = SetItemsDisplayedStatusText(1, this._view.DisplayedItems.Rows.Count, this._allItems.Rows.Count);
        }

        private DataTable FilterByTitle(DataTable originalTable, string filterByTitle)
        {
            Regex filterPattern = new Regex(filterByTitle, REGEX_OPTIONS);

            DataTable filteredTable = originalTable.Clone();
            var rows = originalTable.AsEnumerable()
                .Where(row => filterPattern.IsMatch(row.Field<string>("Title")));
            foreach (var row in rows)
                filteredTable.ImportRow(row);

            return filteredTable;
        }

        private DataTable FilterByTags(DataTable originalTable, IEnumerable<string> filterByTags)
        {
            DataTable filteredTable = originalTable.Clone();
            var rows = originalTable.AsEnumerable();
            foreach (var row in rows)
            {
                foreach (var tag in filterByTags)
                {
                    if (row.Field<string>("Tags").Contains(tag))
                        filteredTable.ImportRow(row);
                }
            }

            return filteredTable;
        }

        public async void ItemSelectionChanged(object sender, EventArgs e)
        {
            if (this._view.SelectedItemId == 0)
            {
                return;
            }
;
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
            this._selectedItemMemento = this._view.SelectedItem.GetMemento();
            this._view.DiscardSelectedItemChangesButtonEnabled = false;
        }

        public async void CategorySelectionChanged(object sender, EventArgs e)
        {
            await DisplayItems();
        }

        public async void UpdateSelectedItemButtonClicked(object sender, EventArgs e)
        {
            if (this._view.CategoryDropDownSelectedIndex == 0)
            {
                // book
                BookDataAccessor dao = new BookDataAccessor();
                await dao.Update((Book)this._view.SelectedItem);
            }
            else
            {
                // media item
                MediaItemDataAccessor dao = new MediaItemDataAccessor();
                await dao.Update((MediaItem)this._view.SelectedItem);
            }

            // update the view
            await DisplayItems();
        }

        public void SelectedItemModified(object sender, EventArgs e)
        {
            this._view.UpdateSelectedItemButtonEnabled = true;
            this._view.DiscardSelectedItemChangesButtonEnabled = true;
        }

        public void DiscardSelectedItemChangesButtonClicked(object sender, EventArgs e)
        {
            Item temp = this._view.SelectedItem;
            temp.Restore(this._selectedItemMemento);

            this._view.SelectedItem = temp;

            this._view.DiscardSelectedItemChangesButtonEnabled = false;
        }

        public async void TagsUpdated(object sender, EventArgs e)
        {
            await DisplayTags();
        }
        #endregion

        private async Task DisplayTags()
        {
            Dictionary<string, bool> tagsAndCheckedStatuses = new Dictionary<string, bool>();

            var allTags = await this._tagRepo.GetAll();
            IEnumerable<string> checkedTags = this._view.SelectedFilterTags;
            foreach (var tagName in checkedTags)
            {
                if (allTags.Any(t => t.Name==tagName))
                    tagsAndCheckedStatuses.Add(tagName, true);
            }
            foreach (var tag in await this._tagRepo.GetAll())
            {
                string tagName = tag.Name;
                if (!tagsAndCheckedStatuses.ContainsKey(tagName))
                    tagsAndCheckedStatuses.Add(tagName, false);
            }

            this._view.PopulateFilterTags(tagsAndCheckedStatuses);
        }

        private async Task DisplayItems()
        {
            // update status bar
            this._view.StatusText = "Updating...";
            this._view.ItemsDisplayedText = null;

            // update the view
            await DisplayTags();
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

            PerformFilter();
        }

        private string SetItemsDisplayedStatusText(int numberSelected, int numberDisplayed, int total)
        {
            return (numberSelected + " items selected. " + numberDisplayed + " of " + total + " items displayed.");
        }
    }//class
}
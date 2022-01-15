//MIT License

//Copyright (c) 2021

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using MyLibrary.ApiService;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Views;
using MyLibrary.Utils;

namespace MyLibrary.Presenters
{
    public class ItemPresenter
    {
        private IBookService _bookService;
        private IMediaItemService _mediaItemService;

        private ITagService _tagService;

        private IAuthorService _authorService;

        private IPublisherService _publisherService;

        private IItemView _view;

        private IAddMediaItemForm _addMediaItemView;
        private IAddBookForm _addBookView;

        protected DataTable _allItems;

        private ItemMemento _selectedItemMemento;

        private const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        public ItemPresenter(IBookService bookService, IMediaItemService mediaItemService,
            IItemView view)
        {
            this._bookService = bookService;
            this._mediaItemService = mediaItemService;

            this._tagService = new TagService();

            this._authorService = new AuthorService();

            this._publisherService = new PublisherService();

            this._view = view;

            // subscribe to the view's events
            this._view.CategorySelectionChanged += CategorySelectionChanged;
            this._view.ItemSelectionChanged += ItemSelectionChanged;
            this._view.FiltersUpdated += PerformFilter; //FiltersUpdated; //
            this._view.ApplyFilterButtonClicked += PerformFilter; //FiltersUpdated; //ApplyFilterButtonClicked; //
            this._view.DeleteButtonClicked += DeleteButtonClicked;
            this._view.UpdateSelectedItemButtonClicked += UpdateSelectedItemButtonClicked;
            this._view.SelectedItemModified += SelectedItemModified; //
            this._view.DiscardSelectedItemChangesButtonClicked += DiscardSelectedItemChangesButtonClicked;
            this._view.TagsUpdated += TagsUpdated;
            this._view.AddNewMediaItemClicked += AddNewMediaItemClicked;
            this._view.AddNewBookClicked += AddNewBookClicked;
            this._view.SearchByIsbnClicked += SearchByIsbnClicked;
            this._view.ShowStatsClicked += ShowStatsClicked;
        }

        #region View event handlers
        public async void DeleteButtonClicked(object sender, EventArgs args)
        {
            // delete the item
            try
            {
                if (this._view.CategoryDropDownSelectedIndex == 0)
                {
                    // book
                    await this._bookService.DeleteById(this._view.SelectedItemId);
                }
                else
                {
                    // media item
                    await this._mediaItemService.DeleteById(this._view.SelectedItemId);
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error deleting item.", ex.Message);
                return;
            }

            // update the view
            await DisplayItems();
        }

        public void PerformFilter(object sender, EventArgs e)
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

            UpdateStatusBarAndSelectedItemDetails();
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
            this._view.DeleteItemButtonEnabled = this._view.IsItemSelected;

            if (this._view.SelectedItemId == 0)
            {
                return;
            }

            if (this._view.CategoryDropDownSelectedIndex == 0)
            {
                // book
                this._view.SelectedItem = await this._bookService.GetById(this._view.SelectedItemId);
            }
            else
            {
                // media item
                this._view.SelectedItem = await this._mediaItemService.GetById(this._view.SelectedItemId);
            }
            this._selectedItemMemento = this._view.SelectedItem.GetMemento();
            this._view.DiscardSelectedItemChangesButtonEnabled = false;

            this._view.SelectedItemDetailsBoxEntry = this._view.SelectedItem.ToString();
        }

        public async void CategorySelectionChanged(object sender, EventArgs e)
        {
            await DisplayItems();
        }

        public async void UpdateSelectedItemButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (this._view.CategoryDropDownSelectedIndex == 0)
                {
                    // book
                    await this._bookService.Update((Book)this._view.SelectedItem);
                }
                else
                {
                    // media item
                    await this._mediaItemService.Update((MediaItem)this._view.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error updating item.", ex.Message);
                return;
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
            await DisplayItems();
        }

        public async void AddNewMediaItemClicked(object sender, EventArgs e)
        {
            this._addMediaItemView = new AddNewMediaItemForm();
            var addItemPresenter = new AddMediaItemPresenter(this._mediaItemService, this._tagService,
                this._addMediaItemView,
                new ImageFileReader());
            await addItemPresenter.PopulateTagsList();

            this._addMediaItemView.ItemAdded += ItemsAdded;

            int categoryIndexToSelect;
            if (this._view.CategoryDropDownSelectedIndex > 2)
            {
                categoryIndexToSelect = this._view.CategoryDropDownSelectedIndex - 2;
            }
            else
            {
                categoryIndexToSelect = 0;
            }
            this._addMediaItemView.SelectedCategoryIndex = categoryIndexToSelect;

            ((AddNewMediaItemForm)this._addMediaItemView).ShowDialog();
        }

        public async void AddNewBookClicked(object sender, EventArgs e)
        {
            this._addBookView = new AddNewBookForm();
            var addBookPresenter = new AddBookPresenter(this._bookService, this._tagService, this._authorService, this._publisherService,
                this._addBookView);
            await addBookPresenter.PopulateTagsList();
            await addBookPresenter.PopulateAuthorList();
            await addBookPresenter.PopulatePublisherList();

            this._addBookView.ItemAdded += ItemsAdded;
            ((AddNewBookForm)this._addBookView).ShowDialog();
        }

        public void SearchByIsbnClicked(object sender, EventArgs e)
        {
            SearchByIsbnDialog searchDialog = new SearchByIsbnDialog();
            this._addBookView = new AddNewBookForm();
            var searchPresenter = new SearchByIsbnPresenter(searchDialog, this._view, this._addBookView, new BookService(), new ApiServiceProvider());
            searchPresenter.AddBookPresenter = new AddBookPresenter(this._bookService, this._tagService, this._authorService, this._publisherService,
                this._addBookView);
            searchDialog.ShowDialog();

            ItemsAdded(null, null);
        }

        public async void ItemsAdded(object sender, EventArgs e)
        {
            await DisplayItems();
        }

        public async void ShowStatsClicked(object sender, EventArgs e)
        {
            ShowStatsDialog statsDialog = new ShowStatsDialog();
            StatsPresenter statsPresenter = new StatsPresenter(statsDialog, this._bookService, this._mediaItemService, this._tagService, this._publisherService, this._authorService);
            await statsPresenter.ShowStats();
            statsDialog.ShowDialog();
        }
        #endregion

        private async Task DisplayBooks()
        {
            // fetch the data
            var allBooks = await this._bookService.GetAll();

            // create DataTable to display and assign to the view
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Title");
            dt.Columns.Add("ISBN");
            dt.Columns.Add("Publisher");
            dt.Columns.Add("Authors");
            dt.Columns.Add("Tags");
            foreach (var book in allBooks)
            {
                dt.Rows.Add(
                    book.Id, 
                    book.Title, 
                    book.GetIsbn(), 
                    book.Publisher.Name, 
                    book.GetAuthorList(), 
                    book.GetCommaDelimitedTags()
                    );
            }
            this._view.DisplayedItems = dt;
            this._allItems = dt;

            PerformFilter(null,null);
        }

        private async Task DisplayMediaItems()
        {
            // fetch the data
            var allItems = await this._mediaItemService.GetAll();

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

            PerformFilter(null,null);
        }

        private async Task DisplayMediaItems(ItemType type)
        {
            // fetch the data
            var items = await this._mediaItemService.GetByType(type);

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

            PerformFilter(null,null);
        }

        private async Task DisplayTags()
        {
            Dictionary<string, bool> tagsAndCheckedStatuses = new Dictionary<string, bool>();

            var allTags = await this._tagService.GetAll();
            IEnumerable<string> checkedTags = this._view.SelectedFilterTags;
            foreach (var tagName in checkedTags)
            {
                if (allTags.Any(t => t.Name==tagName))
                    tagsAndCheckedStatuses.Add(tagName, true);
            }
            foreach (var tag in await this._tagService.GetAll())
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
            this._view.StatusText = "Please Wait...";
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

            PerformFilter(null,null);

            UpdateStatusBarAndSelectedItemDetails();
        }

        private void UpdateStatusBarAndSelectedItemDetails()
        {
            // update status bar
            this._view.StatusText = "Ready.";
            this._view.ItemsDisplayedText = SetItemsDisplayedStatusText(this._view.NumberOfItemsSelected, this._view.DisplayedItems.Rows.Count, this._allItems.Rows.Count);
            // "unselect" item if no items displayed
            if (this._view.DisplayedItems.Rows.Count == 0)
            {
                this._view.SelectedItem = null;
            }
        }

        private string SetItemsDisplayedStatusText(int numberSelected, int numberDisplayed, int total)
        {
            return (numberSelected + " items selected. " + numberDisplayed + " of " + total + " items displayed.");
        }
    }//class
}
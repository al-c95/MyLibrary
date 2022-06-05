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
using MyLibrary.Presenters.ServiceProviders;

namespace MyLibrary.Presenters
{
    /// <summary>
    /// Contains most of the logic which controls the main UI and interacts with the models.
    /// </summary>
    public class ItemPresenter
    {
        private IBookService _bookService;
        private IMediaItemService _mediaItemService;

        private ITagService _tagService;

        private IAuthorService _authorService;

        private IPublisherService _publisherService;

        private IMainWindow _view;

        private IAddMediaItemForm _addMediaItemView;
        private IAddBookForm _addBookView;

        protected DataTable _allItems;

        private ItemMemento _selectedItemMemento;

        private const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="mediaItemService"></param>
        /// <param name="tagService"></param>
        /// <param name="authorService"></param>
        /// <param name="publisherService"></param>
        /// <param name="view"></param>
        public ItemPresenter(IBookService bookService, IMediaItemService mediaItemService, ITagService tagService, IAuthorService authorService, IPublisherService publisherService,
            IMainWindow view)
        {
            this._bookService = bookService;
            this._mediaItemService = mediaItemService;

            this._tagService = tagService;

            this._authorService = authorService;

            this._publisherService = publisherService;

            this._view = view;

            // subscribe to the view's events
            this._view.CategorySelectionChanged += CategorySelectionChanged;
            this._view.ItemSelectionChanged += ItemSelectionChanged;
            this._view.FiltersUpdated += PerformFilter;
            this._view.ApplyFilterButtonClicked += PerformFilter;
            this._view.DeleteButtonClicked += (async (sender, args) => 
            { 
                await HandleDeleteButtonClicked(sender, args); 
            });
            this._view.UpdateSelectedItemButtonClicked += UpdateSelectedItemButtonClicked;
            this._view.SelectedItemModified += SelectedItemModified;
            this._view.DiscardSelectedItemChangesButtonClicked += DiscardSelectedItemChangesButtonClicked;
            this._view.TagsUpdated += TagsUpdated;
            this._view.AddNewMediaItemClicked += AddNewMediaItemClicked;
            this._view.AddNewBookClicked += AddNewBookClicked;
            this._view.SearchByIsbnClicked += SearchByIsbnClicked;
            this._view.ShowStatsClicked += ShowStatsClicked;
            this._view.WishlistButtonClicked += (async (sender, args) =>
            {
                await HandleWishlistButtonClicked(sender, args);
            });
        }

        #region View event handlers
        public async Task HandleWishlistButtonClicked(object sender, EventArgs args)
        {
            var form = new WishlistDialog();
            WishlistPresenter presenter = new WishlistPresenter(form, new WishlistServiceProvider());
            await presenter.LoadData();
            form.ShowDialog();
        }

        public async Task HandleDeleteButtonClicked(object sender, EventArgs args)
        {
            // delete the item
            try
            {
                int selectedItemId = this._view.SelectedItemId;
                if (this._view.CategoryDropDownSelectedIndex == 0)
                {
                    await this._bookService.DeleteById(selectedItemId);
                }
                else
                {
                    await this._mediaItemService.DeleteById(selectedItemId);
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
            {
                filteredTable.ImportRow(row);
            }

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
            // update status bar
            this._view.StatusText = "Please Wait...";
            this._view.ItemsDisplayedText = string.Empty;

            // delete item button enabled if item is selected
            this._view.DeleteItemButtonEnabled = this._view.IsItemSelected;

            int selectedItemId = this._view.SelectedItemId;
            if (selectedItemId == 0)
            {
                // nothing is selected
                // nothing to do
                return;
            }

            // show spinner
            // and disable things the user shouldn't play with at this point
            this._view.ItemDetailsSpinner = true;
            this._view.FilterGroupEnabled = false;
            this._view.CategoryGroupEnabled = false;
            this._view.AddItemEnabled = false;
            this._view.SearchBooksButtonEnabled = false;
            this._view.ViewStatisticsEnabled = false;
            this._view.TagsButtonEnabled = false;
            this._view.WishlistButtonEnabled = false;
            this._view.DeleteItemButtonEnabled = false;

            if (this._view.CategoryDropDownSelectedIndex == 0)
            {

                this._view.SelectedItem = await this._bookService.GetById(selectedItemId);
            }
            else
            {
                this._view.SelectedItem = await this._mediaItemService.GetById(selectedItemId);
            }

            this._selectedItemMemento = this._view.SelectedItem.GetMemento();
            this._view.DiscardSelectedItemChangesButtonEnabled = false;

            this._view.SelectedItemDetailsBoxEntry = this._view.SelectedItem.ToString();

            // update status bar
            this._view.StatusText = "Ready.";
            this._view.ItemsDisplayedText = SetItemsDisplayedStatusText(this._view.NumberOfItemsSelected, this._view.DisplayedItems.Rows.Count, this._allItems.Rows.Count);

            // hide spinner
            // and re-enable controls
            this._view.ItemDetailsSpinner = false;
            this._view.FilterGroupEnabled = true;
            this._view.CategoryGroupEnabled = true;
            this._view.AddItemEnabled = true;
            this._view.SearchBooksButtonEnabled = true;
            this._view.ViewStatisticsEnabled = true;
            this._view.TagsButtonEnabled = true;
            this._view.WishlistButtonEnabled = true;
            this._view.DeleteItemButtonEnabled = true;
        }//ItemSelectionChanged

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
                    await this._bookService.Update((Book)this._view.SelectedItem);
                }
                else
                {
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
                this._addBookView, new ImageFileReader());
            await addBookPresenter.PopulateTagsList();
            await addBookPresenter.PopulateAuthorsList();
            await addBookPresenter.PopulatePublishersList();

            this._addBookView.ItemAdded += ItemsAdded;
            ((AddNewBookForm)this._addBookView).ShowDialog();
        }

        public void SearchByIsbnClicked(object sender, EventArgs e)
        {
            SearchByIsbnDialog searchDialog = new SearchByIsbnDialog();
            this._addBookView = new AddNewBookForm();
            var searchPresenter = new SearchByIsbnPresenter(searchDialog, this._view, this._addBookView, new BookService(), new ApiServiceProvider());
            searchPresenter.AddBookPresenter = new AddBookPresenter(this._bookService, this._tagService, this._authorService, this._publisherService,
                this._addBookView, new ImageFileReader());
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
            
            this._allItems = dt;
        }

        private async Task DisplayMediaItems()
        {
            CreateMediaItemsTable(await this._mediaItemService.GetAll());
        }

        private async Task DisplayMediaItems(ItemType type)
        {
            CreateMediaItemsTable(await this._mediaItemService.GetByType(type));
        }

        /// <summary>
        /// Creates DataTable to display. Assigns it to the view.
        /// </summary>
        /// <param name="items"></param>
        private void CreateMediaItemsTable(IEnumerable<MediaItem> items)
        {
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

            this._allItems = dt;
        }

        private async Task DisplayTags()
        {
            Dictionary<string, bool> tagsAndCheckedStatuses = new Dictionary<string, bool>();

            var allTags = await this._tagService.GetAll();
            IEnumerable<string> checkedTags = this._view.SelectedFilterTags;
            foreach (var tagName in checkedTags)
            {
                if (allTags.Any(t => t.Name == tagName))
                {
                    tagsAndCheckedStatuses.Add(tagName, true);
                }
            }
            foreach (var tag in await this._tagService.GetAll())
            {
                string tagName = tag.Name;
                if (!tagsAndCheckedStatuses.ContainsKey(tagName))
                {
                    tagsAndCheckedStatuses.Add(tagName, false);
                }
            }

            this._view.PopulateFilterTags(tagsAndCheckedStatuses);
        }//DisplayTags

        private async Task DisplayItems()
        {
            // update status bar
            this._view.StatusText = "Please Wait...";
            this._view.ItemsDisplayedText = null;

            // update the view
            // tags
            await DisplayTags();
            // items
            int categorySelectionIndex = this._view.CategoryDropDownSelectedIndex;
            if (categorySelectionIndex == 0)
            {
                await DisplayBooks();
            }
            else if (categorySelectionIndex == 1)
            {
                await DisplayMediaItems();
            }
            else
            {
                await DisplayMediaItems((ItemType)categorySelectionIndex - 1);
            }
            // filter items
            PerformFilter(null,null);

            UpdateStatusBarAndSelectedItemDetails();
        }//DisplayItems

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
//MIT License

//Copyright (c) 2021-2023

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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using MyLibrary.ApiService;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Views;
using MyLibrary.Utils;
using MyLibrary.Presenters.ServiceProviders;
using MyLibrary.Events;

namespace MyLibrary.Presenters
{
    /// <summary>
    /// Contains most of the logic which controls the main UI and interacts with the models.
    /// </summary>
    public class MainWindowPresenter
    {
        private IBookService _bookService;
        private IMediaItemService _mediaItemService;
        private ITagService _tagService;
        private IAuthorService _authorService;
        private IPublisherService _publisherService;

        private IMainWindow _view;
        private IAddMediaItemForm _addMediaItemView;
        private IAddBookForm _addBookView;

        private readonly StatsPresenter _statsPresenter;

        protected DataTable _allItems;

        protected ItemMemento _selectedItemMemento;

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
        public MainWindowPresenter(IBookService bookService, IMediaItemService mediaItemService, ITagService tagService, IAuthorService authorService, IPublisherService publisherService,
            IMainWindow view)
        {
            this._bookService = bookService;
            this._mediaItemService = mediaItemService;

            this._tagService = tagService;

            this._authorService = authorService;

            this._publisherService = publisherService;

            this._view = view;

            // subscribe to the view's events
            this._view.CategorySelectionChanged += (async (sender, args) => { await DisplayItems(); });
            this._view.WindowCreated += (async (sender, args) => { await DisplayItems(); });
            this._view.ItemSelectionChanged += (async (sender, args) =>
            {
                await HandleItemSelectionChanged(sender, args);
            });
            this._view.FiltersUpdated += PerformFilter;
            this._view.ApplyFilterButtonClicked += PerformFilter;
            this._view.DeleteButtonClicked += (async (sender, args) =>
            {
                await HandleDeleteButtonClicked(sender, args);
            });
            this._view.UpdateSelectedItemButtonClicked += (async (sender, args) => { await HandleUpdateSelectedItemButtonClicked(); });
            this._view.SelectedItemModified += SelectedItemModified;
            this._view.DiscardSelectedItemChangesButtonClicked += DiscardSelectedItemChangesButtonClicked;
            this._view.TagsUpdated += TagsUpdated;
            this._view.AddNewMediaItemClicked += (async (sender, args) => { await ShowAddNewMediaItem(); });
            this._view.AddNewBookClicked += (async (sender, args) => { await ShowAddNewBook(); });
            this._view.AddNewItemButtonClicked += HandleAddNewItemButtonClicked;
            this._view.SearchByIsbnClicked += (async (sender, args) => { await SearchByIsbnClicked(); });
            this._view.ShowStatsClicked += ShowStatsClicked;
            this._view.WishlistButtonClicked += (async (sender, args) =>
            {
                await HandleWishlistButtonClicked(sender, args);
            });
            this._view.ManageCopiesButtonClicked += (async (sender, args) =>
            {
                var form = new ManageCopiesForm(this._view.SelectedItem);
                ManageCopiesPresenter presenter = new ManageCopiesPresenter(form, this._view.SelectedItem, new CopyServiceFactory());
                await presenter.LoadData(sender, args);
                form.FormClosed += ((s, a) => { presenter.Dispose(); });
                form.Show();
            });

            EventAggregator.GetInstance().Subscribe<TagsUpdatedEvent>(async m => await DisplayItems());
            EventAggregator.GetInstance().Subscribe<MediaItemsUpdatedEvent>(async m => await DisplayItems());
            EventAggregator.GetInstance().Subscribe<BooksUpdatedEvent>(async m => await DisplayItems());
        }

        #region View event handlers
        public async Task HandleWishlistButtonClicked(object sender, EventArgs args)
        {
            var form = new WishlistForm();
            WishlistPresenter presenter = new WishlistPresenter(form, new WishlistServiceProvider());
            await presenter.LoadData();
            form.FormClosed += (s, a) => { presenter.Dispose(); };
            form.Show();
        }

        public async Task HandleDeleteButtonClicked(object sender, EventArgs args)
        {
            if (!this._view.ShowDeleteConfirmationDialog(this._view.SelectedItem.Title))
            {
                return;
            }

            try
            {
                int selectedItemId = this._view.SelectedItemId;
                if (this._view.CategoryDropDownSelectedIndex == 0)
                {
                    await this._bookService.DeleteByIdAsync(selectedItemId);
                }
                else
                {
                    await this._mediaItemService.DeleteByIdAsync(selectedItemId);
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
            string filterByTitle = this._view.TitleFilterText;
            IEnumerable<string> filterByTags = this._view.SelectedFilterTags;

            DataTable filteredTable = this._allItems.Copy();
            if (!string.IsNullOrWhiteSpace(filterByTitle))
            {
                filteredTable = FilterByTitle(filteredTable, filterByTitle);
            }
            if (filterByTags.Count() > 0)
            {
                filteredTable = FilterByTags(filteredTable, filterByTags);
            }

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
                    {
                        filteredTable.ImportRow(row);
                    }
                }
            }

            return filteredTable;
        }

        public async Task HandleItemSelectionChanged(object sender, EventArgs e)
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
                this._view.SelectedItem = await this._bookService.GetByIdAsync(selectedItemId);
            }
            else
            {
                this._view.SelectedItem = await this._mediaItemService.GetByIdAsync(selectedItemId);
            }

            this._selectedItemMemento = this._view.SelectedItem.GetMemento();
            this._view.DiscardSelectedItemChangesButtonEnabled = false;

            this._view.SelectedItemDetailsBoxEntry = this._view.SelectedItem.ToString();

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

        public async Task HandleUpdateSelectedItemButtonClicked()
        {
            try
            {
                if (this._view.CategoryDropDownSelectedIndex == 0)
                {
                    await this._bookService.UpdateAsync((Book)this._view.SelectedItem, true);
                }
                else
                {
                    await this._mediaItemService.UpdateAsync((MediaItem)this._view.SelectedItem, true);
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error updating item.", ex.Message);
                return;
            }

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
            await LoadTags();
            await DisplayItems();
        }

        public async void HandleAddNewItemButtonClicked(object sender, EventArgs args)
        {
            int selectedCategoryIndex = this._view.CategoryDropDownSelectedIndex;
            if (selectedCategoryIndex == 0)
            {
                await ShowAddNewBook();
            }
            else
            {
                await ShowAddNewMediaItem();
            }
        }

        private async Task ShowAddNewBook()
        {
            this._addBookView = new AddNewBookForm();
            var addBookPresenter = new AddBookPresenter(this._bookService,
                this._tagService, this._authorService,
                this._publisherService,
                this._addBookView,
                new ImageFileReader());
            await addBookPresenter.PopulateTagsList();
            await addBookPresenter.PopulateAuthorsList();
            await addBookPresenter.PopulatePublishersList();

            ((AddNewBookForm)this._addBookView).Show();
        }

        private async Task ShowAddNewMediaItem()
        {
            this._addMediaItemView = new AddNewMediaItemForm();
            var addItemPresenter = new AddMediaItemPresenter(this._mediaItemService,
                this._tagService,
                this._addMediaItemView,
                new ImageFileReader(),
                new NewTagOrPublisherInputBoxProvider());
            await addItemPresenter.PopulateTagsList();
            int selectedCategoryIndex = this._view.CategoryDropDownSelectedIndex;
            if (selectedCategoryIndex == 1)
            {
                this._addMediaItemView.SelectedCategoryIndex = 0;
            }
            else
            {
                this._addMediaItemView.SelectedCategoryIndex = this._view.CategoryDropDownSelectedIndex - 2;
            }

            ((AddNewMediaItemForm)this._addMediaItemView).Show();
        }

        public async Task SearchByIsbnClicked()
        {
            SearchByIsbnDialog searchDialog = new SearchByIsbnDialog();
            this._addBookView = new AddNewBookForm();
            var searchPresenter = new SearchByIsbnPresenter(searchDialog, this._addBookView, new BookService(), new ApiServiceProvider());
            searchPresenter.AddBookPresenter = new AddBookPresenter(this._bookService, this._tagService, this._authorService, this._publisherService,
                this._addBookView, new ImageFileReader());
            searchDialog.ShowDialog();
            searchDialog.Dispose();

            await DisplayItems();
        }

        public async void ShowStatsClicked(object sender, EventArgs e)
        {
            using (var statsDialog = new ShowStatsDialog())
            {
                StatsPresenter statsPresenter = new StatsPresenter(statsDialog, new DataAccessLayer.StatsService());
                await statsPresenter.LoadDataAsync();
                statsDialog.ShowDialog();
            }
        }
        #endregion

        private async Task DisplayBooks()
        {
            // fetch the data
            var allBooks = await this._bookService.GetAllAsync();

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
            CreateMediaItemsTable(await this._mediaItemService.GetAllAsync());
        }

        private async Task DisplayMediaItems(ItemType type)
        {
            CreateMediaItemsTable(await this._mediaItemService.GetByTypeAsync(type));
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
                    Item.GetTypeString(item.Type),
                    item.Number,
                    item.RunningTime,
                    item.ReleaseYear,
                    item.GetCommaDelimitedTags()
                    );
            }

            this._allItems = dt;
        }

        private async Task LoadTags()
        {
            var allTags = await this._tagService.GetAll();
            List<string> allTagNames = new List<string>();
            foreach (var tag in allTags)
            {
                allTagNames.Add(tag.Name);
            }

            this._view.LoadFilterTags(allTagNames);
        }//LoadTags

        private async Task DisplayItems()
        {
            // update status bar
            this._view.StatusText = "Please Wait...";
            this._view.ItemsDisplayedText = null;

            // update the view
            await LoadTags();
            int categorySelectionIndex = this._view.CategoryDropDownSelectedIndex;
            if (categorySelectionIndex == 0)
            {
                await DisplayBooks();
            }
            else if (categorySelectionIndex == 1)
            {
                await DisplayMediaItems();
            }
            else if (categorySelectionIndex == 2)
            {
                await DisplayMediaItems(ItemType.Cassette);
            }
            else if (categorySelectionIndex == 3)
            {
                await DisplayMediaItems(ItemType.Cd);
            }
            else if (categorySelectionIndex == 4)
            {
                await DisplayMediaItems(ItemType.Dvd);
            }
            else if (categorySelectionIndex == 5)
            {
                await DisplayMediaItems(ItemType.BluRay);
            }
            else if (categorySelectionIndex == 6)
            {
                await DisplayMediaItems(ItemType.UhdBluRay);
            }
            else if (categorySelectionIndex == 7)
            {
                await DisplayMediaItems(ItemType.Vhs);
            }
            else if (categorySelectionIndex == 8)
            {
                await DisplayMediaItems(ItemType.Vinyl);
            }
            else if (categorySelectionIndex == 9)
            {
                await DisplayMediaItems(ItemType.FlashDrive);
            }
            else if (categorySelectionIndex == 10)
            {
                await DisplayMediaItems(ItemType.FloppyDisk);
            }
            else if (categorySelectionIndex == 11)
            {
                await DisplayMediaItems(ItemType.Other);
            }

            PerformFilter(null, null);

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
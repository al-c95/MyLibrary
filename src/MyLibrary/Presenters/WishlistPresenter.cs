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
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using MyLibrary.Views;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Events;
using MyLibrary.Utils;
using MyLibrary.Models.Entities.Factories;

namespace MyLibrary.Presenters
{
    public class WishlistPresenter : IDisposable
    {
        private IWishlistForm _view;
        private IWishlistServiceProvider _serviceProvider;

        private DataTable _allItems;

        private const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        public WishlistPresenter(IWishlistForm view, IWishlistServiceProvider serviceProvider)
        {
            this._view = view;
            this._serviceProvider = serviceProvider;

            // register view event handlers
            this._view.ItemSelected += ItemSelected;
            this._view.SaveSelectedClicked += (async (sender, args) =>
            {
                await SaveSelectedClicked(sender, args);
            });
            this._view.DiscardChangesClicked += DiscardChangesClicked;
            this._view.DeleteClicked += (async (sender, args) =>
            {
                await DeleteClicked(sender, args);
            });
            this._view.AddToLibraryClicked += (async (sender, args) =>
            {
                WishlistItem selectedItem = this._view.SelectedItem;

                if (selectedItem.Type == ItemType.Book)
                {
                    using (var addBookDialog = new AddNewBookForm())
                    {
                        addBookDialog.TitleFieldText = selectedItem.Title;
                        addBookDialog.NotesFieldText = selectedItem.Notes;

                        var addBookPresenter = new AddBookPresenter(new BookService(),
                        new TagService(),
                        new AuthorService(),
                        new PublisherService(),
                        addBookDialog,
                        new ImageFileReader());
                        await addBookPresenter.PopulateTagsList();
                        await addBookPresenter.PopulateAuthorsList();
                        await addBookPresenter.PopulatePublishersList();

                        addBookDialog.ShowDialog();
                    }
                }
                else
                {
                    using (var addItemDialog = new AddNewMediaItemForm())
                    {
                        addItemDialog.TitleFieldText = selectedItem.Title;
                        addItemDialog.NotesFieldText = selectedItem.Notes;
                        addItemDialog.SelectedCategory = Item.GetTypeString(selectedItem.Type);

                        var addMediaItemPresenter = new AddMediaItemPresenter(new MediaItemService(), new TagService(),
                        new MediaItemFactory(),
                        addItemDialog,
                        new ImageFileReader(),
                        new NewTagOrPublisherInputBoxProvider());
                        await addMediaItemPresenter.PopulateTagsAsync();

                        addItemDialog.ShowDialog();
                    }
                }
            });
            this._view.SaveNewClicked += (async (sender, args) =>
            {
                await SaveNewClicked(sender, args);
            });
            this._view.NewItemFieldsUpdated += NewItemFieldsUpdated;
            this._view.ApplyFilters += ((sender, args) =>
            {
                ApplyFilters(sender, args);
            });
            this._view.WindowCreated += (async (sender, args) =>
            {
                await LoadData();
            });

            EventAggregator.GetInstance().Subscribe<WishlistUpdatedEvent>(async m => await LoadData());
        }

        public async Task LoadData()
        {
            this._view.StatusText = "Please Wait...";

            var service = this._serviceProvider.Get();
            var allItems = await service.GetAll();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Title");
            dt.Columns.Add("Type");
            dt.Columns.Add("Notes");
            foreach (var item in allItems)
            {
                dt.Rows.Add(item.Id,
                            item.Title,
                            Item.GetTypeString(item.Type),
                            item.Notes);
            }
            this._allItems = dt;

            ApplyFilters(null, null);

            this._view.StatusText = "Ready.";
        }

        private void EnableSelectedItemButtons(bool saveSelected, bool discardChanges, bool deleteSelected, bool addToLibrary)
        {
            this._view.SaveSelectedButtonEnabled = saveSelected;
            this._view.DiscardChangesButtonEnabled = discardChanges;
            this._view.DeleteSelectedButtonEnabled = deleteSelected;
            this._view.AddToLibraryButtonEnabled = addToLibrary;
        }

        private bool GetFilterSelected(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Book:
                    return this._view.BookFilterSelected;
                case ItemType.Cd:
                    return this._view.CdFilterSelected;
                case ItemType.Dvd:
                    return this._view.DvdFilterSelected;
                case ItemType.BluRay:
                    return this._view.BlurayFilterSelected;
                case ItemType.UhdBluRay:
                    return this._view.UhdBlurayFilterSelected;
                case ItemType.Vhs:
                    return this._view.VhsFilterSelected;
                case ItemType.Vinyl:
                    return this._view.VinylFilterSelected;
                case ItemType.Cassette:
                    return this._view.CassetteFilterSelected;
                case ItemType.FlashDrive:
                    return this._view.FlashDriveFilterSelected;
                case ItemType.FloppyDisk:
                    return this._view.FloppyDiskFilterSelected;
                default:
                    return this._view.OtherFilterSelected;
            }
        }

        #region view event handlers
        public void ApplyFilters(object sender, EventArgs args)
        {
            DataTable filteredTable = this._allItems.Copy();
            if (!string.IsNullOrWhiteSpace(this._view.TitleFilterText))
            {
                filteredTable = FilterByTitle(filteredTable);
            }
            filteredTable = FilterByType(filteredTable);
            this._view.DisplayedItems = filteredTable;

            this._view.StatusText = "Ready.";
        }

        private DataTable FilterByTitle(DataTable originalTable)
        {
            Regex filterPattern = new Regex(this._view.TitleFilterText, REGEX_OPTIONS);

            DataTable filteredTable = originalTable.Clone();
            var rows = originalTable.AsEnumerable()
                .Where(row => filterPattern.IsMatch(row.Field<string>("Title")));
            foreach (var row in rows)
            {
                filteredTable.ImportRow(row);
            }

            return filteredTable;
        }

        private DataTable FilterByType(DataTable originalTable)
        {
            DataTable filteredTable = originalTable.Clone();
            var rows = originalTable.AsEnumerable();
            foreach (var row in rows)
            {
                foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
                {
                    if (GetFilterSelected(itemType))
                    {
                        if (row.Field<string>("Type").Equals(Item.GetTypeString(itemType)))
                            filteredTable.ImportRow(row);
                    }
                }
            }     

            return filteredTable;
        }

        public void ItemSelected(object sender, EventArgs args)
        {
            if (this._view.SelectedItem is null)
            {
                this._view.SelectedNotes = "";

                EnableSelectedItemButtons(false, false, false, false);

                return;
            }
            else
            {
                this._view.SelectedNotes = this._view.SelectedItem.Notes;

                EnableSelectedItemButtons(true, true, true, true);
            }
        }

        public async Task SaveSelectedClicked(object sender, EventArgs args)
        {
            this._view.StatusText = "Please Wait...";

            var service = this._serviceProvider.Get();
            await service.Update(this._view.ModifiedItem, false);

            EventAggregator.GetInstance().Publish(new WishlistUpdatedEvent());

            this._view.StatusText = "Ready.";
        }

        public void DiscardChangesClicked(object sender, EventArgs args)
        {
            this._view.SelectedNotes = this._view.SelectedItem.Notes;
        }

        public async Task DeleteClicked(object sender, EventArgs args)
        {
            this._view.StatusText = "Please Wait...";

            var service = this._serviceProvider.Get();
            await service.DeleteById(this._view.SelectedItem.Id);

            EventAggregator.GetInstance().Publish(new WishlistUpdatedEvent());

            this._view.StatusText = "Ready.";
        }

        public async Task SaveNewClicked(object sender, EventArgs args)
        {
            this._view.StatusText = "Please Wait...";

            var service = this._serviceProvider.Get();

            string newItemTitle = this._view.NewItemTitle;
            bool preExisting = await service.ExistsWithTitle(newItemTitle);
            if (preExisting)
            {
                this._view.ShowItemAlreadyExistsDialog(newItemTitle);
                this._view.StatusText = "Ready.";

                return;
            }

            await service.Add(this._view.NewItem);

            EventAggregator.GetInstance().Publish(new WishlistUpdatedEvent());

            this._view.NewItemTitle = string.Empty;
            this._view.NewNotes = string.Empty;

            this._view.StatusText = "Ready.";
        }

        public void NewItemFieldsUpdated(object sender, EventArgs args)
        {
            if (string.IsNullOrWhiteSpace(this._view.NewItemTitle))
            {
                this._view.SaveNewButtonEnabled = false;
            }
            else
            {
                this._view.SaveNewButtonEnabled = true;
            }
        }
        #endregion

        public void Dispose()
        {
            EventAggregator.GetInstance().Unsubscribe<WishlistUpdatedEvent>(async m => await LoadData());
        }
    }//class
}
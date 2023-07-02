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
using MyLibrary.Models.Entities.Builders;

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
                    var addBookDialog = new AddNewBookForm();
                    var addBookPresenter = new AddBookPresenter(new BookService(),
                        new TagService(),
                        new AuthorService(),
                        new PublisherService(),
                        addBookDialog,
                        new ImageFileReader());
                    await addBookPresenter.PopulateTagsList();
                    await addBookPresenter.PopulateAuthorsList();
                    await addBookPresenter.PopulatePublishersList();
                    addBookDialog.TitleFieldText = selectedItem.Title;
                    addBookDialog.NotesFieldText = selectedItem.Notes;
                    addBookDialog.ShowAsDialog();
                }
                else
                {
                    var addMediaItemDialog = new AddNewMediaItemForm();
                    var addMediaItemPresenter = new AddMediaItemPresenter(new MediaItemService(),
                        new TagService(),
                        new MediaItemBuilder(),
                        addMediaItemDialog,
                        new ImageFileReader(),
                        new NewTagOrPublisherInputBoxProvider());
                    await addMediaItemPresenter.PopulateTagsAsync();
                    if (selectedItem.Type == ItemType.FlashDrive)
                    {
                        addMediaItemDialog.SelectedCategory = "Flash Drive";
                    }
                    else if (selectedItem.Type == ItemType.FloppyDisk)
                    {
                        addMediaItemDialog.SelectedCategory = "Floppy Disk";
                    }
                    else
                    {
                        addMediaItemDialog.SelectedCategory = (selectedItem.Type).ToString();
                    }
                    addMediaItemDialog.TitleFieldText = selectedItem.Title;
                    addMediaItemDialog.NotesFieldText = selectedItem.Notes;
                    addMediaItemDialog.ShowDialog();
                }
            });
            this._view.SaveNewClicked += (async (sender, args) =>
            {
                await SaveNewClicked(sender, args);
            });
            this._view.NewItemFieldsUpdated += NewItemFieldsUpdated;
            this._view.SelectedItemFieldsUpdated += SelectedItemFieldsUpdated;
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

        #region view event handlers
        public void ApplyFilters(object sender, EventArgs args)
        {
            string filterByTitle = this._view.TitleFilterText;
            bool cassetteChecked = this._view.CassetteFilterSelected;
            bool bookChecked = this._view.BookFilterSelected;
            bool cdChecked = this._view.CdFilterSelected;
            bool dvdChecked = this._view.DvdFilterSelected;
            bool blurayChecked = this._view.BlurayFilterSelected;
            bool uhdBlurayChecked = this._view.UhdBlurayFilterSelected;
            bool vhsChecked = this._view.VhsFilterSelected;
            bool vinylChecked = this._view.VinylFilterSelected;
            bool flashDriveChecked = this._view.FlashDriveFilterSelected;
            bool floppyDiskChecked = this._view.FloppyDiskFilterSelected;
            bool otherChecked = this._view.OtherFilterSelected;

            DataTable filteredTable = this._allItems.Copy();
            if (!string.IsNullOrWhiteSpace(filterByTitle))
            {
                filteredTable = FilterByTitle(filteredTable, filterByTitle);
            }

            filteredTable = FilterWishlistByType(filteredTable,
                cassetteChecked, bookChecked, cdChecked, dvdChecked, blurayChecked, uhdBlurayChecked, vhsChecked, vinylChecked, flashDriveChecked, floppyDiskChecked, otherChecked);

            this._view.DisplayedItems = filteredTable;

            this._view.StatusText = "Ready.";
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

        private DataTable FilterWishlistByType(DataTable originalTable,
            bool cassetteChecked, bool bookChecked, bool cdChecked, bool dvdChecked, bool blurayChecked, bool uhdBlurayChecked, bool vhsChecked, bool vinylChecked, bool flashDriveChecked, bool floppyDiskChecked, bool otherChecked)
        {
            DataTable filteredTable = originalTable.Clone();
            var rows = originalTable.AsEnumerable();
            foreach (var row in rows)
            {
                if (cassetteChecked)
                {
                    if (row.Field<string>("Type").Equals("Cassette"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (bookChecked)
                {
                    if (row.Field<string>("Type").Equals("Book"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (dvdChecked)
                {
                    if (row.Field<string>("Type").Equals("Dvd"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (cdChecked)
                {
                    if (row.Field<string>("Type").Equals("Cd"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (blurayChecked)
                {
                    if (row.Field<string>("Type").Equals("BluRay"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (uhdBlurayChecked)
                {
                    if (row.Field<string>("Type").Equals("4k BluRay"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (vhsChecked)
                {
                    if (row.Field<string>("Type").Equals("Vhs"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (vinylChecked)
                {
                    if (row.Field<string>("Type").Equals("Vinyl"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (floppyDiskChecked)
                {
                    if (row.Field<string>("Type").Equals("Floppy Disk"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (flashDriveChecked)
                {
                    if (row.Field<string>("Type").Equals("Flash Drive"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                if (otherChecked)
                {
                    if (row.Field<string>("Type").Equals("Other"))
                    {
                        filteredTable.ImportRow(row);
                    }
                }
            }

            return filteredTable;
        }//FilterWishlistByType

        public void ItemSelected(object sender, EventArgs args)
        {
            if (this._view.SelectedItem is null)
            {
                this._view.SelectedNotes = "";

                this._view.SaveSelectedButtonEnabled = false;
                this._view.DiscardChangesButtonEnabled = false;
                this._view.DeleteSelectedButtonEnabled = false;
                this._view.AddToLibraryButtonEnabled = false;

                return;
            }
            else
            {
                this._view.SelectedNotes = this._view.SelectedItem.Notes;

                this._view.SaveSelectedButtonEnabled = true;
                this._view.DiscardChangesButtonEnabled = true;
                this._view.DeleteSelectedButtonEnabled = true;
                this._view.AddToLibraryButtonEnabled = true;
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

        public void SelectedItemFieldsUpdated(object sender, EventArgs args)
        {

        }
        #endregion

        public void Dispose()
        {
            EventAggregator.GetInstance().Unsubscribe<WishlistUpdatedEvent>(async m => await LoadData());
        }
    }//class
}
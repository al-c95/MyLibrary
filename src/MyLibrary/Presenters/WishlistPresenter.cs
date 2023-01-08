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
using MyLibrary.Views;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Events;
using MyLibrary.Utils;

namespace MyLibrary.Presenters
{
    public class WishlistPresenter
    {
        private IWishlistForm _view;
        private IWishlistServiceProvider _serviceProvider;

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
                        addMediaItemDialog, 
                        new ImageFileReader(), 
                        new NewTagOrPublisherInputBoxProvider());
                    await addMediaItemPresenter.PopulateTagsList();
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

            EventAggregator.GetInstance().Subscribe<WishlistUpdatedEvent>(async m => await LoadData());
        }

        public async Task LoadData()
        {
            this._view.StatusText = "Please Wait...";

            var service = this._serviceProvider.Get();
            this._view.DisplayItems(await service.GetAll());

            this._view.StatusText = "Ready.";
        }

        #region view event handlers
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
    }//class
}

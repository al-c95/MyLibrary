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
using System.Threading.Tasks;
using MyLibrary.Views;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary.Presenters
{
    public class ManageCopiesPresenter
    {
        private IManageCopiesForm _view;
        private Item _item;
        private ICopyServiceFactory _serviceFactory;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="item"></param>
        /// <param name="serviceFactory"></param>
        public ManageCopiesPresenter(IManageCopiesForm view, 
            Item item, ICopyServiceFactory serviceFactory)
        {
            this._view = view;
            this._item = item;
            this._serviceFactory = serviceFactory;

            this._view.ItemTitleText = item.Title;

            this._view.SaveSelectedButtonEnabled = false;
            this._view.SaveNewButtonEnabled = false;
            this._view.DiscardChangesButtonEnabled = false;
            this._view.DeleteSelectedButtonEnabled = false;

            // register event handlers
            this._view.CopySelected += CopySelected;
            this._view.SaveSelectedClicked += (async (sender, args) => 
            { 
                await HandleSaveSelectedClicked(sender, args); 
            });
            this._view.DiscardChangesClicked += DiscardChangesClicked;
            this._view.DeleteClicked += (async (sender, args) => 
            { 
                await HandleDeleteClicked(sender, args); 
            });
            this._view.SaveNewClicked += (async (sender, args) => 
            { 
                await HandleSaveNewClicked(sender, args);
            });
            this._view.NewCopyFieldsUpdated += NewCopyFieldsUpdated;
            this._view.SelectedCopyFieldsUpdated += SelectedCopyFieldsUpdated;
        }

        public async Task LoadData(object sender, EventArgs args)
        {
            this._view.StatusText = "Please Wait...";

            if (this._item.GetType() == typeof(Book))
            {
                var copyService = this._serviceFactory.GetBookCopyService();
                this._view.DisplayCopies(await copyService.GetByItemId(this._item.Id));
            }
            else if (this._item.GetType() == typeof(MediaItem))
            {
                var copyService = this._serviceFactory.GetMediaItemCopyService();
                this._view.DisplayCopies(await copyService.GetByItemId(this._item.Id));
            }

            this._view.StatusText = "Ready.";
        }

        #region view event handlers
        public void CopySelected(object sender, EventArgs args)
        {
            if (this._view.SelectedCopy is null)
            {
                // nothing selected
                // clear selected copy fields
                this._view.SelectedDescription = "";
                this._view.SelectedNotes = "";
                // disable delete button
                this._view.DeleteSelectedButtonEnabled = false;

                return;
            }
            else
            {
                // copy selected
                // display selected copy description and notes
                this._view.SelectedDescription = this._view.SelectedCopy.Description;
                this._view.SelectedNotes = this._view.SelectedCopy.Notes;
                // enable delete button
                this._view.DeleteSelectedButtonEnabled = true;
            }
        }

        public async Task HandleSaveSelectedClicked(object sender, EventArgs args)
        {
            this._view.StatusText = "Please Wait...";

            if (this._item.GetType() == typeof(Book))
            {
                var copyService = this._serviceFactory.GetBookCopyService();

                BookCopy copy = (BookCopy)this._view.ModifiedSelectedCopy;
                copy.BookId = this._item.Id;
                await copyService.Update(copy);

                this._view.DisplayCopies(await copyService.GetByItemId(this._item.Id));
            }
            else if (this._item.GetType() == typeof(MediaItem))
            {
                var copyService = this._serviceFactory.GetMediaItemCopyService();

                MediaItemCopy copy = (MediaItemCopy)this._view.ModifiedSelectedCopy;
                copy.MediaItemId = this._item.Id;
                await copyService.Update(copy);

                this._view.DisplayCopies(await copyService.GetByItemId(this._item.Id));
            }

            this._view.StatusText = "Ready.";
        }

        public void DiscardChangesClicked(object sender, EventArgs args)
        {
            this._view.SelectedDescription = this._view.SelectedCopy.Description;
            this._view.SelectedNotes = this._view.SelectedCopy.Notes;
        }

        public async Task HandleDeleteClicked(object sender, EventArgs args)
        {
            this._view.StatusText = "Please Wait...";

            if (this._item.GetType() == typeof(Book))
            {
                var copyService = this._serviceFactory.GetBookCopyService();
                await copyService.DeleteById(this._view.SelectedCopy.Id);

                this._view.DisplayCopies(await copyService.GetByItemId(this._item.Id));
            }
            else if (this._item.GetType() == typeof(MediaItem))
            {
                var copyService = this._serviceFactory.GetMediaItemCopyService();
                await copyService.DeleteById(this._view.SelectedCopy.Id);

                this._view.DisplayCopies(await copyService.GetByItemId(this._item.Id));
            }

            this._view.StatusText = "Ready.";
        }

        public async Task HandleSaveNewClicked(object sender, EventArgs args)
        {
            this._view.StatusText = "Please Wait...";

            if (this._item.GetType() == typeof(Book))
            {
                var copyService = this._serviceFactory.GetBookCopyService();

                BookCopy copy = (BookCopy)this._view.NewCopy;
                await copyService.Create(copy);

                this._view.DisplayCopies(await copyService.GetByItemId(this._item.Id));
            }
            else if (this._item.GetType() == typeof(MediaItem))
            {
                var copyService = this._serviceFactory.GetMediaItemCopyService();

                MediaItemCopy copy = (MediaItemCopy)this._view.NewCopy;
                await copyService.Create(copy);

                this._view.DisplayCopies(await copyService.GetByItemId(this._item.Id));
            }

            this._view.NewDescription = "";
            this._view.NewNotes = "";

            this._view.StatusText = "Ready.";
        }

        public void NewCopyFieldsUpdated(object sender, EventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(this._view.NewDescription))
            {
                this._view.SaveNewButtonEnabled = true;
            }
            else
            {
                this._view.SaveNewButtonEnabled = false;
            }
        }

        public void SelectedCopyFieldsUpdated(object sender, EventArgs args)
        {
            if (this._view.NumberCopiesSelected != 0)
            {
                if (!string.IsNullOrWhiteSpace(this._view.SelectedDescription))
                {
                    this._view.SaveSelectedButtonEnabled = true;
                    this._view.DiscardChangesButtonEnabled = true;
                }
                else
                {
                    this._view.SaveSelectedButtonEnabled = false;
                    this._view.DiscardChangesButtonEnabled = false;
                }
            }
            else
            {
                this._view.SaveSelectedButtonEnabled = false;
                this._view.DiscardChangesButtonEnabled = false;
            }
        }
        #endregion
    }//class
}
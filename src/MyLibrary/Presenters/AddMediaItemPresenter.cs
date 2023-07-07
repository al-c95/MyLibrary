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
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Factories;
using MyLibrary.Views;
using MyLibrary.Utils;
using MyLibrary.Events;
using System.Linq;

namespace MyLibrary.Presenters
{
    /// <summary>
    /// Contains most of the logic of the add new media item dialog.
    /// </summary>
    public class AddMediaItemPresenter
    {
        private IMediaItemService _mediaItemService;
        private ITagService _tagService;
        private IMediaItemFactory _itemFactory;
        public MediaItem NewItem
        {
            get;
            set;
        }

        private IAddMediaItemForm _view;

        private IImageFileReader _imageFileReader;

        private INewTagOrPublisherInputBoxProvider _newTagDialogProvider;

        public Dictionary<string, bool> AllTags;

        /// <summary>
        /// </summary>
        /// <param name="mediaItemService"></param>
        /// <param name="tagService"></param>
        /// <param name="view">add media item window</param>
        /// <param name="imageFileReader"></param>
        public AddMediaItemPresenter(IMediaItemService mediaItemService, ITagService tagService, IMediaItemFactory newItemFactory,
            IAddMediaItemForm view,
            IImageFileReader imageFileReader,
            INewTagOrPublisherInputBoxProvider newTagDialogProvider)
        {
            this._mediaItemService = mediaItemService;
            this._tagService = tagService;
            this._itemFactory = newItemFactory;
            this.NewItem = new MediaItem();

            this._view = view;

            this._imageFileReader = imageFileReader;

            this.AllTags = new Dictionary<string, bool>();

            this._newTagDialogProvider = newTagDialogProvider;

            // subscribe to the view's events
            this._view.SaveButtonClicked += (async (sender, args) =>
            {
                await HandleSaveButtonClicked(sender, args);
            });
            this._view.InputFieldsUpdated += InputFieldsUpdated;
            this._view.FilterTagsFieldUpdated += FilterTags;
            this._view.AddNewTagButtonClicked += ((sender, args) => 
            {
                HandleAddNewTagClicked(sender,args);
                InputFieldsUpdated(sender, args);
            }); 
            this._view.TagCheckedChanged += ((sender, args) => 
            {
                HandleTagCheckedChanged(sender, args);
                InputFieldsUpdated(sender, args);
            });            
        }//ctor

        public async Task PopulateTagsAsync()
        {
            var allTags = await this._tagService.GetAll();
            foreach (var tag in allTags)
            {
                this.AllTags.Add(tag.Name, false);
            }

            FilterTags(null, null);
        }//PopulateTagsList

        public void FilterTags(object sender, EventArgs args)
        {
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(this._view.FilterTagsFieldEntry, REGEX_OPTIONS);

            Dictionary<string, bool> filteredTags = new Dictionary<string, bool>();
            foreach (var kvp in this.AllTags)
            {
                if (filterPattern.IsMatch(kvp.Key))
                {
                    filteredTags.Add(kvp.Key, kvp.Value);
                }
            }

            this._view.AddTags(filteredTags);
        }//FilterTags

        private IEnumerable<string> GetSelectedTags()
        {
            foreach (var kvp in this.AllTags)
            {
                if (this.AllTags[kvp.Key])
                {
                    yield return kvp.Key;
                }
            }
        }

        private void EnableButtons(bool save, bool cancel)
        {
            this._view.SaveButtonEnabled = save;
            this._view.CancelButtonEnabled = cancel;
        }

        private void HandleImageFile()
        {
            try
            {
                this._imageFileReader.Path = this._view.ImageFilePathFieldText;
                byte[] imageBytes = this._imageFileReader.ReadBytes();
                this.NewItem.Image = imageBytes;
            }
            catch (IOException ex)
            {
                this._view.ShowErrorDialog("Image file error", ex.Message);
                EnableButtons(true, true);

                return;
            }
        }

        #region View event handlers
        public void HandleTagCheckedChanged(object sender, EventArgs args)
        {
            foreach (var selectedTag in this._view.SelectedTags)
            {
                this.AllTags[selectedTag] = true;
            }

            foreach (var unselectedTag in this._view.UnselectedTags)
            {
                this.AllTags[unselectedTag] = false;
            }
        }

        public async Task HandleSaveButtonClicked(object sender, EventArgs e)
        {
            EnableButtons(false, false);

            if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
            {
                HandleImageFile();
            }

            try
            {
                bool added = await this._mediaItemService.AddIfNotExistsAsync(this.NewItem);
                if (!added)
                {
                    this._view.ShowItemAlreadyExistsDialog(this.NewItem.Title);
                    EnableButtons(true, true);

                    return;
                }
                else
                {
                    EventAggregator.GetInstance().Publish(new MediaItemsUpdatedEvent());
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error creating item", ex.Message);
                EnableButtons(true, true);

                return;
            }

            this._view.CloseDialog();
        }//SaveButtonClicked

        public void InputFieldsUpdated(object sender, EventArgs e)
        {
            try
            {
                var selectedTags = GetSelectedTags().ToList();
                this.NewItem = this._itemFactory.Create(this._view.TitleFieldText, this._view.NumberFieldText, this._view.YearFieldEntry, this._view.RunningTimeFieldEntry,
                    selectedTags);
                this.NewItem.Type = Item.ParseType(this._view.SelectedCategory);
                this.NewItem.Notes = this._view.NotesFieldText;

                this._view.SaveButtonEnabled = true;

                if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
                {
                    this._view.SaveButtonEnabled = ImageFileReader.ValidateFilePath(this._view.ImageFilePathFieldText);
                }
            }
            catch (InvalidOperationException)
            {
                this._view.SaveButtonEnabled = false;
            }
        }//InputFieldsUpdated

        public void HandleAddNewTagClicked(object sender, EventArgs args)
        {
            string newTag = ShowNewTagDialog();
            if (!string.IsNullOrWhiteSpace(newTag))
            {
                if (!this.AllTags.ContainsKey(newTag))
                {
                    this.AllTags.Add(newTag, true);

                    FilterTags(null, null);
                }
                else
                {
                    this._view.ShowTagAlreadyExistsDialog(newTag);
                }
            }
            else
            {
                return;
            }
        }//HandleAddNewTagClicked
        #endregion

        private string ShowNewTagDialog()
        {
            var dialog = this._newTagDialogProvider.Get();
            var presenter = new NewTagOrPublisherInputPresenter(dialog, NewTagOrPublisherInputPresenter.InputBoxMode.Tag);

            return dialog.ShowAsDialog();
        }
    }//class
}
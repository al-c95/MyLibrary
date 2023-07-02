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
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Views;
using MyLibrary.Utils;
using MyLibrary.Events;

namespace MyLibrary.Presenters
{
    /// <summary>
    /// Contains most of the logic of the add new media item dialog.
    /// </summary>
    public class AddMediaItemPresenter
    {
        private IMediaItemService _mediaItemService;
        private ITagService _tagService;
        private IMediaItemBuilder _newItemBuilder;
        private MediaItem _newItem;

        private IAddMediaItemForm _view;

        private IImageFileReader _imageFileReader;

        private INewTagOrPublisherInputBoxProvider _newTagDialogProvider;

        protected Dictionary<string, bool> _allTags;

        /// <summary>
        /// </summary>
        /// <param name="mediaItemService"></param>
        /// <param name="tagService"></param>
        /// <param name="view">add media item window</param>
        /// <param name="imageFileReader"></param>
        public AddMediaItemPresenter(IMediaItemService mediaItemService, ITagService tagService, IMediaItemBuilder newItemBuilder,
            IAddMediaItemForm view,
            IImageFileReader imageFileReader,
            INewTagOrPublisherInputBoxProvider newTagDialogProvider)
        {
            this._mediaItemService = mediaItemService;
            this._tagService = tagService;
            this._newItemBuilder = newItemBuilder;
            this._newItem = new MediaItem();

            this._view = view;

            this._imageFileReader = imageFileReader;

            this._allTags = new Dictionary<string, bool>();

            this._newTagDialogProvider = newTagDialogProvider;

            // subscribe to the view's events
            this._view.SaveButtonClicked += (async (sender, args) =>
            {
                await HandleSaveButtonClicked(sender, args);
            });
            this._view.InputFieldsUpdated += InputFieldsUpdated;
            this._view.FilterTagsFieldUpdated += FilterTags;
            this._view.AddNewTagButtonClicked += HandleAddNewTagClicked;
            this._view.TagCheckedChanged += HandleTagCheckedChanged;
        }//ctor

        public async Task PopulateTagsAsync()
        {
            var allTags = await this._tagService.GetAll();
            foreach (var tag in allTags)
            {
                this._allTags.Add(tag.Name, false);
            }

            FilterTags(null, null);
        }//PopulateTagsList

        public void FilterTags(object sender, EventArgs args)
        {
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(this._view.FilterTagsFieldEntry, REGEX_OPTIONS);

            Dictionary<string, bool> filteredTags = new Dictionary<string, bool>();
            foreach (var kvp in this._allTags)
            {
                if (filterPattern.IsMatch(kvp.Key))
                {
                    filteredTags.Add(kvp.Key, kvp.Value);
                }
            }

            this._view.AddTags(filteredTags);
        }//FilterTags

        private void AddSelectedTags()
        {
            foreach (var kvp in this._allTags)
            {
                if (this._allTags[kvp.Key])
                {
                    this._newItem.Tags.Add(new Tag
                    {
                        Name = kvp.Key
                    });
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
                this._newItem.Image = imageBytes;
            }
            catch (IOException ex)
            {
                // error reading the image
                // alert the user
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
                this._allTags[selectedTag] = true;
            }

            foreach (var unselectedTag in this._view.UnselectedTags)
            {
                this._allTags[unselectedTag] = false;
            }
        }

        public async Task HandleSaveButtonClicked(object sender, EventArgs e)
        {
            EnableButtons(false, false);

            AddSelectedTags();

            if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
            {
                HandleImageFile();
            }

            try
            {
                bool added = await this._mediaItemService.AddIfNotExistsAsync(this._newItem);
                if (!added)
                {
                    this._view.ShowItemAlreadyExistsDialog(this._view.TitleFieldText);

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
                this._newItem = this._newItemBuilder
                    .WithTitle(this._view.TitleFieldText)
                    .WithNumber(this._view.NumberFieldText)
                    .WithYear(this._view.YearFieldEntry)
                    .WithRunningTime(this._view.RunningTimeFieldEntry)
                        .Build();
                this._newItem.Type = Item.ParseType(this._view.SelectedCategory);

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
                if (!this._allTags.ContainsKey(newTag))
                {
                    this._allTags.Add(newTag, true);

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
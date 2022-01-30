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
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Views;
using MyLibrary.Utils;

namespace MyLibrary.Presenters
{
    public class AddMediaItemPresenter
    {
        private IMediaItemService _mediaItemService;
        private ITagService _tagService;

        private IAddMediaItemForm _view;

        private IImageFileReader _imageFileReader;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="mediaItemService"></param>
        /// <param name="tagService"></param>
        /// <param name="view"></param>
        /// <param name="imageFileReader"></param>
        public AddMediaItemPresenter(IMediaItemService mediaItemService, ITagService tagService, 
            IAddMediaItemForm view,
            IImageFileReader imageFileReader)
        {
            this._mediaItemService = mediaItemService;
            this._tagService = tagService;

            this._view = view;

            this._imageFileReader = imageFileReader;

            // subscribe to the view's events
            this._view.SaveButtonClicked += (async (sender, args) => 
            {
                await HandleSaveButtonClicked(sender, args);
            });
            this._view.InputFieldsUpdated += InputFieldsUpdated;
            this._view.NewTagFieldUpdated += NewTagFieldUpdated;
        }

        public async Task PopulateTagsList()
        {
            var allTags = await this._tagService.GetAll();
            List<string> tagNames = new List<string>();
            foreach (var tag in allTags)
                tagNames.Add(tag.Name);

            this._view.PopulateTagsList(tagNames);
        }

        #region View event handlers
        public async Task HandleSaveButtonClicked(object sender, EventArgs e)
        {
            // disable save and cancel buttons
            this._view.SaveButtonEnabled = false;
            this._view.CancelButtonEnabled = false;

            // check if item with title already exists
            try
            {
                if (await this._mediaItemService.ExistsWithTitle(this._view.TitleFieldText))
                {
                    // tell the user
                    this._view.ShowItemAlreadyExistsDialog(this._view.TitleFieldText);

                    // re-enable save and cancel buttons
                    this._view.SaveButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;

                    // nothing more to do
                    return;
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error checking title.", ex.Message);

                // re-enable save and cancel buttons
                this._view.SaveButtonEnabled = true;
                this._view.CancelButtonEnabled = true;

                return;
            }

            // create item
            string selectedCategory = this._view.SelectedCategory;
            string enteredRunningTime = this._view.RunningTimeFieldEntry;
            MediaItem item = new MediaItem
            {
                Title = this._view.TitleFieldText,
                Type = (ItemType)Enum.Parse(typeof(ItemType), selectedCategory),
                Number = long.Parse(this._view.NumberFieldText),
                ReleaseYear = int.Parse(this._view.YearFieldEntry),
                Notes = this._view.NotesFieldText
            };
            if (!string.IsNullOrWhiteSpace(enteredRunningTime))
            {
                item.RunningTime = int.Parse(enteredRunningTime);
            }
            foreach (var tagName in this._view.SelectedTags)
                item.Tags.Add(new Tag { Name = tagName });
            if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
            {
                try
                {
                    this._imageFileReader.Path = this._view.ImageFilePathFieldText;
                    byte[] imageBytes = this._imageFileReader.ReadBytes();
                    item.Image = imageBytes;
                }
                catch (System.IO.IOException ex)
                {
                    // I/O error
                    // alert the user
                    this._view.ShowErrorDialog("Image file error", ex.Message);

                    // re-enable save and cancel buttons
                    this._view.SaveButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;

                    return;
                }
            }
            
            // add new item
            try
            {
                await this._mediaItemService.Add(item);
                this._view.ItemAddedFinished();
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error creating item", ex.Message);

                // re-enable save and cancel buttons
                this._view.SaveButtonEnabled = true;
                this._view.CancelButtonEnabled = true;

                return;
            }

            this._view.CloseDialog();
        }//SaveButtonClicked

        public void InputFieldsUpdated(object sender, EventArgs e)
        {
            bool sane = true;
            // title field mandatory
            sane = sane && !string.IsNullOrWhiteSpace(this._view.TitleFieldText);
            // number field mandatory, must be an integer
            if (!string.IsNullOrWhiteSpace(this._view.NumberFieldText))
            {
                long number;
                sane = sane && long.TryParse(this._view.NumberFieldText, out number);
            }
            else
            {
                sane = false;
            }
            // running time field must be either blank or an integer
            if (!string.IsNullOrWhiteSpace(this._view.RunningTimeFieldEntry))
            {
                int runningTime;
                sane = sane && int.TryParse(this._view.RunningTimeFieldEntry, out runningTime);
            }
            // year field mandatory, must be an integer
            if (!string.IsNullOrWhiteSpace(this._view.YearFieldEntry))
            {
                int year;
                sane = sane && int.TryParse(this._view.YearFieldEntry, out year);
            }
            else
            {
                sane = false;
            }
            // image file path field must be either blank or a valid file path with supported extension
            string imageFilePath = this._view.ImageFilePathFieldText;
            sane = sane && (Item.IsValidImageFileType(imageFilePath) || string.IsNullOrWhiteSpace(imageFilePath));

            this._view.SaveButtonEnabled = sane;
        }

        public void NewTagFieldUpdated(object sender, EventArgs args)
        {
            this._view.AddNewTagButtonEnabled = !string.IsNullOrWhiteSpace(this._view.NewTagFieldText);
        }
        #endregion
    }//class
}
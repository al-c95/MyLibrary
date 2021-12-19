﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class AddMediaItemPresenter
    {
        private MediaItemRepository _mediaItemRepo;
        private TagRepository _tagRepo;

        private IAddMediaItemForm _view;

        // ctor
        public AddMediaItemPresenter(MediaItemRepository mediaItemRepository, TagRepository tagRepository, 
            IAddMediaItemForm view)
        {
            this._mediaItemRepo = mediaItemRepository;
            this._tagRepo = tagRepository;

            this._view = view;

            // subscribe to the view's events
            this._view.SaveButtonClicked += SaveButtonClicked;
            this._view.InputFieldsUpdated += InputFieldsUpdated;
        }

        public async Task PopulateTagsList()
        {
            var allTags = await this._tagRepo.GetAll();
            List<string> tagNames = new List<string>();
            foreach (var tag in allTags)
                tagNames.Add(tag.Name);

            this._view.PopulateTagsList(tagNames);
        }

        #region View event handlers
        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            // check if item with title already exists
            try
            {
                if (await this._mediaItemRepo.ExistsWithTitle(this._view.TitleFieldText))
                {
                    // tell the user
                    this._view.ShowItemAlreadyExistsDialog(this._view.TitleFieldText);

                    // nothing more to do
                    return;
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error checking title.", ex.Message);
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
                    byte[] imageBytes = System.IO.File.ReadAllBytes(this._view.ImageFilePathFieldText);
                    item.Image = imageBytes;
                }
                catch (System.IO.IOException ex)
                {
                    // I/O error
                    // alert the user
                    this._view.ShowErrorDialog("Image file error", ex.Message);
                    return;
                }
            }
            
            // add new item
            try
            {
                await this._mediaItemRepo.Create(item);
                this._view.ItemAddedFinished();
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error creating item", ex.Message);
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
        #endregion
    }//class
}
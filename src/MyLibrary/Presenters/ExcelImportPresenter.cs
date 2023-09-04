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
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using MyLibrary.Views;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Events;
using MyLibrary.Import;

namespace MyLibrary.Presenters
{
    public class ExcelImportPresenter
    {
        private IExcelImportDialog _view;
        private IBookExcelReader _bookExcelReader;
        private IMediaItemExcelReader _mediaItemExcelReader;

        public int importedCount;
        public int skippedCount;
        public int updatedCount;

        public ExcelImportPresenter(IExcelImportDialog view)
        {
            this._view = view;

            this._view.Label1Text = "Ready.";
            this._view.Label2Text = "";

            this._view.BookChecked = true;

            this._view.BrowseButtonEnabled = true;
            this._view.CloseButtonEnabled = true;
            this._view.StartButtonEnabled = false;

            // subscribe to view events
            this._view.BrowseButtonClicked += HandleBrowseButtonClicked;
            this._view.StartButtonClicked += (async (sender, args) => 
            {
                await HandleStartButtonClicked(sender, args);
            });
            this._view.FileFieldTextChanged += HandleFileFieldTextChanged;
        }

        public void HandleFileFieldTextChanged(object sender, EventArgs e)
        {
            ValidateFilePath();
        }

        private void ProgressCallback(int parsed, int skipped)
        {
            skippedCount = skipped;
        }

        public async Task HandleStartButtonClicked(object sender, EventArgs args)
        {
            this._view.BrowseButtonEnabled = false;
            this._view.CloseButtonEnabled = false;
            this._view.FileFieldEnabled = false;
            this._view.StartButtonEnabled = false;
            this._view.RadioButtonsEnabled = false;
            this._view.Label1Text = "Reading worksheet...";
            this._view.Label2Text = "";

            FileInfo file = new FileInfo(this._view.FileFieldText);
            ExcelPackage excel = new ExcelPackage(file);
            List<Book> parsedBooks = new List<Book>();
            List<MediaItem> parsedMediaItems = new List<MediaItem>();

            if (this._view.BookChecked && !this._view.MediaItemChecked)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        this._bookExcelReader = new BookExcelReader(excel, "Book", Configuration.APP_VERSION);
                        foreach (var book in this._bookExcelReader.Read(ProgressCallback))
                        {
                            parsedBooks.Add(book);
                        }
                    });
                }
                catch (FormatException e)
                {
                    this._view.ShowErrorDialog(e);

                    this._view.Label1Text = "Task aborted.";
                    this._view.Label2Text = "";
                    this._view.CloseButtonEnabled = true;
                    this._view.BrowseButtonEnabled = true;
                    this._view.FileFieldEnabled = true;
                    this._view.RadioButtonsEnabled = true;

                    return;
                }

                this._view.Label1Text = "Updating database...";
                BookService service = new BookService();
                foreach (var book in parsedBooks)
                {
                    if (await service.AddIfNotExistsAsync(book))
                    {
                        importedCount++;
                    }
                    else
                    {
                        int id = await service.GetIdByTitleAsync(book.Title);

                        book.Id = id;
                        await service.UpdateAsync(book, false);

                        IEnumerable<Tag> currentTags = (await service.GetByIdAsync(id)).Tags;
                        List<string> currentTagNames = new List<string>();
                        foreach (Tag tag in currentTags)
                        {
                            currentTagNames.Add(tag.Name);
                        }
                        List<string> updatedTagNames = new List<string>();
                        foreach (Tag tag in book.Tags)
                        {
                            updatedTagNames.Add(tag.Name);
                        }
                        ItemTagsDto updateTags = new ItemTagsDto(id, currentTagNames, updatedTagNames);
                        await service.UpdateTagsAsync(updateTags);

                        updatedCount++;
                    }
                }

                EventAggregator.GetInstance().Publish(new BooksUpdatedEvent());
            }
            else if (this._view.MediaItemChecked && !this._view.BookChecked)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        this._mediaItemExcelReader = new MediaItemExcelReader(excel, "Media item", Configuration.APP_VERSION);
                        foreach (var mediaItem in this._mediaItemExcelReader.Read(ProgressCallback))
                        {
                            parsedMediaItems.Add(mediaItem);
                        }
                    });
                }
                catch (FormatException e)
                {
                    this._view.ShowErrorDialog(e);

                    this._view.Label1Text = "Task aborted.";
                    this._view.Label2Text = "";
                    this._view.CloseButtonEnabled = true;
                    this._view.BrowseButtonEnabled = true;
                    this._view.FileFieldEnabled = true;
                    this._view.RadioButtonsEnabled = true;

                    return;
                }

                this._view.Label1Text = "Updating database...";
                MediaItemService service = new MediaItemService();
                foreach (var item in parsedMediaItems)
                {
                    if (await service.AddIfNotExistsAsync(item))
                    {
                        importedCount++;
                    }
                    else
                    {
                        int id = await service.GetIdByTitleAsync(item.Title);

                        item.Id = id;
                        await service.UpdateAsync(item, false);

                        IEnumerable<Tag> currentTags = (await service.GetByIdAsync(id)).Tags;
                        List<string> currentTagNames = new List<string>();
                        foreach (Tag tag in currentTags)
                        {
                            currentTagNames.Add(tag.Name);
                        }
                        List<string> updatedTagNames = new List<string>();
                        foreach (Tag tag in item.Tags)
                        {
                            updatedTagNames.Add(tag.Name);
                        }
                        ItemTagsDto updateTags = new ItemTagsDto(id, currentTagNames, updatedTagNames);
                        await service.UpdateTagsAsync(updateTags);

                        updatedCount++;
                    }
                }

                EventAggregator.GetInstance().Publish(new MediaItemsUpdatedEvent());
            }//if

            this._view.Label1Text = "Task complete.";
            this._view.Label2Text = $"{importedCount} imported. {updatedCount} updated. {skippedCount} skipped.";
            this._view.CloseButtonEnabled = true;
        }//HandleStartButtonClicked

        public void HandleBrowseButtonClicked(object sender, EventArgs e)
        {
            string filePath = this._view.ShowFileBrowserDialog();
            this._view.FileFieldText = filePath;

            ValidateFilePath();
        }

        private void ValidateFilePath()
        {
            if (Path.GetExtension(this._view.FileFieldText).Equals(".xlsx"))
            {
                this._view.StartButtonEnabled = true;
            }
            else
            {
                this._view.StartButtonEnabled = false;
            }
        }
    }//class
}
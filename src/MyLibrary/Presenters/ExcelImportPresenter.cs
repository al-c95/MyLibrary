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
using MyLibrary.Models.BusinessLogic.ImportExcel;
using MyLibrary.Events;

namespace MyLibrary.Presenters
{
    // https://stackoverflow.com/questions/51199319/why-is-iprogresst-reportt-method-blocking-the-ui-thread
    public class ExcelImportPresenter
    {
        private IExcelImportDialog _view;

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

        public async Task HandleStartButtonClicked(object sender, EventArgs args)
        {
            this._view.BrowseButtonEnabled = false;
            this._view.CloseButtonEnabled = false;
            this._view.FileFieldEnabled = false;
            this._view.StartButtonEnabled = false;
            this._view.RadioButtonsEnabled = false;
            this._view.Label1Text = "Reading worksheet...";
            this._view.Label2Text = "";

            int importCount = 0;
            int updateCount = 0;
            int errorCount = 0;
            int warningCount = 0;

            Progress<ExcelRowResult> parseProgress = new Progress<ExcelRowResult>(result =>
            {
                if (result.Status == ExcelRowResultStatus.Success)
                {
                    this._view.AddSuccess(result.Message + " " + result.Item.Title);    
                }
                else if (result.Status == ExcelRowResultStatus.Warning)
                {
                    this._view.AddWarning(result.Message);
                    warningCount++;
                }
                else if (result.Status == ExcelRowResultStatus.Error)
                {
                    this._view.AddError(result.Message);
                    errorCount++;
                }
            });

            FileInfo file = new FileInfo(this._view.FileFieldText);
            ExcelPackage excel = new ExcelPackage(file);
            List<ExcelRowResult> parseResults = new List<ExcelRowResult>();

            if (this._view.BookChecked && !this._view.MediaItemChecked)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        BookExcelParser excelParser = new BookExcelParser(excel, Configuration.APP_VERSION);

                        foreach (var result in excelParser.Run())
                        {
                            if (result.Status == ExcelRowResultStatus.Success)
                            {
                                parseResults.Add(result);
                            }
                            else
                            {
                                ((IProgress<ExcelRowResult>)parseProgress).Report(result);
                            }
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
                foreach (var result in parseResults)
                {
                    Book book = (Book)result.Item;
                    if (await service.AddIfNotExistsAsync(book))
                    {
                        importCount++;
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
                        foreach (Tag tag in result.Item.Tags)
                        {
                            updatedTagNames.Add(tag.Name);
                        }
                        ItemTagsDto updateTags = new ItemTagsDto(id, currentTagNames, updatedTagNames);
                        await service.UpdateTagsAsync(updateTags);

                        updateCount++;
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
                        MediaItemExcelParser excelParser = new MediaItemExcelParser(excel, Configuration.APP_VERSION);

                        foreach (var result in excelParser.Run())
                        {
                            if (result.Status == ExcelRowResultStatus.Success)
                            {
                                parseResults.Add(result);
                            }
                            else
                            {
                                ((IProgress<ExcelRowResult>)parseProgress).Report(result);
                            }
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
                foreach (var result in parseResults)
                {
                    MediaItem item = (MediaItem)result.Item;
                    if (await service.AddIfNotExistsAsync(item))
                    {
                        importCount++;
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
                        foreach (Tag tag in result.Item.Tags)
                        {
                            updatedTagNames.Add(tag.Name);
                        }
                        ItemTagsDto updateTags = new ItemTagsDto(id,currentTagNames,updatedTagNames);
                        await service.UpdateTagsAsync(updateTags);

                        updateCount++;
                    }
                }

                EventAggregator.GetInstance().Publish(new MediaItemsUpdatedEvent());
            }//if

            this._view.Label1Text = "Task complete.";
            this._view.Label2Text = errorCount + " errors. " + warningCount + " warnings. " + importCount + " imported, " + updateCount + " updated.";
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
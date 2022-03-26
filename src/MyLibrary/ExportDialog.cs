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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views.Excel;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ExportDialog : Form
    {
        private string _type;

        public ExportDialog(string type)
        {
            InitializeComponent();

            this.CenterToParent();

            this.Text = "Export " + type + "s";
            this._type = type;

            this.label1.Text = "";
            this.label2.Text = "";

            this.startButton.Enabled = false;

            // register event handlers
            this.browseButton.Click += ((sender, args) =>
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.pathField.Text = dialog.SelectedPath + @"\" + type + "s_export.xlsx";

                    this.startButton.Enabled = true;
                }
                else
                {
                    return;
                }
            });
            this.startButton.Click += (async (sender, args) =>
            {
                var numberExported = new Progress<int>(p =>
                {
                    this.label2.Text = p + " rows exported";
                });
                await Process(numberExported);
            });
            this.cancelButton.Click += ((sender, args) =>
            {
                this.Close();
            });
        }

        public async Task Process(IProgress<int> numberExported)
        {
            string path = this.pathField.Text;

            this.label1.Text = "Exporting...";
            this.startButton.Enabled = false;
            this.browseButton.Enabled = false;
            this.pathField.Enabled = false;

            ExcelFile file = new ExcelFile(path);

            // do the work
            // TODO: refactor
            int count = 0;
            if (this._type == "tag")
            {
                TagsExcel excel = new TagsExcel(file);
                TagService service = new TagService();

                var allTags = await service.GetAll();

                await Task.Run(() =>
                {
                    foreach (var tag in allTags)
                    {
                        excel.WriteEntity(tag);
                        if (numberExported != null)
                            numberExported.Report(++count);
                    }
                });
               
                await excel.SaveAsync();
            }
            else if (this._type == "author")
            {
                AuthorsExcel excel = new AuthorsExcel(file);
                AuthorService service = new AuthorService();

                var allAuthors = await service.GetAll();

                await Task.Run(() =>
                {
                    foreach (var author in allAuthors)
                    {
                        excel.WriteEntity(author);
                        if (numberExported != null)
                            numberExported.Report(++count);
                    }
                });

                await excel.SaveAsync();
            }
            else if (this._type == "book")
            {
                BooksExcel excel = new BooksExcel(file);
                BookService service = new BookService();

                var allBooks = await service.GetAll();

                await Task.Run(() =>
                {
                    foreach (var book in allBooks)
                    {
                        excel.WriteEntity(book);
                        Task.Delay(1000);
                        if (numberExported != null)
                            numberExported.Report(++count);
                    }
                });

                await excel.SaveAsync();
            }
            else if (this._type == "media item")
            {
                MediaItemsExcel excel = new MediaItemsExcel(file);
                MediaItemService service = new MediaItemService();

                var allItems = await service.GetAll();

                await Task.Run(() =>
                {
                    foreach (var item in allItems)
                    {
                        excel.WriteEntity(item);
                        if (numberExported != null)
                            numberExported.Report(++count);
                    }
                });

                await excel.SaveAsync();
            }
            else if (this._type == "publisher")
            {
                PublishersExcel excel = new PublishersExcel(file);
                PublisherService service = new PublisherService();

                var allPublishers = await service.GetAll();

                await Task.Run(() =>
                {
                    foreach (var publisher in allPublishers)
                    {
                        excel.WriteEntity(publisher);
                        if (numberExported != null)
                            numberExported.Report(++count);
                    }
                });

                await excel.SaveAsync();
            }
            else
            {
                throw new Exception("Unknown export type: " + this._type);
            }

            // finished
            this.label1.Text = "Task complete.";
        }
    }
}

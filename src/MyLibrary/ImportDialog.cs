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
using System.Windows.Forms;
using MyLibrary;
using MyLibrary.Import;
using MyLibrary.Models.Entities;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ImportDialog : Form
    {
        private string _type;

        public int importedCount;
        public int skippedCount;

        private void ProgressCallback(int parsed, int skipped)
        {
            skippedCount = skipped;
        }

        public ImportDialog(string type)
        {
            InitializeComponent();

            this.CenterToParent();

            this.Text = "Import " + type + "s";
            this._type = type;

            this.label1.Text = "";
            this.label2.Text = "";

            // prepare list column
            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Item";
            columnHeader1.Width = 435;
            this.listView.Columns.AddRange(new ColumnHeader[] { columnHeader1 });

            // register event handlers
            this.startButton.Click += (async (sender, args) =>
            {
                string filePath = null;
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "CSV files (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;

                    this.startButton.Enabled = false;
                    this.closeButton.Enabled = false;

                    this.listView.Items.Clear();

                    dialog.Dispose();
                }
                else
                {
                    dialog.Dispose();
                    return;
                }

                try
                {
                    CsvFile csv = new CsvFile(filePath);
                    if (_type == "tag")
                    {
                        TagService tagService = new TagService();
                        TagCsvReader reader = new TagCsvReader(csv, Configuration.APP_VERSION);
                        List<Tag> parsedTags = new List<Tag>();
                        await Task.Run(() =>
                        {
                            var tags = reader.Read(ProgressCallback);
                            foreach (var tag in tags)
                            {
                               parsedTags.Add(tag);
                            }
                        });

                        foreach (var tag in parsedTags)
                        {
                            bool added = await tagService.AddIfNotExists(tag);
                            if (added)
                            {
                                importedCount++;
                            }
                            else
                            {
                                skippedCount++;
                            }
                        }
                    }
                    else if ( _type == "author")
                    {
                        AuthorService authorService = new AuthorService();
                        AuthorCsvReader reader = new AuthorCsvReader(csv, Configuration.APP_VERSION);
                        List<Author> parsedAuthors = new List<Author>();
                        await Task.Run(() =>
                        {
                            var authors = reader.Read(ProgressCallback);
                            foreach (var author in authors)
                            {
                                parsedAuthors.Add(author);
                            }
                        });

                        foreach (var author in parsedAuthors)
                        {
                            bool added = await authorService.ExistsWithName(author.FirstName, author.LastName);
                            if (added)
                            {
                                importedCount++;
                            }
                            else
                            {
                                skippedCount++;
                            }
                        }
                    }
                    else if (_type == "publisher")
                    {
                        PublisherService publisherService = new PublisherService();
                        PublisherCsvReader reader = new PublisherCsvReader(csv, Configuration.APP_VERSION);
                        List<Publisher> parsedPublishers = new List<Publisher>();
                        await Task.Run(() =>
                        {
                            var publishers = reader.Read(ProgressCallback);
                            foreach (var publisher in publishers)
                            {
                                parsedPublishers.Add(publisher);
                            }
                        });

                        foreach (var publisher in parsedPublishers)
                        {
                            bool added = await publisherService.AddIfNotExists(publisher);
                            if (added)
                            {
                                importedCount++;
                            }
                            else
                            {
                                skippedCount++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.label1.Text = "Task aborted.";
                    this.label2.Text = "";

                    this.closeButton.Enabled = true;
                }
                finally
                {
                    this.label1.Text = "Task complete.";
                    this.label2.Text = $"{importedCount} imported. {skippedCount} skipped.";

                    this.startButton.Enabled = false;
                    this.closeButton.Enabled = true;
                }
            });
            this.closeButton.Click += ((sender, args) =>
            {
                this.Close();
            });
        }

        private void AddListViewRow(string message)
        {
            string[] row = { message };
            ListViewItem listItem = new ListViewItem(row);
            this.listView.Items.Add(listItem);
        }
    }//class
}
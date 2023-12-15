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

using MyLibrary.Import;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            importedCount = 0;
            skippedCount = 0;

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

                    dialog.Dispose();
                }
                else
                {
                    dialog.Dispose();
                    return;
                }

                try
                {
                    if (this._type == "tag")
                    {
                        // parse CSV file
                        TagCsvImportCollection tags = new TagCsvImportCollection(new CsvParserService());
                        await Task.Run(() => { tags.LoadFromFile(filePath, ProgressCallback); });

                        // import data
                        TagService tagService = new TagService();
                        foreach (var tag in tags.GetAll())
                        {
                            if (await tagService.AddIfNotExists(tag))
                            {
                                importedCount++;
                            }
                            else
                            {
                                skippedCount++;
                            }
                        }
                    }
                    else if (this._type == "publisher")
                    {
                        // parse CSV file
                        PublisherCsvImportCollection publishers = new PublisherCsvImportCollection(new CsvParserService());
                        await Task.Run(() => { publishers.LoadFromFile(filePath, ProgressCallback); });

                        // import data
                        PublisherService publisherService = new PublisherService();
                        foreach (var publisher in publishers.GetAll())
                        {
                            if (await publisherService.AddIfNotExists(publisher))
                            {
                                importedCount++;
                            }
                            else
                            {
                                skippedCount++;
                            }
                        }
                    }
                    else //if (this._type == "author")
                    {
                        // parse CSV file
                        AuthorCsvImportCollection authors = new AuthorCsvImportCollection(new CsvParserService());
                        await Task.Run(() => { authors.LoadFromFile(filePath, ProgressCallback); });

                        // import data
                        AuthorService authorService = new AuthorService();
                        foreach (var author in authors.GetAll())
                        {
                            if (await authorService.ExistsWithName(author.FirstName, author.LastName))
                            {
                                skippedCount++;
                            }
                            else
                            {
                                await authorService.Add(author);

                                importedCount++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.label1.Text = "Task aborted.";
                    this.label2.Text = "";

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
    }//class
}
//MIT License

//Copyright (c) 2021-2022

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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary
{
    // TODO: refactor and unit test
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ManageTagsForm : Form
    {
        private ITagService _service;

        private ManageTagsForm()
        {
            InitializeComponent();

            this.tagsList.MultiSelect = false;

            this._service = new TagService();

            // set up ListView
            this.tagsList.GridLines = true;
            this.tagsList.Columns.Clear();
            this.tagsList.Columns.Add("");
            this.tagsList.View = View.Details;
            this.tagsList.HeaderStyle = ColumnHeaderStyle.None;
            this.tagsList.Columns[0].Width = this.tagsList.Width;

            this.deleteSelectedTagButton.Enabled = false;
            this.addTagButton.Enabled = false;

            this.CenterToParent();

            // register event handlers
            this.tagsList.SelectedIndexChanged += ((sender, args) =>
            {
                if (this.tagsList.SelectedItems.Count > 0)
                {
                    this.deleteSelectedTagButton.Enabled = true;
                }
                else
                {
                    this.deleteSelectedTagButton.Enabled = false;
                }
            });
            this.newTagText.TextChanged += ((sender, args) =>
            {
                if (string.IsNullOrWhiteSpace(this.newTagText.Text))
                {
                    this.addTagButton.Enabled = false;
                }
                else
                {
                    this.addTagButton.Enabled = true;
                }
            });
            this.addTagButton.Click += (async (sender, args) =>
            {
                // disable add and delete buttons
                this.addTagButton.Enabled = false;
                this.deleteSelectedTagButton.Enabled = false;

                string newTagName = this.newTagText.Text;

                try
                {
                    // check for existing tag
                    if (await this._service.ExistsWithName(newTagName))
                    {
                        MessageBox.Show("Tag: \"" + newTagName + "\" already exists.", "Manage Tags", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // re-enable add and delete buttons
                        this.addTagButton.Enabled = true;
                        this.deleteSelectedTagButton.Enabled = true;

                        return;
                    }
                }
                catch (Exception ex)
                {
                    // something bad happened
                    MessageBox.Show("Error checking if tag \"" + newTagName + "\" already exists.", "Manage Tags", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // re-enable add and delete buttons
                    this.addTagButton.Enabled = true;
                    this.deleteSelectedTagButton.Enabled = true;

                    return;
                }

                try
                {
                    // add tag
                    await this._service.Add(new Tag { Name = newTagName });

                    // clear new tag field
                    this.newTagText.Clear();
                }
                catch (Exception ex)
                {
                    // something bad happened
                    MessageBox.Show("Error creating tag: " + ex.Message, "Manage Tags", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // re-enable add and delete buttons
                    this.addTagButton.Enabled = true;
                    this.deleteSelectedTagButton.Enabled = true;

                    return;
                }

                try
                {
                    // re-populate the list
                    await FilterTags(this.filterTagField.Text);
                }
                catch(Exception ex)
                {
                    // something bad happened
                    MessageBox.Show("Error reading tags: " + ex.Message, "Manage Tags", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // re-enable add and delete buttons
                    this.addTagButton.Enabled = true;
                    this.deleteSelectedTagButton.Enabled = true;

                    return;
                }

                // re-enable add and delete buttons
                this.addTagButton.Enabled = true;
                this.deleteSelectedTagButton.Enabled = true;

                TagsUpdated?.Invoke(this, args);
            });
            this.deleteSelectedTagButton.Click += (async (sender, args) =>
            {
                // disable add and delete buttons
                this.addTagButton.Enabled = false;
                this.deleteSelectedTagButton.Enabled = false;

                string selectedTag = this.tagsList.SelectedItems[0].SubItems[0].Text;

                try
                {
                    // delete tag
                    await this._service.DeleteByName(selectedTag);
                }
                catch (Exception ex)
                {
                    // something bad happened
                    MessageBox.Show("Error deleting tag ", "Manage Tags", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // re-enable add and delete buttons
                    this.addTagButton.Enabled = true;
                    this.deleteSelectedTagButton.Enabled = true;

                    return;
                }

                try
                {
                    // re-populate the list
                    await FilterTags(this.filterTagField.Text);
                }
                catch (Exception ex)
                {
                    // something bad happened
                    MessageBox.Show("Error reading tags: " + ex.Message, "Manage Tags", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // re-enable add and delete buttons
                    this.addTagButton.Enabled = true;
                    this.deleteSelectedTagButton.Enabled = true;

                    return;
                }

                TagsUpdated?.Invoke(this, args);
            });
            this.filterTagField.TextChanged += (async (sender, args) =>
            {
                await Task.Delay(MainWindow.FILTER_DELAY);
                await FilterTags(this.filterTagField.Text);
            });
            this.applyFilterButton.Click += (async (sender, args) =>
            {
                await FilterTags(this.filterTagField.Text);
            });
            this.clearFilterButton.Click += ((sender, args) =>
            {
                this.filterTagField.Text = "";
            });

            // set tab order
            this.newTagText.TabIndex = 0;
            this.addTagButton.TabIndex = 1;
            this.filterTagField.TabIndex = 0;
            this.tagsList.TabStop = false;
            this.applyFilterButton.TabIndex = 1;
            this.clearFilterButton.TabIndex = 2;
        }

        private async Task FilterTags(string filterText)
        {
            // grab the filter
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(filterText, REGEX_OPTIONS);

            // perform filtering
            var allTags = await this._service.GetAll();
            this.tagsList.Items.Clear();
            foreach (var tag in allTags)
            {
                if (filterPattern.IsMatch(tag.Name))
                {
                    this.tagsList.Items.Add(tag.Name);
                }
            }

            if (this.tagsList.Items.Count > 0)
            {
                this.deleteSelectedTagButton.Enabled = this.tagsList.SelectedItems.Count > 0;
            }
            else
            {
                this.deleteSelectedTagButton.Enabled = false;
            }
        }//FilterTags

        public event EventHandler TagsUpdated;

        public static async Task<ManageTagsForm> CreateAsync()
        {
            ManageTagsForm form = new ManageTagsForm();
            await form.PopulateTags();
            
            return form;
        }

        async Task PopulateTags()
        {
            this.tagsList.Items.Clear();

            var allTags = await this._service.GetAll();
            foreach (var tag in allTags)
            {
                this.tagsList.Items.Add(tag.Name);
            }
        }//PopulateTags
    }//class
}

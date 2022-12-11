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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;

namespace MyLibrary
{
    // TODO: refactor and unit test
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ManageTagsForItemDialog : Form
    {
        private ITagService _tagService;

        private Item _item;

        private List<string> _checkedTags;

        public ManageTagsForItemDialog(Item item)
        {
            InitializeComponent();

            this._tagService = new TagService();

            this._item = item;
            this.itemTitleLabel.Text = item.Title;

            this.tagsList.CheckOnClick = true;

            this._checkedTags = new List<string>();   

            this.CenterToParent();

            // initially disable add and save buttons
            this.buttonSave.Enabled = false;
            this.addNewTagButton.Enabled = false;

            // register event handlers
            this.buttonCancel.Click += ((sender, args) =>
            {
                this.Close();
            });
            this.buttonSave.Click += (async (sender, args) =>
            {
                // disable add, save and cancel buttons
                this.addNewTagButton.Enabled = false;
                this.buttonSave.Enabled = false;
                this.buttonCancel.Enabled = false;

                // save changes
                List<string> originalTags = new List<string>();
                foreach (var tag in this._item.Tags)
                {
                    originalTags.Add(tag.Name);
                }
                ItemTagsDto dto = new ItemTagsDto(this._item.Id, originalTags, this.SelectedTags);
                try
                {
                    if (this._item.Type == ItemType.Book)
                    {
                        IBookService _bookService = new BookService();
                        await _bookService.UpdateTagsAsync(dto);
                    }
                    else
                    {
                        IMediaItemService _itemRepo = new MediaItemService();
                        await _itemRepo.UpdateTagsAsync(dto);
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    // something bad happened
                    // notify the user
                    MessageBox.Show("Error updating tags: " + ex.Message, "manage tags", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // re-enable buttons
                    this.addNewTagButton.Enabled = true;
                    this.buttonSave.Enabled = true;
                    this.buttonCancel.Enabled = true;
                }

                TagsUpdated?.Invoke(this, args);
            });
            this.newTagField.TextChanged += ((sender, args) =>
            {
                this.addNewTagButton.Enabled = !string.IsNullOrWhiteSpace(this.newTagField.Text);
            });
            this.addNewTagButton.Click += (async (sender, args) =>
            {
                // disable add, save and cancel buttons
                this.addNewTagButton.Enabled = false;
                this.buttonSave.Enabled = false;
                this.buttonCancel.Enabled = false;

                string newTagName = this.newTagField.Text;

                try
                {
                    // check for existing tag
                    bool tagAlreadyInList = false;
                    foreach (var tagInList in this.tagsList.Items)
                    {
                        if (tagInList.ToString().Equals(newTagName))
                        {
                            tagAlreadyInList = true;
                        }
                    }
                    if (await this._tagService.ExistsWithName(newTagName) ||
                        tagAlreadyInList)
                    {
                        MessageBox.Show("Tag: \"" + newTagName + "\" already exists.", "Manage tags", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // re-enable buttons
                        this.addNewTagButton.Enabled = true;
                        this.buttonSave.Enabled = true;
                        this.buttonCancel.Enabled = true;

                        return;
                    }
                }
                catch (Exception ex)
                {
                    // something bad happened
                    MessageBox.Show("Error checking if tag \"" + newTagName + "\" exists: " + ex.Message, "Manage tags", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // re-enable buttons
                    this.addNewTagButton.Enabled = true;
                    this.buttonSave.Enabled = true;
                    this.buttonCancel.Enabled = true;

                    return;
                }

                this.tagsList.Items.Add(this.newTagField.Text, true);

                // re-enable buttons
                this.addNewTagButton.Enabled = true;
                this.buttonSave.Enabled = true;
                this.buttonCancel.Enabled = true;
            });
            this.tagsList.ItemCheck += ((sender, args) =>
            {
                this.buttonSave.Enabled = true;
            });
            this.filterTagField.TextChanged += (async (sender, args) =>
            {
                await Task.Delay(MainWindow.FILTER_DELAY);
                await FilterTags(this.filterTagField.Text);
            });
            this.clearFilterButton.Click += ((sender, args) =>
            {
                this.filterTagField.Text = "";
            });
            this.applyFilterButton.Click += (async (sender, args) =>
            {
                await FilterTags(this.filterTagField.Text);
            });
            this.tagsList.ItemCheck += ((sender, args) =>
            {
                if (args.NewValue == CheckState.Checked)
                {
                    if (!this._checkedTags.Contains(tagsList.Items[args.Index]))
                    {
                        this._checkedTags.Add(tagsList.Items[args.Index].ToString());
                    }
                }
                else if (args.NewValue == CheckState.Unchecked)
                {
                    this._checkedTags.Remove(tagsList.Items[args.Index].ToString());
                    this._checkedTags.Remove(tagsList.Items[args.Index].ToString());
                }
            });

            // set tab order
            this.tagsList.TabStop = false;
            this.newTagField.TabIndex = 0;
            this.addNewTagButton.TabIndex = 1;
            this.filterTagField.TabIndex = 0;
            this.applyFilterButton.TabIndex = 1;
            this.clearFilterButton.TabIndex = 2;
            this.buttonSave.TabIndex = 3;
            this.buttonCancel.TabIndex = 4;
        }

        private async Task FilterTags(string filterText)
        {
            // grab the filter
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(filterText, REGEX_OPTIONS);

            // perform filtering
            List<Tag> allTags = (await this._tagService.GetAll()).ToList();
            for (int i = 0; i < tagsList.Items.Count; i++)
            {
                string currTagName = tagsList.Items[i].ToString();
                if (!allTags.Any(t => t.Name.Equals(currTagName)))
                {
                    allTags.Add(new Models.Entities.Tag { Name = currTagName });
                }
            }
            // tell the list it is being updated
            tagsList.BeginUpdate();
            tagsList.Items.Clear();
            // fill the list with filtered items
            foreach (Tag tag in allTags)
            {
                if (filterPattern.IsMatch(tag.Name))
                {
                    tagsList.Items.Add(tag.Name);
                }
            }
            // now check previously-checked tags
            for (int i = 0; i < tagsList.Items.Count; i++)
            {
                string currTagName = tagsList.Items[i].ToString();
                if (_checkedTags.Contains(currTagName))
                {
                    tagsList.SetItemChecked(tagsList.Items.IndexOf(currTagName), true);
                }
            }
            // finished
            tagsList.EndUpdate();
        }//FilterTags

        public event EventHandler TagsUpdated;

        public static async Task<ManageTagsForItemDialog> CreateAsync(Item item)
        {
            ManageTagsForItemDialog form = new ManageTagsForItemDialog(item);
            await form.PopulateTags();

            return form;
        }

        public IEnumerable<string> SelectedTags
        {
            get
            {
                foreach (var tag in this._checkedTags)
                    yield return tag.ToString();
            }
        }

        async Task PopulateTags()
        {
            // populate list
            this.tagsList.Items.Clear();
            var allTags = await this._tagService.GetAll();
            foreach (var tag in allTags)
            {
                bool isChecked = this._item.Tags.Any(t => t.Name.Equals(tag.Name));
                this.tagsList.Items.Add(tag.Name, isChecked);

                // take note of checked tags
                if (isChecked)
                    _checkedTags.Add(tag.Name);
            }
            this.buttonSave.Enabled = false;
        }//PopulateTags
    }//class
}
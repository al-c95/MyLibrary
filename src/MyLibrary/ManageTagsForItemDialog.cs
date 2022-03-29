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
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views;
using MyLibrary.Models.Entities;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ManageTagsForItemDialog : Form
    {
        private ITagService _tagService;

        private Item _item;

        public ManageTagsForItemDialog(Item item)
        {
            InitializeComponent();

            this._tagService = new TagService();

            this._item = item;
            this.itemTitleLabel.Text = item.Title;

            this.tagsList.CheckOnClick = true;

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
                    originalTags.Add(tag.Name);
                ItemTagsDto dto = new ItemTagsDto(this._item.Id, originalTags, this.SelectedTags);
                try
                {
                    if (this._item.Type == ItemType.Book)
                    {
                        IBookService _bookService = new BookService();
                        await _bookService.UpdateTags(dto);
                    }
                    else
                    {
                        IMediaItemService _itemRepo = new MediaItemService();
                        await _itemRepo.UpdateTags(dto);
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
        }

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
                foreach (var tag in this.tagsList.CheckedItems)
                    yield return tag.ToString();
            }
        }

        async Task PopulateTags()
        {
            this.tagsList.Items.Clear();

            var allTags = await this._tagService.GetAll();
            foreach (var tag in allTags)
            {
                bool checkedState = this._item.Tags.Any(t => t.Name.Equals(tag.Name));
                this.tagsList.Items.Add(tag.Name, checkedState);
            }

            this.buttonSave.Enabled = false;
        }//PopulateTags
    }//class
}

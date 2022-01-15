using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MyLibrary.BusinessLogic.Repositories; // TODO: remove

using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views;
using MyLibrary.Models.Entities;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ManageTagsForItemDialog : Form
    {
        private TagRepository _tagRepo;

        private Item _item;

        public ManageTagsForItemDialog(Item item)
        {
            InitializeComponent();

            this._tagRepo = new TagRepository();

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
                    if (await this._tagRepo.ExistsWithName(newTagName))
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

            var allTags = await this._tagRepo.GetAll();
            foreach (var tag in allTags)
            {
                bool checkedState = this._item.Tags.Any(t => t.Name.Equals(tag.Name));
                this.tagsList.Items.Add(tag.Name, checkedState);
            }

            this.buttonSave.Enabled = false;
        }//PopulateTags
    }//class
}

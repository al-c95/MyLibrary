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
using MyLibrary.DataAccessLayer;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Views;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ManageTagsForm : Form
    {
        private TagRepository _repo;

        private ManageTagsForm()
        {
            InitializeComponent();

            this._repo = new TagRepository();

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
                    if (await this._repo.ExistsWithName(newTagName))
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
                    await this._repo.Create(new Tag { Name = newTagName });

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
                    await PopulateTags();
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
                    await this._repo.DeleteByName(selectedTag);
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
                    await PopulateTags();
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
        }

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

            var allTags = await this._repo.GetAll();
            foreach (var tag in allTags)
            {
                this.tagsList.Items.Add(tag.Name);
            }
        }//PopulateTags
    }//class
}

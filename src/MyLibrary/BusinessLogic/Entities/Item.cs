using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities.Builders;

namespace MyLibrary.Models.Entities
{
    /// <summary>
    /// Base class for book or media item that can be stored in the database.
    /// </summary>
    public abstract class Item : Entity // abstract class to reduce code duplication of title and image properties
    {
        public Item()
        { 
            this.Tags = new List<Tag>(); 
        }

        private string _title;
        public string Title
        {
            get => this._title;
            set
            {
                if (value == null || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Can't have an empty title.");
                }
                else
                {
                    _title = value;
                }
            }
        }

        public string Image { get; set; } // conversion to and from images is handled by the presentation layer (view)
        public string Notes { get; set; }

        public ICollection<Tag> Tags;

        public string GetCommaDelimitedTags()
        {
            var tagsBuilder = new StringBuilder();
            int tagCount = 0;
            foreach (var t in this.Tags)
            {
                tagsBuilder.Append(t.Name);

                if (tagCount < this.Tags.Count - 1)
                    tagsBuilder.Append(", ");

                tagCount++;
            }

            return tagsBuilder.ToString();
        }
    }//class
}
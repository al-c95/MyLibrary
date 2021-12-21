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
        public abstract ItemType Type { get; set; }

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

        public byte[] Image { get; set; } // conversion to and from images is handled by the presentation layer (view)
        public string Notes { get; set; }

        public ICollection<Tag> Tags;

        /// <summary>
        /// Gets the list of tags for the item in the format: tag, tag, ... tag
        /// </summary>
        /// <returns></returns>
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

        public static bool IsValidImageFileType(string path)
        {
            return (System.IO.Path.GetExtension(path).Equals(".bmp") ||
                    System.IO.Path.GetExtension(path).Equals(".jpg") ||
                    System.IO.Path.GetExtension(path).Equals(".jpeg") ||
                    System.IO.Path.GetExtension(path).Equals(".png"));
        }

        public ItemMemento GetMemento()
        {
            return new ItemMemento(this.Notes, this.Image);
        }

        public void Restore(ItemMemento m)
        {
            if (m != null)
            {
                this.Notes = m.Notes;
                this.Image = m.Image;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Id: ");
            builder.AppendLine(this.Id.ToString());
            builder.AppendLine();
            builder.AppendLine("Title: ");
            builder.AppendLine(this.Title);
            builder.AppendLine();
            builder.AppendLine("Type: ");
            builder.Append(this.Type.ToString());

            return builder.ToString();
        }
    }//class
}
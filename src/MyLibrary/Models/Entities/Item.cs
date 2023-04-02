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
using System.Text;

namespace MyLibrary.Models.Entities
{
    /// <summary>
    /// Base class for book or media item that can be stored in the database.
    /// </summary>
    public abstract class Item : ItemBase
    {
        public Item()
        { 
            this.Tags = new List<Tag>(); 
        }

        public byte[] Image { get; set; } // conversion to and from images is handled by the presentation layer (view)

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

        protected StringBuilder ToStringAppendField(StringBuilder builder, string fieldName, string value)
        {
            builder.AppendLine(fieldName);
            builder.AppendLine(value);
            builder.AppendLine();

            return builder;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            ToStringAppendField(builder, "Id: ", this.Id.ToString());
            ToStringAppendField(builder, "Title: ", this.Title);
            builder.AppendLine("Type: ");
            builder.Append(GetTypeString(this.Type));

            return builder.ToString();
        }

        public static ItemType ParseType(string type)
        {
            if (type.Equals("Flash Drive"))
            {
                return ItemType.FlashDrive;
            }
            else if (type.Equals("Floppy Disk"))
            {
                return ItemType.FloppyDisk;
            }
            else if (type.Equals("4k BluRay"))
            {
                return ItemType.UhdBluRay;
            }
            else
            {
                if (Enum.TryParse(type, out ItemType parsed))
                    return parsed;
                else
                    throw new FormatException("Cannot parse type: " + type);
            }
        }

        public static string GetTypeString(ItemType type)
        {
            if (type == ItemType.FlashDrive)
            {
                return "Flash Drive";
            }
            else if (type == ItemType.FloppyDisk)
            {
                return "Floppy Disk";
            }
            else if (type == ItemType.UhdBluRay)
            {
                return "4k BluRay";
            }
            else
            {
                return type.ToString();
            }
        }
    }//class
}
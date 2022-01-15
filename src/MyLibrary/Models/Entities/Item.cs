﻿//MIT License

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
            builder.Append(this.Type.ToString());

            return builder.ToString();
        }
    }//class
}
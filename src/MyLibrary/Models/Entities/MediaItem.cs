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
using System.Text;

namespace MyLibrary.Models.Entities
{
    public class MediaItem : Item
    {
        private ItemType _type;
        public override ItemType Type
        {
            get => this._type;
            set
            {
                if (value == ItemType.Book)
                    throw new InvalidOperationException("Cannot set MediaItem.Type to ItemType.Book");

                this._type = value;
            } 
        }

        public long Number { get; set; }
        public int? RunningTime { get; set; }
        public int ReleaseYear { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());

            builder.AppendLine();
            builder.AppendLine();

            ToStringAppendField(builder, "Number: ", this.Number.ToString());

            builder.AppendLine("Running Time: ");
            if (!(RunningTime is null))
            {
                builder.AppendLine(this.RunningTime.ToString());
                builder.AppendLine();
            }
            else
            {
                builder.AppendLine();
                builder.AppendLine();
            }
            builder.AppendLine("Release Year: ");
            builder.Append(this.ReleaseYear.ToString());

            return builder.ToString();
        }//ToString
    }//class
}

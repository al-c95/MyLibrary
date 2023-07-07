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
using System.Linq;

namespace MyLibrary.Models.Entities.Builders
{
    public class MediaItemBuilder : IMediaItemBuilder
    {
        protected MediaItem _item;

        public MediaItemBuilder()
        {
            this._item = new MediaItem();
        }

        public IMediaItemBuilder WithTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new InvalidOperationException("Name cannot be empty.");
            }

            this._item.Title = title;

            return this;
        }

        public IMediaItemBuilder WithNumber(object value)
        {
            if (value is null)
            {
                throw new InvalidOperationException("Number cannot be null.");
            }

            long number = 0;
            if (!long.TryParse(value.ToString(), out number))
            {
                throw new InvalidOperationException($"Could not parse number value: {value.ToString()}");
            }
            this._item.Number = number;

            return this;
        }

        public IMediaItemBuilder WithYear(object value)
        {
            if (value is null)
            {
                throw new InvalidOperationException("Year cannot be null.");
            }

            int year = 0;
            if (!int.TryParse(value.ToString(), out year))
            {
                throw new InvalidOperationException($"Could not parse year value: {value.ToString()}");
            }
            this._item.ReleaseYear = year;

            return this;
        }

        public IMediaItemBuilder WithRunningTime(object value)
        {
            if (value is null)
            {
                this._item.RunningTime = null;
                return this;
            }
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                this._item.RunningTime = null;
                return this;
            }

            int runTime = 0;
            if (!int.TryParse(value.ToString(), out runTime))
            {
                throw new InvalidOperationException($"Could not parse running time value: {value.ToString()}");
            }
            this._item.RunningTime = runTime;

            return this;
        }

        public IMediaItemBuilder WithTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                if (!string.IsNullOrWhiteSpace (tag))
                {
                    if (!this._item.Tags.Any(t => t.Name == tag))
                    {
                        this._item.Tags.Add(new Tag
                        { 
                            Name = tag 
                        });
                    }
                }
            }

            return this;
        }

        public MediaItem Build() => this._item;

        public static implicit operator MediaItem(MediaItemBuilder builder) => builder.Build();
    }//class
}
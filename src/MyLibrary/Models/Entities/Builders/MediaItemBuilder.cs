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

namespace MyLibrary.Models.Entities.Builders
{
    public class MediaItemBuilder : ItemBuilderBase<MediaItem>
    {
        public MediaItemBuilder()
        {
            this._item = new MediaItem();
        }

        public MediaItemBuilder WithTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            this._item.Title = title;

            return this;
        }

        public MediaItemBuilder WithNumber(object value)
        {
            if (value is null)
            {
                throw new ArgumentException("Number cannot be null.");
            }

            long number = 0;
            if (!long.TryParse(value.ToString(), out number))
            {
                throw new ArgumentException($"Could not parse number value: {value.ToString()}");
            }
            this._item.Number = number;

            return this;
        }

        public MediaItemBuilder WithYear(object value)
        {
            if (value is null)
            {
                throw new ArgumentException("Year cannot be null.");
            }

            int year = 0;
            if (!int.TryParse(value.ToString(), out year))
            {
                throw new ArgumentException($"Could not parse year value: {value.ToString()}");
            }
            this._item.ReleaseYear = year;

            return this;
        }

        public MediaItemBuilder WithRunningTime(object value)
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
                throw new ArgumentException($"Could not parse running time value: {value.ToString()}");
            }
            this._item.RunningTime = runTime;

            return this;
        }

        public static implicit operator MediaItem(MediaItemBuilder builder) => builder.Build();
    }//class
}
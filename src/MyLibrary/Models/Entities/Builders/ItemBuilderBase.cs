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
    public abstract class ItemBuilderBase<T> where T : Item
    {
        protected T _item;
        protected ItemBuilderBase<T> _builder;

        public ItemBuilderBase<T> WithId(object id) 
        {
            if (id is null)
            {
                throw new ArgumentException("Id cannot be null.");
            }

            int idValue = 0;
            if (int.TryParse(id.ToString(), out idValue))
            {
                this._item.Id = idValue;
            }
            else
            {
                throw new ArgumentException($"Could not parse: {id.ToString()}");
            }

            return this;
        }

        public ItemBuilderBase<T> WithTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    if (tag.Contains(","))
                    {
                        throw new ArgumentException($"Tag: {tag} has invalid format");
                    }

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

        public T Build() => this._item;
    }
}
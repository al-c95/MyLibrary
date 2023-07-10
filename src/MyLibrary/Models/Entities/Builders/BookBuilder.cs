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
using System.Text.RegularExpressions;
using System.Linq;

namespace MyLibrary.Models.Entities.Builders
{
    public class BookBuilder
    {
        protected Book _book;

        public BookBuilder()
        {
            this._book = new Book();
        }

        public BookBuilder WithTitles(string title, string longTitle)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                this._book.Title = title;
                this._book.TitleLong = longTitle;

                return this;
            }

            throw new ArgumentException("Can't have an empty title.");
        }

        public BookBuilder WrittenBy(IEnumerable<string> authors) 
        {
            foreach (var a in authors)
            {
                Author author = new Author();
                author.SetFullNameFromCommaFormat(a);
                if (!this._book.Authors.Contains(author))
                {
                    this._book.Authors.Add(author);
                }            
            }

            return this;
        }

        public BookBuilder WithIsbns(string isbn10, string isbn13)
        {
            if (!string.IsNullOrWhiteSpace(isbn10))
            {
                if (!Regex.IsMatch(isbn10, Book.ISBN_10_PATTERN))
                {
                    throw new ArgumentException($"ISBN: {isbn10} has invalid format.");
                }
            }
            this._book.Isbn = isbn10;

            if (!string.IsNullOrWhiteSpace(isbn13))
            {
                if (!Regex.IsMatch(isbn13, Book.ISBN_13_PATTERN))
                {
                    throw new ArgumentException($"ISBN: {isbn13} has invalid format.");
                }
            }
            this._book.Isbn13 = isbn13;

            return this;
        }

        public BookBuilder WithPages(object value)
        {
            int pages;
            if (int.TryParse(value.ToString(), out pages))
            {
                this._book.Pages = pages;
                return this;
            }

            throw new ArgumentException($"Could not parse pages: {value.ToString()}");
        }

        public BookBuilder InLanguage(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                throw new ArgumentException("Language cannot be empty.");
            }

            this._book.Language = language;
            return this;
        }

        public BookBuilder PublishedBy(string publisher)
        {
            if (string.IsNullOrWhiteSpace(publisher))
            {
                throw new ArgumentException("Publisher cannot be empty.");
            }

            this._book.Publisher = new Publisher(publisher);
            return this;
        }

        public BookBuilder WithDeweyDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                this._book.DeweyDecimal = null;
                return this;
            }

            if (Regex.IsMatch((string)value, Book.DEWEY_DECIMAL_PATTERN))
            {
                this._book.DeweyDecimal = decimal.Parse(value);
                return this;
            }

            throw new ArgumentException($"Dewey decimal: {value.ToString()} has invalid format.");
        }

        public BookBuilder WithTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    if (!this._book.Tags.Any(t => t.Name == tag))
                    {
                        this._book.Tags.Add(new Tag
                        {
                            Name = tag
                        });
                    }
                }
            }

            return this;
        }

        public Book Build() => this._book;

        public static implicit operator Book(BookBuilder builder) => builder.Build();
    }//class
}
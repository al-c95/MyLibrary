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

using System.Collections.Generic;
using MyLibrary.Models.Entities.Builders;

namespace MyLibrary.Models.Entities.Factories
{
    public class BookFactory : IBookFactory
    {
        private readonly BookBuilder _builder;

        public BookFactory()
        {
            this._builder = new BookBuilder();
        }

        public struct Titles
        {
            public string Title;
            public string LongTitle;
        }

        public struct Isbns
        {
            public string Isbn10;
            public string Isbn13;
        }

        public Book Create(Titles titles, Isbns isbns,
            string pages, string language, string publisher, string deweyDecimal, IEnumerable<string> tags, IEnumerable<string> authors)
        {
            return this._builder
                .WithTitles(titles.Title, titles.LongTitle)
                .WrittenBy(authors)
                .WithIsbns(isbns.Isbn10, isbns.Isbn13)
                .WithPages(pages)
                .InLanguage(language)
                .PublishedBy(publisher)
                .WithDeweyDecimal(deweyDecimal)
                .WithTags(tags)
                .Build();
        }
    }
}

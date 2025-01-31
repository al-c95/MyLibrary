﻿//MIT License

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
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using System.Linq;

namespace MyLibrary_Test.Models_Tests.Entities_Tests.Builders_Tests
{
    [TestFixture]
    public class BookBuilder_Tests
    {
        [TestCase(1)]
        [TestCase("1")]
        public void WithId_Test_Valid(object value)
        {
            BookBuilder builder = new BookBuilder();

            Book result = builder.WithId(value).Build();

            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void WithId_Test_Empty()
        {
            BookBuilder builder = new BookBuilder();

            Assert.Throws<ArgumentException>(() => builder.WithId("").Build());
        }

        [Test]
        public void WithId_Test_Invalid()
        {
            BookBuilder builder = new BookBuilder();

            Assert.Throws<ArgumentException>(() => builder.WithId("bogus id").Build());
        }

        [Test]
        public void WithTitles_Test_Valid()
        {
            string title = "book";
            string longTitle = "a very long book";
            BookBuilder builder = new BookBuilder();

            Book result = builder.WithTitles(title, longTitle).Build();

            Assert.AreEqual(title, result.Title);
            Assert.AreEqual(longTitle, result.TitleLong);
        }

        [TestCase("")]
        [TestCase(null)]
        public void WithTitles_Test_Invalid(string title)
        {
            string longTitle = "a very long book";
            BookBuilder builder = new BookBuilder();

            Assert.Throws<ArgumentException>(() => builder.WithTitles(title, longTitle).Build());
        }

        [TestCase("", "")]
        [TestCase("012345X789", "")]
        [TestCase("012345X789", "012345X789012")]
        [TestCase("", "012345X789012")]
        public void WithIsbns_Test_BothValid(string isbn10, string isbn13)
        {
            BookBuilder builder = new BookBuilder();

            Book result = builder.WithIsbns(isbn10, isbn13).Build();

            Assert.AreEqual(isbn10, result.Isbn);
            Assert.AreEqual(isbn13, result.Isbn13);
        }

        [TestCase("012345X789", "bogus_isbn13")]
        [TestCase("", "bogus_isbn13")]
        [TestCase("bogus_isbn10", "")]
        [TestCase("bogus_isbn10", "012345X789012")]
        public void WithIsbns_Test_OneInvalid(string isbn10, string isbn13)
        {
            BookBuilder builder = new BookBuilder();

            Assert.Throws<ArgumentException>(() => builder.WithIsbns(isbn10, isbn13));
        }

        [Test]
        public void WrittenBy_Test()
        {
            BookBuilder builder = new BookBuilder();
            string author1 = "Smith, John";
            string author2 = "Doe, Jane";
            string author3 = "Smith-Jones, John";
            List<string> authors = new List<string>
            { author1,
            author2,
            author2,
            author3
            };

            Book result = builder.WrittenBy(authors).Build();

            Assert.AreEqual(3, result.Authors.Count);
            Assert.IsTrue(result.Authors.Any(a => a.FirstName == "John" && a.LastName == "Smith"));
            Assert.IsTrue(result.Authors.Any(a => a.FirstName == "Jane" && a.LastName == "Doe"));
            Assert.IsTrue(result.Authors.Any(a => a.FirstName == "John" && a.LastName == "Smith-Jones"));
        }

        [Test]
        public void WithIsbns_Test_BothInvalid()
        {
            BookBuilder builder = new BookBuilder();

            Assert.Throws<ArgumentException>(() => builder.WithIsbns("bogus_isbn10", "bogus_isbn13"));
        }

        [Test]
        public void WithPages_Test_Valid()
        {
            BookBuilder builder = new BookBuilder();
            int pages = 100;

            Book result = builder.WithPages(pages).Build();

            Assert.AreEqual(result.Pages, pages);
        }

        [Test]
        public void WithPages_Test_Invalid()
        {
            BookBuilder builder = new BookBuilder();
            string pages = "bogus_pages";

            Assert.Throws<ArgumentException>(() => builder.WithPages(pages));
        }

        [Test]
        public void InLanguage_Test_Valid()
        {
            BookBuilder builder = new BookBuilder();
            string language = "English";

            Book result = builder.InLanguage(language).Build();

            Assert.AreEqual(language, result.Language);
        }

        [Test]
        public void InLanguage_Test_Invalid()
        {
            BookBuilder builder = new BookBuilder();

            Assert.Throws<ArgumentException>(() => builder.InLanguage("").Build());
        }

        [Test]
        public void PublishedBy_Test_Valid()
        {
            BookBuilder builder = new BookBuilder();
            string publisher = "publisher";

            Book result = builder.PublishedBy(publisher).Build();

            Assert.AreEqual(publisher, result.Publisher.Name);
        }

        [Test]
        public void PublishedBy_Test_Invalid()
        {
            BookBuilder builder = new BookBuilder();
            string publisher = "";

            Assert.Throws<ArgumentException>(() => builder.PublishedBy(publisher));
        }

        [Test]
        public void WithDeweyDecimal_Test_Valid()
        {
            BookBuilder builder = new BookBuilder();
            string deweyDecimal1 = "100";
            string deweyDecimal2 = "100.5";
            string deweyDecimal3 = "";

            Book result1 = builder.WithDeweyDecimal(deweyDecimal1).Build();
            builder = new BookBuilder();
            Book result2 = builder.WithDeweyDecimal(deweyDecimal2).Build();
            builder = new BookBuilder();
            Book result3 = builder.WithDeweyDecimal(deweyDecimal3).Build();

            Assert.AreEqual(100, result1.DeweyDecimal);
            Assert.AreEqual(100.5m, result2.DeweyDecimal);
            Assert.AreEqual(null, result3.DeweyDecimal);
        }

        [Test]
        public void WithDeweyDecimal_Test_Invalid()
        {
            BookBuilder builder = new BookBuilder();

            Assert.Throws<ArgumentException>(() => builder.WithDeweyDecimal("bogus_Dewey_decimal").Build());
        }

        [Test]
        public void WithTags_Test_Valid()
        {
            BookBuilder builder = new BookBuilder();
            List<string> tags = new List<string>
            {
                "tag1",
                "tag2",
                "tag2",
                "tag3",
                ""
            };

            Book item = builder.WithTags(tags).Build();

            Assert.AreEqual(3, item.Tags.Count);
            Assert.IsTrue(item.Tags.Any(t => t.Name == "tag1"));
            Assert.IsTrue(item.Tags.Any(t => t.Name == "tag2"));
            Assert.IsTrue(item.Tags.Any(t => t.Name == "tag3"));
        }

        [Test]
        public void WithTags_Test_Invalid()
        {
            BookBuilder builder = new BookBuilder();
            List<string> tags = new List<string>
            {
                "tag,"
            };

            Assert.Throws<ArgumentException>(() => builder.WithTags(tags).Build());
        }
    }//class
}
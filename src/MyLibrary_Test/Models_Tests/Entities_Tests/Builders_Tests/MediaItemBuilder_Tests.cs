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
using NUnit.Framework;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;

namespace MyLibrary_Test.Models_Tests.Entities_Tests.Builders_Tests
{
    [TestFixture]
    public class MediaItemBuilder_Tests
    {
        [Test]
        public void WithTitle_Test_Valid()
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            MediaItem item = builder.WithTitle("Title").Build();

            Assert.AreEqual("Title", item.Title);
        }

        [TestCase(null)]
        [TestCase("")]
        public void WithTitle_Test_Invalid(string title)
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            Assert.Throws<InvalidOperationException>(() => builder.WithTitle(title).Build());
        }

        [TestCase(0123)]
        [TestCase("0123")]
        public void WithNumber_Test_Valid(object value)
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            MediaItem item = builder.WithNumber(value).Build();

            Assert.AreEqual(0123, item.Number);
        }

        [TestCase(null)]
        [TestCase("bogus number")]
        public void WithNumber_Test_Invalid(object value)
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            Assert.Throws<InvalidOperationException>(() => builder.WithNumber(value));
        }

        [TestCase(2023)]
        [TestCase("2023")]
        public void WithYear_Test_Valid(object value)
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            MediaItem item = builder.WithYear(value).Build();

            Assert.AreEqual(2023, item.ReleaseYear);
        }

        [TestCase(null)]
        [TestCase("bogus year")]
        public void WithYear_Test_Invalid(object value)
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            Assert.Throws<InvalidOperationException>(() => builder.WithYear(value));
        }

        [TestCase(null)]
        [TestCase("")]
        public void WithRunningTime_Test_Valid_NoValue(object value)
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            MediaItem item = builder.WithRunningTime(value).Build();

            Assert.AreEqual(null, item.RunningTime);
        }

        [TestCase("80")]
        [TestCase(80)]
        public void WithRunningTime_Test_Valid_HasValue(object value)
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            MediaItem item = builder.WithRunningTime(value).Build();

            Assert.AreEqual(80, item.RunningTime);
        }

        [Test]
        public void WithRunningTime_Test_Invalid()
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            Assert.Throws<InvalidOperationException>(() => builder.WithRunningTime("bogus running time").Build());
        }
    }
}
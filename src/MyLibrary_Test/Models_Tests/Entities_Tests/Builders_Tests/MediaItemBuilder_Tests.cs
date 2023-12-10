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
using NUnit.Framework;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using System.Linq;
using MyLibrary.Import;

namespace MyLibrary_Test.Models_Tests.Entities_Tests.Builders_Tests
{
    [TestFixture]
    public class MediaItemBuilder_Tests
    {
        [TestCase(1)]
        [TestCase("1")]
        public void WithId_Test_Valid(object value)
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            MediaItem result = builder.WithId(value).Build();

            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void WithId_Test_Empty()
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            Assert.Throws<ArgumentException>(() => builder.WithId("").Build());
        }

        [Test]
        public void WithId_Test_Invalid()
        {
            MediaItemBuilder builder = new MediaItemBuilder();

            Assert.Throws<ArgumentException>(() => builder.WithId("bogus id").Build());
        }

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

            Assert.Throws<ArgumentException>(() => builder.WithTitle(title).Build());
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

            Assert.Throws<ArgumentException>(() => builder.WithNumber(value));
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

            Assert.Throws<ArgumentException>(() => builder.WithYear(value));
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

            Assert.Throws<ArgumentException>(() => builder.WithRunningTime("bogus running time").Build());
        }

        [Test]
        public void WithTags_Test_Valid()
        {
            MediaItemBuilder builder = new MediaItemBuilder();
            List<string> tags = new List<string>
            {
                "tag1",
                "tag2",
                "tag2",
                "tag3",
                ""
            };

            MediaItem item = builder.WithTags(tags).Build();

            Assert.AreEqual(3, item.Tags.Count);
            Assert.IsTrue(item.Tags.Any(t => t.Name == "tag1"));
            Assert.IsTrue(item.Tags.Any(t => t.Name == "tag2"));
            Assert.IsTrue(item.Tags.Any(t => t.Name == "tag3"));
        }

        [Test]
        public void WithTags_Test_Invalid()
        {
            MediaItemBuilder builder = new MediaItemBuilder();
            List<string> tags = new List<string>
            {
                "tag,"
            };

            Assert.Throws<ArgumentException>(() => builder.WithTags(tags).Build());
        }
    }
}
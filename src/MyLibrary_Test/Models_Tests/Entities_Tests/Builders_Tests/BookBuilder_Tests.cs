using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;

namespace MyLibrary_Test.Models_Tests.Entities_Tests.Builders_Tests
{
    [TestFixture]
    public class BookBuilder_Tests
    {
        [Test]
        public void WithIsbn_Test_Valid()
        {
            // arrange/act
            string isbn = "0123456789";
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithIsbn(isbn)
                    .Get();

            // assert
            Assert.AreEqual(isbn, book.GetIsbn());
        }

        [TestCase("fhkjgh")]
        [TestCase("01234h6789")]
        [TestCase("01234567890")]
        public void WithIsbn_Test_Invalid(string isbn)
        {
            Assert.Throws<FormatException>(() => BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithIsbn(isbn));
        }

        [Test]
        public void WithIsbn13_Test_Valid()
        {
            // arrange/act
            string isbn = "0123456789012";
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithIsbn13(isbn)
                    .Get();

            // assert
            Assert.AreEqual(isbn, book.GetIsbn());
        }

        [TestCase("fhkjgh")]
        [TestCase("01234h6789012")]
        [TestCase("01234567890")]
        public void WithIsbn13_Test_Invalid(string isbn)
        {
            Assert.Throws<FormatException>(() => BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithIsbn13(isbn));
        }

        [Test]
        public void WithTags_Test()
        {
            // arrange/acts
            List<Tag> tags = new List<Tag>
            {
                new Tag
                {
                    Name = "tag1"
                },
                new Tag
                {
                    Name = "tag2"
                }
            };
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithTags(tags)
                    .Get();
            var tagsResult = (List<Tag>)(book.Tags);

            // assert
            Assert.IsTrue(tagsResult.Count == 2);
            Assert.IsTrue(tagsResult.Any(t => t.Name == "tag1"));
            Assert.IsTrue(tagsResult.Any(t => t.Name == "tag1"));
        }
    }//class
}

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
        public void WithIsbn_Test_10()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithIsbn("0123456789")
                    .Get();

            // assert
            Assert.AreEqual("0123456789", book.Isbn);
        }

        [Test]
        public void WithIsbn_Test_13()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithIsbn("0123456789012")
                    .Get();

            // assert
            Assert.AreEqual("0123456789012", book.Isbn13);
        }

        [TestCase("0")]
        [TestCase("01234567890")]
        [TestCase("01234567890123")]
        public void WithIsbn_Test_IncorrectNumberOfDigits(string isbn)
        {
            Assert.Throws<ArgumentException>(() => BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithIsbn(isbn)
                    .Get());
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

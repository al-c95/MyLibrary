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
            // arrange/act
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

        [TestCase("")]
        [TestCase(null)]
        public void WithDeweyDecimal_Test_Null(string dewey)
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithDeweyDecimal(dewey)
                    .Get();

            // assert
            Assert.AreEqual(null, book.DeweyDecimal);
        }

        [TestCase("1")]
        [TestCase("1.0")]
        public void WithDeweyDecimal_Test_CorrectFormat(string dewey)
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithDeweyDecimal(dewey)
                    .Get();

            // assert
            Assert.AreEqual(1m, book.DeweyDecimal);
        }

        [Test]
        public void WithDeweyDecimal_Test_IncorrectFormat()
        {
            string dewey = "bogus";
            Assert.Throws<FormatException>(() => BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithDeweyDecimal(dewey)
                    .Get());
        }

        [Test]
        public void InFormat_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .InFormat("Hardcover")
                    .Get();

            // assert
            Assert.AreEqual("Hardcover", book.Format);
        }

        [Test]
        public void PublishedIn_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .PublishedIn("2020")
                    .Get();

            // assert
            Assert.AreEqual("2020", book.DatePublished);
        }

        [Test]
        public void Edition_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .Edition("First Edition")
                    .Get();

            // assert
            Assert.AreEqual("First Edition", book.Edition);
        }

        [Test]
        public void Pages_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .Pages(100)
                    .Get();

            // assert
            Assert.AreEqual(100, book.Pages);
        }

        [Test]
        public void Sized_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .Sized("big")
                    .Get();

            // assert
            Assert.AreEqual("big", book.Dimensions);
        }

        [Test]
        public void WithOverview_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithOverview("overview")
                    .Get();

            // assert
            Assert.AreEqual("overview", book.Overview);
        }

        [Test]
        public void WithMsrp_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithMsrp("5.95")
                    .Get();

            // assert
            Assert.AreEqual("5.95", book.Msrp);
        }

        [Test]
        public void WithExcerpt_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithExcerpt("excerpt")
                    .Get();

            // assert
            Assert.AreEqual("excerpt", book.Excerpt);
        }

        [Test]
        public void WithSynopsys_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithSynopsys("synopsys")
                    .Get();

            // assert
            Assert.AreEqual("synopsys", book.Synopsys);
        }

        [Test]
        public void WrittenBy_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WrittenBy(new Author { FirstName="John", LastName="Smith"})
                    .Get();

            // assert
            Assert.IsTrue(book.Authors.Any(a => a.FirstName.Equals("John") || a.LastName.Equals("Smith")));
        }

        [Test]
        public void PublishedBy_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .PublishedBy(new Publisher { Name="some_publisher" })
                    .Get();

            // assert
            Assert.AreEqual("some_publisher", book.Publisher.Name);
        }

        [Test]
        public void PublishedAt_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .PublishedAt("AU")
                    .Get();

            // assert
            Assert.AreEqual("AU", book.PlaceOfPublication);
        }

        [Test]
        public void WrittenInLanguage_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WrittenInLanguage("English")
                    .Get();

            // assert
            Assert.AreEqual("English", book.Language);
        }

        [Test]
        public void WithAuthors_Test()
        {
            // arrange/act
            Book book = BookBuilder.CreateBook("test", "test book", null, "English", 100)
                .WithAuthors(new List<Author> { new Author { FirstName = "John", LastName = "Smith" },
                                                new Author { FirstName = "Jane", LastName = "Doe"} })
                    .Get();

            // assert
            Assert.IsTrue(book.Authors.Any(a => a.FirstName.Equals("John") || a.LastName.Equals("Smith")));
            Assert.IsTrue(book.Authors.Any(a => a.FirstName.Equals("Jane") || a.LastName.Equals("Doe")));
        }
    }//class
}

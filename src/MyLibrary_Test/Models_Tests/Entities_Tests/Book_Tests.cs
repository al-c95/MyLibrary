using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    public class Book_Tests
    {
        [Test]
        public void getIsbn_Test_isbn10()
        {
            // arrange
            Book testBook = new Book { Isbn = "0123456789", Isbn13 = "" };
            string expectedResult = "0123456789";

            // act
            string actualResult = testBook.GetIsbn();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void getIsbn_Test_isbn13()
        {
            // arrange
            Book testBook = new Book { Isbn13 = "0123456789012", Isbn = "" };
            string expectedResult = "0123456789012";

            // act
            string actualResult = testBook.GetIsbn();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void getIsbn_Test_both()
        {
            // arrange
            Book testBook = new Book { Isbn13 = "0123456789012", Isbn = "0123456789" };
            string expectedResult = "0123456789012";

            // act
            string actualResult = testBook.GetIsbn();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void getIsbn_Test_neither()
        {
            // arrange
            Book testBook = new Book();
            testBook.Isbn = ""; testBook.Isbn13 = "";
            string expectedResult = "";

            // act
            string actualResult = testBook.GetIsbn();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetAuthorList_Test_AuthorsExist()
        {
            // arrange
            Author author1 = new Author();
            author1.FirstName = "John";
            author1.LastName = "Smith";
            Author author2 = new Author();
            author2.FirstName = "Jane";
            author2.LastName = "Doe";
            Author author3 = new Author();
            author3.FirstName = "Korky";
            author3.LastName = "Buchek";
            Book book = new Book();
            book.Authors.Add(author1);
            book.Authors.Add(author2);
            book.Authors.Add(author3);
            string expectedResult = "Smith, J. and Doe, J. and Buchek, K.";

            // act
            var actualResult = book.GetAuthorList();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetAuthorList_Test_NoAuthors()
        {
            // arrange
            Book book = new Book();

            // act
            var result = book.GetAuthorList();

            // assert
            Assert.IsNull(book.GetAuthorList());
        }

        [TestCase("0123456789")]
        [TestCase("012345678X")]
        public void Isbn_Set_Test_Valid(string givenIsbn)
        {
            // arrange
            Book book = new Book();

            // act
            book.Isbn = givenIsbn;

            // assert
            Assert.AreEqual(givenIsbn, book.GetIsbn());
        }

        [TestCase("34hg5867g7")]
        [TestCase("012345678")]
        [TestCase("01234567891")]
        public void Isbn_Set_Test_Invalid(string givenIsbn)
        {
            // arrange
            Book book = new Book();

            // act/assert
            Assert.Throws<FormatException>(() => book.Isbn = givenIsbn);
        }

        [TestCase("0123456789012")]
        [TestCase("012345678901X")]
        public void Isbn13_Set_Test_Valid(string givenIsbn)
        {
            // arrange
            Book book = new Book();

            // act
            book.Isbn13 = givenIsbn;

            // assert
            Assert.AreEqual(givenIsbn, book.GetIsbn());
        }

        [TestCase("34hg5867g7")]
        [TestCase("012345678")]
        [TestCase("01234567891")]
        public void Isbn13_Set_Test_Invalid(string givenIsbn)
        {
            // arrange
            Book book = new Book();

            // act/assert
            Assert.Throws<FormatException>(() => book.Isbn13 = givenIsbn);
        }

        [Test]
        public void ToString_Test_HasDeweyDecimal()
        {
            // arrange
            Book item = new Book();
            item.Id = 1;
            item.Title = "Test item";
            item.TitleLong = "Test item: this item is a test";
            item.Isbn = "0123456789";
            item.Isbn13 = "0123456789123";
            item.DeweyDecimal = (decimal)500.0;
            item.Format = "print";
            item.Publisher = new Publisher { Name = "publisher" };
            item.DatePublished = "2021";
            item.PlaceOfPublication = "USA";
            item.Edition = "1st Edition";
            item.Pages = 100;
            item.Dimensions = "dim";
            item.Overview = "overview";
            item.Language = "English";
            item.Msrp = "5.95";
            item.Excerpt = "excerpt";
            item.Synopsys = "synopsys";
            item.Authors.Add(new Author { FirstName = "John", LastName = "Smith" });
            string expectedResult = "Id: " + "\r\n" +
                "1" + "\r\n" +
                "" + "\r\n" +
                "Title: " + "\r\n" +
                "Test item" + "\r\n" +
                "" + "\r\n" +
                "Type: " + "\r\n" +
                "Book" + "\r\n" +
                "" + "\r\n" +
                "ISBN: " + "\r\n" +
                "0123456789" + "\r\n" +
                "" + "\r\n" +
                "ISBN 13: " + "\r\n" +
                "0123456789123" + "\r\n" +
                "" + "\r\n" +
                "Long Title: " + "\r\n" +
                "Test item: this item is a test" + "\r\n" +
                "" + "\r\n" +
                "Dewey Decimal: " + "\r\n" +
                "500" + "\r\n" +
                "" + "\r\n" +
                "Format: " + "\r\n" +
                "print" + "\r\n" +
                "" + "\r\n" +
                "Publisher: " + "\r\n" +
                "publisher" + "\r\n" +
                "" + "\r\n" +
                "Date Published: " + "\r\n" +
                "2021" + "\r\n" +
                "" + "\r\n" +
                "Place of Publication: " + "\r\n" +
                "USA" + "\r\n" +
                "" + "\r\n" +
                "Edition: " + "\r\n" +
                "1st Edition" + "\r\n" +
                "" + "\r\n" +
                "Pages: " + "\r\n" +
                "100" + "\r\n" +
                "" + "\r\n" +
                "Dimensions: " + "\r\n" +
                "dim" + "\r\n" +
                "" + "\r\n" +
                "Overview: " + "\r\n" +
                "overview" + "\r\n" +
                "" + "\r\n" +
                "Language: " + "\r\n" +
                "English" + "\r\n" +
                "" + "\r\n" +
                "MSRP: " + "\r\n" +
                "5.95" + "\r\n" +
                "" + "\r\n" +
                "Excerpt: " + "\r\n" +
                "excerpt" + "\r\n" +
                "" + "\r\n" +
                "Synopsys: " + "\r\n" +
                "synopsys" + "\r\n" +
                "" + "\r\n" +
                "Authors: " + "\r\n" +
                "Smith, J.";

            // act
            string actualResult = item.ToString();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ToString_Test_NoDeweyDecimal()
        {
            // arrange
            Book item = new Book();
            item.Id = 1;
            item.Title = "Test item";
            item.TitleLong = "Test item: this item is a test";
            item.Isbn = "0123456789";
            item.Isbn13 = "0123456789123";
            item.Format = "print";
            item.Publisher = new Publisher { Name = "publisher" };
            item.DatePublished = "2021";
            item.PlaceOfPublication = "USA";
            item.Edition = "1st Edition";
            item.Pages = 100;
            item.Dimensions = "dim";
            item.Overview = "overview";
            item.Language = "English";
            item.Msrp = "5.95";
            item.Excerpt = "excerpt";
            item.Synopsys = "synopsys";
            item.Authors.Add(new Author { FirstName = "John", LastName = "Smith" });
            string expectedResult = "Id: " + "\r\n" +
                "1" + "\r\n" +
                "" + "\r\n" +
                "Title: " + "\r\n" +
                "Test item" + "\r\n" +
                "" + "\r\n" +
                "Type: " + "\r\n" +
                "Book" + "\r\n" +
                "" + "\r\n" +
                "ISBN: " + "\r\n" +
                "0123456789" + "\r\n" +
                "" + "\r\n" +
                "ISBN 13: " + "\r\n" +
                "0123456789123" + "\r\n" +
                "" + "\r\n" +
                "Long Title: " + "\r\n" +
                "Test item: this item is a test" + "\r\n" +
                "" + "\r\n" +
                "Dewey Decimal: " + "\r\n" +
                "" + "\r\n" +
                "" + "\r\n" +
                "Format: " + "\r\n" +
                "print" + "\r\n" +
                "" + "\r\n" +
                "Publisher: " + "\r\n" +
                "publisher" + "\r\n" +
                "" + "\r\n" +
                "Date Published: " + "\r\n" +
                "2021" + "\r\n" +
                "" + "\r\n" +
                "Place of Publication: " + "\r\n" +
                "USA" + "\r\n" +
                "" + "\r\n" +
                "Edition: " + "\r\n" +
                "1st Edition" + "\r\n" +
                "" + "\r\n" +
                "Pages: " + "\r\n" +
                "100" + "\r\n" +
                "" + "\r\n" +
                "Dimensions: " + "\r\n" +
                "dim" + "\r\n" +
                "" + "\r\n" +
                "Overview: " + "\r\n" +
                "overview" + "\r\n" +
                "" + "\r\n" +
                "Language: " + "\r\n" +
                "English" + "\r\n" +
                "" + "\r\n" +
                "MSRP: " + "\r\n" +
                "5.95" + "\r\n" +
                "" + "\r\n" +
                "Excerpt: " + "\r\n" +
                "excerpt" + "\r\n" +
                "" + "\r\n" +
                "Synopsys: " + "\r\n" +
                "synopsys" + "\r\n" +
                "" + "\r\n" +
                "Authors: " + "\r\n" +
                "Smith, J.";

            // act
            string actualResult = item.ToString();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }//class
}

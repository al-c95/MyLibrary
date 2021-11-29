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
            string expectedResult = "0123456789; 0123456789012";

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
    }
}

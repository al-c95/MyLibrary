using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    public class Author_Tests
    {
        [Test]
        public void GetFullName_Test()
        {
            // arrange
            Author testAuthor = new Author { FirstName = "John", LastName = "Smith" };
            string expectedResult = "John Smith";

            // act
            string actualResult = testAuthor.GetFullName();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetFullNameLastNameCommaFirstName_Test()
        {
            // arrange
            Author testAuthor = new Author { FirstName = "John", LastName = "Smith" };
            string expectedResult = "Smith, John";

            // act
            string actualResult = testAuthor.GetFullNameLastNameCommaFirstName();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void FirstName_setter_Test_noname()
        {
            // arrange
            Author testAuthor = new Author();

            // act/assert
            Assert.Throws<ArgumentNullException>(() => testAuthor.FirstName = "");
            Assert.Throws<ArgumentNullException>(() => testAuthor.FirstName = null);
        }

        [Test]
        public void LastName_setter_Test_noname()
        {
            // arrange
            Author testAuthor = new Author();

            // act/assert
            Assert.Throws<ArgumentNullException>(() => testAuthor.FirstName = "");
            Assert.Throws<ArgumentNullException>(() => testAuthor.FirstName = null);
        }
    }
}

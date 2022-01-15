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
        public void Author_Test_WithMiddleName()
        {
            // arrange
            string authorName = "John H. Smith";

            // act
            Author author = new Author(authorName);

            // assert
            Assert.AreEqual("John H.", author.FirstName);
            Assert.AreEqual("Smith", author.LastName);
        }

        [Test]
        public void Author_Test_NoMiddleName()
        {
            // arrange
            string authorName = "John Smith";

            // act
            Author author = new Author(authorName);

            // assert
            Assert.AreEqual("John", author.FirstName);
            Assert.AreEqual("Smith", author.LastName);
        }

        [Test]
        public void Author_Test_InvalidFormat()
        {
            // arrange
            string authorName = "JohnSmith";

            // act/assert
            Assert.Throws<ArgumentException>(() => new Author(authorName));
        }

        [TestCase("John", "Smith")]
        [TestCase("John H.", "Smith")]
        public void GetFullName_Test(string firstName, string lastName)
        {
            // arrange
            Author testAuthor = new Author { FirstName = firstName, LastName = lastName };
            string expectedResult = firstName + " " + lastName;

            // act
            string actualResult = testAuthor.GetFullName();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("John", "Smith")]
        [TestCase("John H.", "Smith")]
        public void GetFullNameLastNameCommaFirstName_Test(string firstName, string lastName)
        {
            // arrange
            Author testAuthor = new Author { FirstName = firstName, LastName = lastName };
            string expectedResult = lastName + ", " + firstName;

            // act
            string actualResult = testAuthor.GetFullNameLastNameCommaFirstName();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void FirstName_setter_Test_Valid()
        {
            // arrange
            Author author = new Author();
            string name = "John";

            // act
            author.FirstName = name;

            // assert
            Assert.AreEqual(name, author.FirstName);
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
        public void LastName_setter_Test_Valid()
        {
            // arrange
            Author author = new Author();
            string name = "Smith";

            // act
            author.LastName = name;

            // assert
            Assert.AreEqual(name, author.LastName);
        }

        [Test]
        public void LastName_setter_Test_noname()
        {
            // arrange
            Author testAuthor = new Author();

            // act/assert
            Assert.Throws<ArgumentNullException>(() => testAuthor.LastName = "");
            Assert.Throws<ArgumentNullException>(() => testAuthor.LastName = null);
        }

        [TestCase("John", "Smith")]
        [TestCase("John H.", "Smith")]
        public void GetFullNameWithFirstInitial(string firstName, string lastName)
        {
            // arrange
            Author testAuthor = new Author { FirstName = firstName, LastName = lastName };
            string expectedResult = lastName + ", J.";

            // act/assert
            Assert.AreEqual(expectedResult, testAuthor.GetFullNameWithFirstInitial());
        }

        [TestCase("John", "Smith")]
        [TestCase("John H.", "Smith")]
        [TestCase("John", "Smith-Jones")]
        public void SetFullNameFromCommaFormat(string firstName, string lastName)
        {
            // arrange
            Author author = new Author();
            string name = lastName + ", " + firstName;

            // act
            author.SetFullNameFromCommaFormat(name);

            // assert
            author.FirstName = firstName;
            author.LastName = lastName;
        }

        [Test]
        public void SetFullNameFromCommaFormat_Test_Invalid()
        {
            // arrange
            Author author = new Author();
            string name = "bogus";

            // act/assert
            Assert.Throws<ArgumentException>(() => author.SetFullNameFromCommaFormat(name));
        }
    }//class
}

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
        public void Author_Test_WithMiddleNameSimple()
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
        public void Author_Test_NoMiddleNameSimple()
        {
            // arrange
            string authorName = "John Smith";

            // act
            Author author = new Author(authorName);

            // assert
            Assert.AreEqual("John", author.FirstName);
            Assert.AreEqual("Smith", author.LastName);
        }

        [TestCase("JohnSmith")]
        [TestCase("John Smith1")]
        public void Author_Test_InvalidFormat(string authorName)
        {
            // act/assert
            Assert.Throws<ArgumentException>(() => new Author(authorName));
        }

        [TestCase("John", "Smith")]
        [TestCase("John H.", "Smith")]
        [TestCase("John", "de Coder")]
        [TestCase("John", "d'Coder")]
        [TestCase("John", "de Coder-Jones")]
        [TestCase("John", "d'Coder-Jones")]
        [TestCase("John H.", "de Coder")]
        [TestCase("John H.", "d'Coder")]
        [TestCase("John H.", "de Coder-Jones")]
        [TestCase("John H.", "d'Coder-Jones")]
        public void Author_Test_CompositeName(string firstName, string lastName)
        {
            // act
            Author author = new Author(firstName + " " + lastName);

            // assert
            Assert.AreEqual(firstName, author.FirstName);
            Assert.AreEqual(lastName, author.LastName);
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
        [TestCase("John", "de Coder")]
        [TestCase("John", "d'Coder")]
        [TestCase("John", "de Coder-Jones")]
        [TestCase("John", "d'Coder-Jones")]
        [TestCase("John H.", "de Coder")]
        [TestCase("John H.", "d'Coder")]
        [TestCase("John H.", "de Coder-Jones")]
        [TestCase("John H.", "d'Coder-Jones")]
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

        [TestCase("John")]
        [TestCase("John H.")]
        public void FirstName_setter_Test_Valid(string name)
        {
            // arrange
            Author author = new Author();

            // act
            author.FirstName = name;

            // assert
            Assert.AreEqual(name, author.FirstName);
        }

        [Test]
        public void FirstName_setter_Test_Invalid()
        {
            // arrange
            Author author = new Author();
            string name = "John1";

            // act/assert
            Assert.Throws<FormatException>(() => author.FirstName = name);
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

        [TestCase("Smith")]
        [TestCase("Smith-Jones")]
        [TestCase("de Coder")]
        [TestCase("d'Coder")]
        [TestCase("de Coder-Jones")]
        [TestCase("d'Coder-Jones")]
        public void LastName_setter_Test_Valid(string name)
        {
            // arrange
            Author author = new Author();

            // act
            author.LastName = name;

            // assert
            Assert.AreEqual(name, author.LastName);
        }

        [Test]
        public void LastName_setter_Test_Invalid()
        {
            // arrange
            Author author = new Author();
            string name = "Smith1";

            // act/assert
            Assert.Throws<FormatException>(() => author.LastName = name);
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
        [TestCase("John", "de Coder")]
        [TestCase("John", "d'Coder")]
        [TestCase("John", "de Coder-Jones")]
        [TestCase("John", "d'Coder-Jones")]
        [TestCase("John H.", "de Coder")]
        [TestCase("John H.", "d'Coder")]
        [TestCase("John H.", "de Coder-Jones")]
        [TestCase("John H.", "d'Coder-Jones")]
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
        [TestCase("John", "de Coder")]
        [TestCase("John", "d'Coder")]
        [TestCase("John", "de Coder-Jones")]
        [TestCase("John", "d'Coder-Jones")]
        [TestCase("John H.", "de Coder")]
        [TestCase("John H.", "d'Coder")]
        [TestCase("John H.", "de Coder-Jones")]
        [TestCase("John H.", "d'Coder-Jones")]
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

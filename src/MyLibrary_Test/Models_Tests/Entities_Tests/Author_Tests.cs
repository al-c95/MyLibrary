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

        [Test]
        public void Equals_Test_AreEqual()
        {
            string firstName = "John";
            string lastName = "Smith";
            Author author1 = new Author();
            author1.FirstName = firstName;
            author1.LastName = lastName;
            Author author2 = new Author();
            author2.FirstName = firstName;
            author2.LastName = lastName;

            bool result = author1.Equals(author2);

            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_Test_AreNotEqual()
        {
            Author author1 = new Author();
            author1.FirstName = "John";
            author1.LastName = "Smith";
            Author author2 = new Author();
            author2.FirstName = "Jane";
            author2.LastName = "Doe";
            int bogusAuthor = 0;

            bool result1 = author1.Equals(author2);
            bool result2 = author1.Equals(bogusAuthor);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }
    }//class
}

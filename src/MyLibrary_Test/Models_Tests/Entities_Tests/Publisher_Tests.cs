using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    class Publisher_Tests
    {
        [Test]
        public void Publisher_Test_ValidName()
        {
            // arrange
            string name = "some_publisher";
            Publisher publisher = new Publisher();

            // act
            publisher.Name = name;

            // assert
            Assert.AreEqual(name, publisher.Name);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Publisher_Test_EmptyName(string name)
        {
            // act/assert
            Assert.Throws<ArgumentNullException>(() => new Publisher(name));
        }

        [Test]
        public void Publisher_Name_Set_Test_Valid()
        {
            // arrange
            string name = "some_publisher";
            Publisher publisher = new Publisher();

            // act
            publisher.Name = name;

            // assert
            Assert.AreEqual(name, publisher.Name);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Publisher_Name_Set_Test_Empty(string name)
        {
            // act/assert
            Assert.Throws<ArgumentNullException>(() => new Publisher(name));
        }
    }//class
}

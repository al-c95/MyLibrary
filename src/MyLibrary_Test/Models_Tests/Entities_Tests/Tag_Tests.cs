using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    public class Tag_Tests
    {
        [Test]
        public void Name_setter_Test_noName()
        {
            // arrange
            Tag testTag = new Tag();

            // act/assert
            Assert.Throws<ArgumentNullException>(() => testTag.Name = "");
            Assert.Throws<ArgumentNullException>(() => testTag.Name = null);
        }

        [Test]
        public void Name_setter_Test_containsComma()
        {
            // arrange
            Tag testTag = new Tag();

            // act/assert
            Assert.Throws<ArgumentException>(() => testTag.Name = "Trains,railways");
            Assert.Throws<ArgumentException>(() => testTag.Name = "Trains, railways");
        }

        [TestCase("Trains and railways")]
        [TestCase("Trains;railways")]
        public void Name_setter_Test_validName(string name)
        {
            // arrange
            Tag testTag = new Tag();

            // act
            testTag.Name = name;

            // assert
            // no exceptions should be thrown here
            Assert.Pass();
        }
    }
}

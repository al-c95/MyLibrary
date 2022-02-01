using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    public class ItemBase_Tests
    {
        [Test]
        public void Title_Setter_Test_Valid()
        {
            // arrange
            MockItemBase item = new MockItemBase();
            string title = "item";

            // act
            item.Title = title;

            // assert
            Assert.AreEqual(title, item.Title);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Title_Setter_Test_Empty(string title)
        {
            // arrange
            MockItemBase item = new MockItemBase();

            // act/assert
            Assert.Throws<ArgumentNullException>(() => item.Title = title);
        }

        class MockItemBase : ItemBase
        {
            public override ItemType Type { get; set; }
        }//class
    }//class
}

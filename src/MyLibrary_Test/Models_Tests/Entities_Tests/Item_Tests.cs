using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    public class Item_Tests
    {
        [Test]
        public void GetCommaDelimitedTags_Test()
        {
            // arrange
            MockItem testItem = new MockItem();
            testItem.Tags = new List<Tag> { new Tag { Id = 1, Name = "History" },
                    new Tag { Id = 2, Name = "Trains and railways" },
                    new Tag { Id = 3, Name = "Fiction" } };
            string expectedResult = "History, Trains and railways, Fiction";

            // act
            string actualResult = testItem.GetCommaDelimitedTags();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockItem : Item
        {
            public override ItemType Type { get; set; }
        }
    }
}

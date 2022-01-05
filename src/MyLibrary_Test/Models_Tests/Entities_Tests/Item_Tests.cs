using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings;
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

        [Test]
        public void ToString_Test()
        {
            // arrange
            MockItem testItem = new MockItem();
            testItem.Id = 1;
            testItem.Title = "Test item";
            testItem.Type = ItemType.Book;
            string expectedResult = "Id: " + "\r\n" +
                "1" + "\r\n" +
                "" + "\r\n" +
                "Title: " + "\r\n" +
                "Test item" + "\r\n" +
                "" + "\r\n" +
                "Type: " + "\r\n" +
                "Book";

            // act
            string actualResult = testItem.ToString();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Title_Setter_Test_Valid()
        {
            // arrange
            MockItem item = new MockItem();
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
            MockItem item = new MockItem();

            // act/assert
            Assert.Throws<ArgumentNullException>(() => item.Title = title);
        }

        [Test]
        public void GetMemento_Test()
        {
            // arrange
            MockItem item = new MockItem
            {
                Notes = "test",
                Image = Encoding.ASCII.GetBytes("test")
            };

            // act
            ItemMemento memento = item.GetMemento();

            // assert
            Assert.AreEqual("test", memento.Notes);
            Assert.AreEqual(Encoding.ASCII.GetBytes("test"), memento.Image);
        }

        [Test]
        public void Restore_Test()
        {
            // arrange
            MockItem item = new MockItem();

            // act
            ItemMemento memento = new ItemMemento("test", Encoding.ASCII.GetBytes("test"));
            item.Restore(memento);

            // assert
            Assert.AreEqual("test", item.Notes);
            Assert.AreEqual(Encoding.ASCII.GetBytes("test"), item.Image);
        }

        [Test]
        public void Restore_Test_NullMemento()
        {
            // arrange
            MockItem item = new MockItem();

            // act
            ItemMemento memento = null;
            item.Restore(memento);

            // assert
            Assert.AreEqual(null, item.Notes);
            Assert.AreEqual(null, item.Image);
        }

        class MockItem : Item
        {
            public override ItemType Type { get; set; }
        }
    }//class
}

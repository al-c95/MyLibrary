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

        [TestCase("", false)]
        [TestCase("bogus", false)]
        [TestCase(@"C:\path\to\file.bmp", true)]
        [TestCase(@"C:\path\to\file.jpg", true)]
        [TestCase(@"C:\path\to\file.jpeg", true)]
        [TestCase(@"C:\path\to\file.png", true)]
        public void IsValidImageFileType_Test(string path, bool expectedResult)
        {
            // act
            bool actualResult = Item.IsValidImageFileType(path);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("Flash Drive", ItemType.FlashDrive)]
        [TestCase("Floppy Disk", ItemType.FloppyDisk)]
        [TestCase("Other", ItemType.Other)]
        [TestCase("Vhs", ItemType.Vhs)]
        [TestCase("Vinyl", ItemType.Vinyl)]
        [TestCase("BluRay", ItemType.BluRay)]
        [TestCase("Dvd", ItemType.Dvd)]
        [TestCase("Cd", ItemType.Cd)]
        [TestCase("Book", ItemType.Book)]
        public void ParseType_Test_ParseSuccessful(string type, ItemType expectedResult)
        {
            // act
            ItemType actualResult = Item.ParseType(type);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ParseType_Test_Invalid()
        {
            // arrange
            string type = "bogus_type";

            // act/assert
            Assert.Throws<FormatException>(() => Item.ParseType(type));
        }

        [TestCase(ItemType.FlashDrive, "Flash Drive")]
        [TestCase(ItemType.FloppyDisk, "Floppy Disk")]
        [TestCase(ItemType.Other, "Other")]
        [TestCase(ItemType.Vhs, "Vhs")]
        [TestCase(ItemType.Vinyl, "Vinyl")]
        [TestCase(ItemType.BluRay, "BluRay")]
        [TestCase(ItemType.Dvd, "Dvd")]
        [TestCase(ItemType.Cd, "Cd")]
        [TestCase(ItemType.Book, "Book")]
        public void GetTypeString_Test(ItemType input, string expectedResult)
        {
            // act
            string actualResult = Item.GetTypeString(input);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockItem : Item
        {
            public override ItemType Type { get; set; }
        }
    }//class
}

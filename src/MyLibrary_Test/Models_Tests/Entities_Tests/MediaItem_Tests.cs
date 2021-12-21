using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    class MediaItem_Tests
    {
        [Test]
        public void ItemType_Setter_Test_Incorrect()
        {
            // arrange
            MediaItem item = new MediaItem();

            // act/assert
            Assert.Throws<InvalidOperationException>(() => item.Type = ItemType.Book);
        }

        [TestCase(ItemType.Cd)]
        public void ItemType_Setter_Test_Correct(ItemType type)
        {
            // arrange
            MediaItem item = new MediaItem();

            // act/assert
            Assert.DoesNotThrow(() => item.Type = type);
        }

        [Test]
        public void ToString_Test_WithRunningTime()
        {
            // arrange
            MediaItem item = new MediaItem();
            item.Id = 1;
            item.Title = "Test item";
            item.Type = ItemType.Dvd;
            item.Number = 123;
            item.RunningTime = 60;
            item.ReleaseYear = 1999;
            string expectedResult = "Id: " + "\r\n" +
                "1" + "\r\n" +
                "" + "\r\n" +
                "Title: " + "\r\n" +
                "Test item" + "\r\n" +
                "" + "\r\n" +
                "Type: " + "\r\n" +
                "Dvd" + "\r\n" +
                "" + "\r\n" +
                "Number: " + "\r\n" +
                "123" + "\r\n" +
                "" + "\r\n" +
                "Running Time: " + "\r\n" +
                "60" + "\r\n" +
                "" + "\r\n" +
                "Release Year: " + "\r\n" +
                "1999";

            // act
            string actualResult = item.ToString();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ToString_Test_NoRunningTime()
        {
            // arrange
            MediaItem item = new MediaItem();
            item.Id = 1;
            item.Title = "Test item";
            item.Type = ItemType.Dvd;
            item.Number = 123;
            item.ReleaseYear = 1999;
            string expectedResult = "Id: " + "\r\n" +
                "1" + "\r\n" +
                "" + "\r\n" +
                "Title: " + "\r\n" +
                "Test item" + "\r\n" +
                "" + "\r\n" +
                "Type: " + "\r\n" +
                "Dvd" + "\r\n" +
                "" + "\r\n" +
                "Number: " + "\r\n" +
                "123" + "\r\n" +
                "" + "\r\n" +
                "Running Time: " + "\r\n" +
                "" + "\r\n" +
                "" + "\r\n" +
                "Release Year: " + "\r\n" +
                "1999";

            // act
            string actualResult = item.ToString();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }//class
}

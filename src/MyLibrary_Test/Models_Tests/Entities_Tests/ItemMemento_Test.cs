using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    class ItemMemento_Test
    {
        [Test]
        public void ItemMemento_Test_ctor()
        {
            // arrange
            string notes = "test";
            byte[] bytes = Encoding.ASCII.GetBytes("test");

            // act
            ItemMemento memento = new ItemMemento(notes, bytes);

            // assert
            Assert.AreEqual(notes, memento.Notes);
            Assert.AreEqual(bytes, memento.Image);
        }
    }//class
}

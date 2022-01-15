using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    class ItemTagsDto_Test
    {
        [Test]
        public void ItemTagsDto_constructor_Test()
        {
            // arrange
            List<string> originalTags = new List<string>
            {
                "Software",
                "History",
                "Science"
            };
            List<string> selectedTags = new List<string>
            {
                "History",
                "Physics"
            };

            // act
            ItemTagsDto dto = new ItemTagsDto(1, originalTags, selectedTags);

            // assert
            Assert.AreEqual(1, dto.TagsToAdd.Count());
            Assert.IsTrue(dto.TagsToAdd.Any(t => t.Equals("Physics")));
            Assert.AreEqual(2, dto.TagsToRemove.Count());
            Assert.IsTrue(dto.TagsToRemove.Any(t => t.Equals("Software")));
            Assert.IsTrue(dto.TagsToRemove.Any(t => t.Equals("Science")));
        }
    }//class
}

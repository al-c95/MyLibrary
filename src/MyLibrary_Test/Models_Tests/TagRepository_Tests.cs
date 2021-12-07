using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary_Test.Models_Tests
{
    [TestFixture]
    public class TagRepository_Tests
    {
        private TagRepository _repo;

        public TagRepository_Tests()
        {
            // arrange
            var fakeDao = A.Fake<ITagDataAccessor>();
            List<Tag> tags = new List<Tag>
            {
                new Tag
                {
                    Name = "tag1"
                },
                new Tag
                {
                    Name = "tag2"
                }
            };
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(tags);
            this._repo = new TagRepository(fakeDao);
        }

        [Test]
        public async Task GetAll_Test()
        {
            // act
            var result = await _repo.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 2);
        }

        [Test]
        public async Task GetByName_Test_Exists()
        {
            // act
            var result = await _repo.TagWithNameExists("tag1");

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetByName_Test_DoesNotExist()
        {
            // act
            var result = await _repo.TagWithNameExists("tag3");

            // assert
            Assert.IsFalse(result);
        }
    }//class
}

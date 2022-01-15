using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    public class TagService_Tests
    {
        [TestCase("tag1", true)]
        [TestCase("bogus", false)]
        public async Task ExistsWithName_Test(string name, bool expectedResult)
        {
            // arrange
            MockTagService service = new MockTagService();

            // act
            bool actualResult = await service.ExistsWithName(name);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockTagService : TagService
        {
            public override async Task<IEnumerable<Tag>> GetAll()
            {
                List<Tag> tags = new List<Tag>();
                await Task.Run(() =>
                {
                    tags.Add(new Tag
                    {
                        Name="tag1"
                    });

                    tags.Add(new Tag 
                    { 
                        Name="tag2"
                    });
                });

                return tags;
            }//GetAll
        }//class
    }//class
}

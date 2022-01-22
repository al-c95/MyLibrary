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
    public class MediaItemCopyService_Tests
    {
        [TestCase("copy1", true)]
        [TestCase("copy2", false)]
        public async Task ExistsWithDescription_Test(string description, bool expectedResult)
        {
            // arrange
            MockService service = new MockService();

            // act
            bool actualResult = await service.ExistsWithDescription(description);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockService : MediaItemCopyService
        {
            public async override Task<IEnumerable<MediaItemCopy>> GetAll()
            {
                List<MediaItemCopy> copies = new List<MediaItemCopy>();
                await Task.Run(() =>
                {
                    copies.Add(new MediaItemCopy
                    {
                        Description = "copy1"
                    });
                });

                return copies;
            }
        }//class
    }//class
}

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
    public class BookCopyService_Tests
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

        class MockService : BookCopyService
        {
            public async override Task<IEnumerable<BookCopy>> GetAll()
            {
                List<BookCopy> copies = new List<BookCopy>();
                await Task.Run(() =>
                {
                    copies.Add(new BookCopy
                    {
                        Description="copy1"
                    });
                });

                return copies;
            }
        }//class
    }//class
}

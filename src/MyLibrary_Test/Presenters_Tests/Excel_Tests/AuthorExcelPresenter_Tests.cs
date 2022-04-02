using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using OfficeOpenXml;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Views.Excel;
using MyLibrary.Presenters.Excel;

namespace MyLibrary_Test.Presenters_Tests.Excel_Tests
{
    [TestFixture]
    class AuthorExcelPresenter_Tests
    {
        [Test]
        public async Task HandleStartButtonClicked_Test_Success()
        {
            // arrange
            var fakeService = A.Fake<IAuthorService>();
            Author author1 = new Author { Id=1, FirstName = "John", LastName = "Smith" };
            Author author2 = new Author { Id=2, FirstName = "Jane", LastName = "Doe" };
            List<Author> authors = new List<Author>
            {
                author1,
                author2
            };
            A.CallTo(() => fakeService.GetAll()).Returns(authors);
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            MockPresenter presenter = new MockPresenter(fakeService, fakeExcelFile, fakeDialog);

            // act
            await presenter.HandleStartButtonClicked(null, null);

            // assert
            Assert.AreEqual("Task complete.", fakeDialog.Label1);
            Assert.AreEqual("2 rows exported", fakeDialog.Label2);
        }

        class MockPresenter : AuthorExcelPresenter
        {
            public MockPresenter(IAuthorService authorService, IExcelFile file, IExportDialog dialog)
                :base(authorService, file, dialog)
            {

            }
        }//class
    }//class
}
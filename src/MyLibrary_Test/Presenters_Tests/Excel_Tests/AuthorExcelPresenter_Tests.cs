using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary;
using MyLibrary.Models.Entities;
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
            List<Author> authors = new List<Author>
            {
                author1
            };
            A.CallTo(() => fakeService.GetAll()).Returns(authors);
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            var excel = new Excel();
            MockPresenter presenter = new MockPresenter(fakeService, fakeExcelFile, fakeDialog, excel);

            // act
            await presenter.HandleStartButtonClicked(null, null);

            // assert
            // dialog labels
            Assert.AreEqual("Task complete.", fakeDialog.Label1);
            Assert.AreEqual("1 rows exported", fakeDialog.Label2);
            // worksheet
            Assert.AreEqual("MyLibrary", excel.Worksheet.Cells["A1"].GetValue<string>());       
            Assert.AreEqual("Type", excel.Worksheet.Cells["A2"].GetValue<string>());
            Assert.AreEqual("Authors", excel.Worksheet.Cells["B2"].GetValue<string>());
            Assert.AreEqual("App Version:", excel.Worksheet.Cells["A3"].GetValue<string>());
            Assert.AreEqual("Extracted At:", excel.Worksheet.Cells["A4"].GetValue<string>());
            Assert.AreEqual("Id", excel.Worksheet.Cells["A6"].GetValue<string>());
            Assert.AreEqual("First Name", excel.Worksheet.Cells["B6"].GetValue<string>());
            Assert.AreEqual("Last Name", excel.Worksheet.Cells["C6"].GetValue<string>());
            Assert.AreEqual(1, excel.Worksheet.Cells["A7"].GetValue<int>());
            Assert.AreEqual("John", excel.Worksheet.Cells["B7"].GetValue<string>());
            Assert.AreEqual("Smith", excel.Worksheet.Cells["C7"].GetValue<string>());       
        }

        class MockPresenter : AuthorExcelPresenter
        {
            public MockPresenter(IAuthorService authorService, IExcelFile file, IExportDialog dialog, MyLibrary.Views.Excel.Excel excel)
                :base(authorService, file, dialog, excel)
            {

            }
        }//class
    }//class
}
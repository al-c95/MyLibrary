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
using MyLibrary.Models.Entities.Builders;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Views.Excel;
using MyLibrary.Presenters.Excel;

namespace MyLibrary_Test.Presenters_Tests.Excel_Tests
{
    [TestFixture]
    class BookExcelPresenter_Tests
    {
        [Test]
        public async Task HandleStartButtonClicked_Test_Success()
        {
            // arrange
            var fakeService = A.Fake<IBookService>();
            Author author = new Author { FirstName = "John", LastName = "Smith" };
            Tag tag = new Tag { Name = "test" };
            Book item1 = BookBuilder.CreateBook("book1", "book1: this is a test", new Publisher { Name = "some_publisher" }, "English", 100)
                .WithIsbn("0123456789")
                .WithIsbn13("")
                .WithAuthors(new List<Author> { author })
                .WrittenInLanguage("English")
                .WithTags(new List<Tag> { tag })
                .WithDeweyDecimal("1.2")
                .WithMsrp("")
                .InFormat("")
                .PublishedIn("2022")
                .PublishedAt("Australia")
                .Edition("")
                .WithOverview("")
                .WithExcerpt("")
                .WithSynopsys("")
                    .Get();
            item1.Dimensions = "";
            item1.Id = 1;
            item1.Notes = "";
            List<Book> items = new List<Book>
            {
                item1
            };
            A.CallTo(() => fakeService.GetAllAsync()).Returns(items);
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            var excel = new MyLibrary.Views.Excel.Excel();
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
            Assert.AreEqual("Books", excel.Worksheet.Cells["B2"].GetValue<string>());
            Assert.AreEqual("App Version:", excel.Worksheet.Cells["A3"].GetValue<string>());
            Assert.AreEqual("Extracted At:", excel.Worksheet.Cells["A4"].GetValue<string>());
            Assert.AreEqual("Id", excel.Worksheet.Cells["A6"].GetValue<string>());
            Assert.AreEqual("Title", excel.Worksheet.Cells["B6"].GetValue<string>());
            Assert.AreEqual("Long Title", excel.Worksheet.Cells["C6"].GetValue<string>());
            Assert.AreEqual("ISBN", excel.Worksheet.Cells["D6"].GetValue<string>());
            Assert.AreEqual("ISBN13", excel.Worksheet.Cells["E6"].GetValue<string>());
            Assert.AreEqual("Authors", excel.Worksheet.Cells["F6"].GetValue<string>());
            Assert.AreEqual("Language", excel.Worksheet.Cells["G6"].GetValue<string>());
            Assert.AreEqual("Tags", excel.Worksheet.Cells["H6"].GetValue<string>());
            Assert.AreEqual("Dewey Decimal", excel.Worksheet.Cells["I6"].GetValue<string>());
            Assert.AreEqual("MSRP", excel.Worksheet.Cells["J6"].GetValue<string>());
            Assert.AreEqual("Publisher", excel.Worksheet.Cells["K6"].GetValue<string>());
            Assert.AreEqual("Format", excel.Worksheet.Cells["L6"].GetValue<string>());
            Assert.AreEqual("Date Published", excel.Worksheet.Cells["M6"].GetValue<string>());
            Assert.AreEqual("Place of Publication", excel.Worksheet.Cells["N6"].GetValue<string>());
            Assert.AreEqual("Edition", excel.Worksheet.Cells["O6"].GetValue<string>());
            Assert.AreEqual("Pages", excel.Worksheet.Cells["P6"].GetValue<string>());
            Assert.AreEqual("Dimensions", excel.Worksheet.Cells["Q6"].GetValue<string>());
            Assert.AreEqual("Overview", excel.Worksheet.Cells["R6"].GetValue<string>());
            Assert.AreEqual("Excerpt", excel.Worksheet.Cells["S6"].GetValue<string>());
            Assert.AreEqual("Synopsys", excel.Worksheet.Cells["T6"].GetValue<string>());
            Assert.AreEqual("Notes", excel.Worksheet.Cells["U6"].GetValue<string>());
            Assert.AreEqual(1, excel.Worksheet.Cells["A7"].GetValue<int>());
            Assert.AreEqual("book1", excel.Worksheet.Cells["B7"].GetValue<string>());
            Assert.AreEqual("book1: this is a test", excel.Worksheet.Cells["C7"].GetValue<string>());
            Assert.AreEqual("0123456789", excel.Worksheet.Cells["D7"].GetValue<string>());
            Assert.AreEqual("", excel.Worksheet.Cells["E7"].GetValue<string>());
            Assert.AreEqual("John Smith", excel.Worksheet.Cells["F7"].GetValue<string>());
            Assert.AreEqual("English", excel.Worksheet.Cells["G7"].GetValue<string>());
            Assert.AreEqual("test", excel.Worksheet.Cells["H7"].GetValue<string>());
            Assert.AreEqual("1.2", excel.Worksheet.Cells["I7"].GetValue<string>());
            Assert.AreEqual("", excel.Worksheet.Cells["J7"].GetValue<string>());
            Assert.AreEqual("some_publisher", excel.Worksheet.Cells["K7"].GetValue<string>());
            Assert.AreEqual("", excel.Worksheet.Cells["L7"].GetValue<string>());
            Assert.AreEqual(2022, excel.Worksheet.Cells["M7"].GetValue<int>());
            Assert.AreEqual("Australia", excel.Worksheet.Cells["N7"].GetValue<string>());
            Assert.AreEqual("", excel.Worksheet.Cells["O7"].GetValue<string>());
            Assert.AreEqual(100, excel.Worksheet.Cells["P7"].GetValue<int>());
            Assert.AreEqual("", excel.Worksheet.Cells["Q7"].GetValue<string>());
            Assert.AreEqual("", excel.Worksheet.Cells["R7"].GetValue<string>());
            Assert.AreEqual("", excel.Worksheet.Cells["S7"].GetValue<string>());
            Assert.AreEqual("", excel.Worksheet.Cells["T7"].GetValue<string>());
            Assert.AreEqual("", excel.Worksheet.Cells["U7"].GetValue<string>());
        }

        class MockPresenter : BookExcelPresenter
        {
            public MockPresenter(IBookService bookService, IExcelFile file, IExportDialog dialog, MyLibrary.Views.Excel.Excel excel)
                : base(bookService, file, dialog, excel)
            {

            }
        }//class
    }//class
}

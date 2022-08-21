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
using MyLibrary;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.BusinessLogic.ImportExcel;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.Utils;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class ExcelImportPresenter_Tests
    {
        [Test]
        public void HandleBrowseButtonClicked_Test_ExcelFile()
        {
            // arrange
            var fakeDialog = A.Fake<IExcelImportDialog>();
            string filePath = @"C:\path\to\my\file.xlsx";
            A.CallTo(() => fakeDialog.ShowFileBrowserDialog()).Returns(filePath);
            ExcelImportPresenter presenter = new ExcelImportPresenter(fakeDialog);

            // act
            presenter.HandleBrowseButtonClicked(null, null);

            // assert
            Assert.IsTrue(fakeDialog.StartButtonEnabled);
            Assert.AreEqual(filePath, fakeDialog.FileFieldText);
        }

        [Test]
        public void HandleBrowseButtonClicked_Test_NonExcelFile()
        {
            // arrange
            var fakeDialog = A.Fake<IExcelImportDialog>();
            string filePath = @"C:\path\to\my\file.docx";
            A.CallTo(() => fakeDialog.ShowFileBrowserDialog()).Returns(filePath);
            ExcelImportPresenter presenter = new ExcelImportPresenter(fakeDialog);

            // act
            presenter.HandleBrowseButtonClicked(null, null);

            // assert
            Assert.IsFalse(fakeDialog.StartButtonEnabled);
            Assert.AreEqual(filePath, fakeDialog.FileFieldText);
        }

        [TestCase("", false)]
        [TestCase(@"C:\path\to\my\file.docx", false)]
        [TestCase(@"C:\path\to\my\file.xlsx", true)]
        public void HandleFileFieldTextChanged_Test(string entry, bool expectedStartButtonEnabled)
        {
            // arrange
            var fakeDialog = A.Fake<IExcelImportDialog>();
            A.CallTo(() => fakeDialog.FileFieldText).Returns(entry);
            ExcelImportPresenter presenter = new ExcelImportPresenter(fakeDialog);

            // act
            presenter.HandleFileFieldTextChanged(null, null);

            // assert
            Assert.AreEqual(expectedStartButtonEnabled, fakeDialog.StartButtonEnabled);
        }
    }
}

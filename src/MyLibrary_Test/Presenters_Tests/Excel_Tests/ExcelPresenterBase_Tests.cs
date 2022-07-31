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
    class ExcelPresenterBase_Tests
    {
        [Test]
        public void BrowseButtonClicked_Test_EmptyPath()
        {
            // arrange
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            A.CallTo(() => fakeDialog.ShowFolderBrowserDialog("test")).Returns(null);
            CancellationTokenSource cts = new CancellationTokenSource();
            MockPresenter presenter = new MockPresenter("test", fakeExcelFile, fakeDialog, cts);

            // act
            presenter.BrowseButtonClicked(null, null);

            // assert
            Assert.IsFalse(fakeDialog.StartButtonEnabled);
            Assert.AreEqual(string.Empty, fakeDialog.Path);
        }

        [Test]
        public void BrowseButtonClicked_Test_GivenPath()
        {
            // arrange
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            string path = @"C:\path\to\my\file.xlsx";
            A.CallTo(() => fakeDialog.ShowFolderBrowserDialog("test")).Returns(path);
            CancellationTokenSource cts = new CancellationTokenSource();
            MockPresenter presenter = new MockPresenter("test", fakeExcelFile, fakeDialog, cts);

            // act
            presenter.BrowseButtonClicked(null, null);

            // assert
            Assert.IsTrue(fakeDialog.StartButtonEnabled);
            Assert.AreEqual(path, fakeDialog.Path);
        }

        public class MockPresenter : ExcelPresenterBase
        {
            public MockPresenter(string type, IExcelFile file, IExportDialog dialog, CancellationTokenSource cts)
                :base(type,file,dialog)
            {
                this._cts = cts;
            }

            protected override Task RenderExcel(IProgress<int> numberExported, CancellationToken token)
            {
                throw new NotImplementedException();
            }

            protected override void WriteHeaders()
            {
                throw new NotImplementedException();
            }
        }//class
    }//class
}

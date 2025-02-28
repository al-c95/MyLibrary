﻿using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeItEasy;
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
            MockPresenter presenter = new MockPresenter("test", fakeExcelFile, fakeDialog);
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
            MockPresenter presenter = new MockPresenter("test", fakeExcelFile, fakeDialog);

            // act
            presenter.BrowseButtonClicked(null, null);

            // assert
            Assert.IsTrue(fakeDialog.StartButtonEnabled);
            Assert.AreEqual(path, fakeDialog.Path);
        }

        [Test]
        public async Task HandleStartButtonClicked_Test_Error()
        {
            // arrange
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            string path = @"C:\path\to\my\file.xlsx";
            A.CallTo(() => fakeDialog.ShowFolderBrowserDialog("test")).Returns(path);
            MockPresenter presenter = new MockPresenter("test", fakeExcelFile, fakeDialog);

            // act
            await presenter.HandleStartButtonClicked(null, null);

            // assert
            Assert.AreEqual("Task aborted.", fakeDialog.Label1);
            Assert.AreEqual("", fakeDialog.Label2);
            A.CallTo(() => fakeDialog.ShowErrorDialog("error")).MustHaveHappened();
        }

        public class MockPresenter : ExcelPresenterBase
        {
            public MockPresenter(string type, IExcelFile file, IExportDialog dialog)
                : base(type, file, dialog, new MyLibrary.Views.Excel.Excel())
            {

            }

            protected override Task RenderExcel(IProgress<int> numberExported)
            {
                throw new Exception("error");
            }

            protected override void WriteHeaders()
            {
                throw new NotImplementedException();
            }
        }//class

        public class MockPresenter2 : ExcelPresenterBase
        {
            public MockPresenter2(string type, IExcelFile file, IExportDialog dialog, CancellationTokenSource cts)
                : base(type, file, dialog, new MyLibrary.Views.Excel.Excel())
            {

            }

            protected override Task RenderExcel(IProgress<int> numberExported)
            {
                throw new OperationCanceledException("operation cancelled");
            }

            protected override void WriteHeaders()
            {
                throw new NotImplementedException();
            }
        }//class
    }//class
}

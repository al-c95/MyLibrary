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
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.ApiService;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Presenters.ServiceProviders;
namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    public class ManageCopiesPresenter_Tests
    {
        [Test]
        public async Task LoadData_Test_Book()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            Book book = new Book { Id = 1 };
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            var fakeCopyService = A.Fake<IBookCopyService>();
            List<BookCopy> copies = new List<BookCopy>
            {
                new BookCopy{Id=1, BookId=1}
            };
            A.CallTo(() => fakeCopyService.GetByItemId(1)).Returns(copies);
            A.CallTo(() => fakeCopyServiceFactory.GetBookCopyService()).Returns(fakeCopyService);
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, book, fakeCopyServiceFactory);

            // act
            await presenter.LoadData(null, null);

            // assert
            A.CallTo(() => fakeView.DisplayCopies(copies)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task LoadData_Test_MediaItem()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            MediaItem item = new MediaItem { Id = 1 };
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            var fakeCopyService = A.Fake<IMediaItemCopyService>();
            List<MediaItemCopy> copies = new List<MediaItemCopy>
            {
                new MediaItemCopy{Id=1, MediaItemId=1}
            };
            A.CallTo(() => fakeCopyService.GetByItemId(1)).Returns(copies);
            A.CallTo(() => fakeCopyServiceFactory.GetMediaItemCopyService()).Returns(fakeCopyService);
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);

            // act
            await presenter.LoadData(null, null);

            // assert
            A.CallTo(() => fakeView.DisplayCopies(copies)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public void CopySelected_Test_Selected()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            Copy copy = new BookCopy { Id = 1, Description = "test copy", Notes = "test copy" };
            A.CallTo(() => fakeView.NumberCopiesSelected).Returns(1);
            A.CallTo(() => fakeView.SelectedCopy).Returns(copy);
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            Item item = new Book { Id = 1, Title = "item" };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);

            // act
            presenter.CopySelected(null, null);

            // assert
            Assert.AreEqual("test copy", fakeView.SelectedDescription);
            Assert.AreEqual("test copy", fakeView.SelectedNotes);
            Assert.IsTrue(fakeView.DeleteSelectedButtonEnabled);
            Assert.IsTrue(fakeView.SelectedDescriptionFieldEnabled);
            Assert.IsTrue(fakeView.SelectedNotesFieldEnabled);
        }

        [Test]
        public void CopySelected_Test_NoSelection()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            A.CallTo(() => fakeView.NumberCopiesSelected).Returns(0);
            A.CallTo(() => fakeView.SelectedCopy).Returns(null);
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            Item item = new Book { Id = 1, Title = "item" };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);

            // act
            presenter.CopySelected(null, null);

            // assert
            Assert.AreEqual("", fakeView.SelectedDescription);
            Assert.AreEqual("", fakeView.SelectedNotes);
            Assert.IsFalse(fakeView.DeleteSelectedButtonEnabled);
            Assert.IsFalse(fakeView.SelectedDescriptionFieldEnabled);
            Assert.IsFalse(fakeView.SelectedNotesFieldEnabled);
        }

        [Test]
        public void DiscardChangesClicked_Test()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            fakeView.SelectedDescription = "some description";
            fakeView.SelectedNotes = "some notes";
            Copy copy = new BookCopy { Id = 1, Description = "test copy", Notes = "test copy" };
            A.CallTo(() => fakeView.SelectedCopy).Returns(copy);
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            Item item = new Book { Id = 1, Title = "item" };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);

            // act
            presenter.DiscardChangesClicked(null, null);

            // assert
            Assert.AreEqual("test copy", fakeView.SelectedDescription);
            Assert.AreEqual("test copy", fakeView.SelectedNotes);
        }

        [Test]
        public void NewCopyFieldsUpdated_Test_DescriptionProvided()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            A.CallTo(() => fakeView.NewDescription).Returns("description");
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            Item item = new Book { Id = 1, Title = "item" };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);

            // act
            presenter.NewCopyFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveNewButtonEnabled);
        }

        [Test]
        public void NewCopyFieldsUpdated_Test_DescriptionEmpty()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            A.CallTo(() => fakeView.NewDescription).Returns("");
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            Item item = new Book { Id = 1, Title = "item" };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);

            // act
            presenter.NewCopyFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveNewButtonEnabled);
        }

        [TestCase("description", true)]
        [TestCase("", false)]
        public void SelectedCopyFieldsUpdated_Test_CopySelected(string selectedCopyDescriptionFieldEntry, bool expectedButtonsEnabled)
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            A.CallTo(() => fakeView.NumberCopiesSelected).Returns(1);
            A.CallTo(() => fakeView.SelectedDescription).Returns(selectedCopyDescriptionFieldEntry);
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            Item item = new Book { Id = 1, Title = "item" };
            BookCopy copy = new BookCopy { Id = 1, BookId = 1 };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);

            // act
            presenter.SelectedCopyFieldsUpdated(null, null);

            // assert
            Assert.AreEqual(expectedButtonsEnabled, fakeView.SaveSelectedButtonEnabled);
            Assert.AreEqual(expectedButtonsEnabled, fakeView.DiscardChangesButtonEnabled);
        }

        [Test]
        public void SelectedCopyFieldsUpdated_Test_NoCopySelected()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            A.CallTo(() => fakeView.NumberCopiesSelected).Returns(0);
            A.CallTo(() => fakeView.SelectedDescription).Returns("");
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            Item item = new Book { Id = 1, Title = "item" };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);

            // act
            presenter.SelectedCopyFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveSelectedButtonEnabled);
            Assert.IsFalse(fakeView.DiscardChangesButtonEnabled);
        }

        [Test]
        public async Task HandleSaveSelectedClicked_Test_Book()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            A.CallTo(() => fakeView.SelectedDescription).Returns("description");
            A.CallTo(() => fakeView.SelectedNotes).Returns("notes");
            BookCopy copy = new BookCopy { Id = 1, BookId = 1, Description = fakeView.SelectedDescription, Notes = fakeView.SelectedNotes };
            A.CallTo(() => fakeView.ModifiedSelectedCopy).Returns(copy);
            Book item = new Book { Id = 1, Title = "item" };
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            var fakeCopyService = A.Fake<IBookCopyService>();
            A.CallTo(() => fakeCopyServiceFactory.GetBookCopyService()).Returns(fakeCopyService);
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);
            List<BookCopy> copies = new List<BookCopy>
            {
                copy
            };
            A.CallTo(() => fakeCopyService.GetByItemId(1)).Returns(copies);

            // act
            await presenter.HandleSaveSelectedClicked(null, null);

            // assert
            A.CallTo(() => fakeView.DisplayCopies(copies)).MustHaveHappened();
            A.CallTo(() => fakeCopyService.Update(copy)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task HandleSaveSelectedClicked_Test_MediaItem()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            A.CallTo(() => fakeView.SelectedDescription).Returns("description");
            A.CallTo(() => fakeView.SelectedNotes).Returns("notes");
            MediaItemCopy copy = new MediaItemCopy { Id = 1, MediaItemId = 1, Description = fakeView.SelectedDescription, Notes = fakeView.SelectedNotes };
            A.CallTo(() => fakeView.ModifiedSelectedCopy).Returns(copy);
            MediaItem item = new MediaItem{ Id = 1, Title = "item" };
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            var fakeCopyService = A.Fake<IMediaItemCopyService>();
            A.CallTo(() => fakeCopyServiceFactory.GetMediaItemCopyService()).Returns(fakeCopyService);
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);
            List<MediaItemCopy> copies = new List<MediaItemCopy>
            {
                copy
            };
            A.CallTo(() => fakeCopyService.GetByItemId(1)).Returns(copies);

            // act
            await presenter.HandleSaveSelectedClicked(null, null);

            // assert
            A.CallTo(() => fakeView.DisplayCopies(copies)).MustHaveHappened();
            A.CallTo(() => fakeCopyService.Update(copy)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task HandleDeleteClicked_Test_Book()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            BookCopy copy = new BookCopy { Id = 1 };
            A.CallTo(() => fakeView.SelectedCopy).Returns(copy);
            Book item = new Book { Id = 1, Title = "item" };
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            var fakeCopyService = A.Fake<IBookCopyService>();
            A.CallTo(() => fakeCopyServiceFactory.GetBookCopyService()).Returns(fakeCopyService);
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);
            List<BookCopy> copies = new List<BookCopy>
            {
                copy
            };
            A.CallTo(() => fakeCopyService.GetByItemId(1)).Returns(copies);

            // act
            await presenter.HandleDeleteClicked(null, null);

            // assert
            A.CallTo(() => fakeView.DisplayCopies(copies)).MustHaveHappened();
            A.CallTo(() => fakeCopyService.DeleteById(1)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task HandleDeleteClicked_Test_MediaItem()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            MediaItemCopy copy = new MediaItemCopy { Id = 1 };
            A.CallTo(() => fakeView.SelectedCopy).Returns(copy);
            MediaItem item = new MediaItem { Id = 1, Title = "item" };
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            var fakeCopyService = A.Fake<IMediaItemCopyService>();
            A.CallTo(() => fakeCopyServiceFactory.GetMediaItemCopyService()).Returns(fakeCopyService);
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);
            List<MediaItemCopy> copies = new List<MediaItemCopy>
            {
                copy
            };
            A.CallTo(() => fakeCopyService.GetByItemId(1)).Returns(copies);

            // act
            await presenter.HandleDeleteClicked(null, null);

            // assert
            A.CallTo(() => fakeView.DisplayCopies(copies)).MustHaveHappened();
            A.CallTo(() => fakeCopyService.DeleteById(1)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task HandleSaveNewClicked_Test_Book()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            BookCopy copy = new BookCopy { Id = 1 };
            A.CallTo(() => fakeView.NewCopy).Returns(copy);
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            var fakeCopyService = A.Fake<IBookCopyService>();
            A.CallTo(() => fakeCopyServiceFactory.GetBookCopyService()).Returns(fakeCopyService);
            Book item = new Book { Id = 1 };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);
            List<BookCopy> copies = new List<BookCopy>
            {
                copy
            };
            A.CallTo(() => fakeCopyService.GetByItemId(1)).Returns(copies);

            // act
            await presenter.HandleSaveNewClicked(null, null);

            // assert
            A.CallTo(() => fakeView.DisplayCopies(copies)).MustHaveHappened();
            A.CallTo(() => fakeCopyService.Create(copy)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task HandleSaveNewClicked_Test_MediaItem()
        {
            // arrange
            var fakeView = A.Fake<IManageCopiesForm>();
            MediaItemCopy copy = new MediaItemCopy { Id = 1 };
            A.CallTo(() => fakeView.NewCopy).Returns(copy);
            var fakeCopyServiceFactory = A.Fake<ICopyServiceFactory>();
            var fakeCopyService = A.Fake<IMediaItemCopyService>();
            A.CallTo(() => fakeCopyServiceFactory.GetMediaItemCopyService()).Returns(fakeCopyService);
            MediaItem item = new MediaItem { Id = 1 };
            ManageCopiesPresenter presenter = new ManageCopiesPresenter(fakeView, item, fakeCopyServiceFactory);
            List<MediaItemCopy> copies = new List<MediaItemCopy>
            {
                copy
            };
            A.CallTo(() => fakeCopyService.GetByItemId(1)).Returns(copies);

            // act
            await presenter.HandleSaveNewClicked(null, null);

            // assert
            A.CallTo(() => fakeView.DisplayCopies(copies)).MustHaveHappened();
            A.CallTo(() => fakeCopyService.Create(copy)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }
    }//class
}

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

using MyLibrary.BusinessLogic.Repositories; // TODO: remove

using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.Utils;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class AddMediaItemPresenter_Tests
    {
        [TestCase("notes", "60", ".bmp")]
        [TestCase("notes", "", ".bmp")]
        [TestCase("notes", null, ".bmp")]
        [TestCase("", "60", ".bmp")]
        [TestCase(null, "60", ".bmp")]
        [TestCase("notes", "60", ".jpg")]
        [TestCase("notes", "", ".jpg")]
        [TestCase("notes", null, ".jpg")]
        [TestCase("", "60", ".jpg")]
        [TestCase(null, "60", ".jpg")]
        [TestCase("notes", "60", ".jpeg")]
        [TestCase("notes", "", ".jpeg")]
        [TestCase("notes", null, ".jpeg")]
        [TestCase("", "60", ".jpeg")]
        [TestCase(null, "60", ".jpeg")]
        [TestCase("notes", "60", ".png")]
        [TestCase("notes", "", ".png")]
        [TestCase("notes", null, ".png")]
        [TestCase("", "60", ".png")]
        [TestCase(null, "60", ".png")]
        public void InputFieldsUpdated_Test_Valid_HasImageFilePath(string notesFieldEntry, string runningTimeFieldEntry, string ext)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("new item");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\file" + ext);
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagRepo, fakeView, fakeImageFileReader);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
        }

        [TestCase("notes", "60")]
        [TestCase("notes", "")]
        [TestCase("notes", null)]
        [TestCase("", "60")]
        [TestCase(null, "60")]
        public void InputFieldsValidated_Test_Valid_NoImageFilePath(string notesFieldEntry, string runningTimeFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("new item");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns("");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagRepo, fakeView, fakeImageFileReader);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
        }

        [TestCase("", "0", "0", "0", "notes", ".txt")]
        [TestCase("title", "", "0", "0", "notes", ".txt")]
        [TestCase("title", "", "", "0", "notes", ".txt")]
        [TestCase("title", "", "", "", "notes", ".txt")]
        [TestCase("title", "", "", "", "", ".txt")]
        [TestCase("title", "0", "", "", "notes", ".txt")]
        [TestCase("title", "0", "0", "", "notes", ".txt")]
        [TestCase("title", "", "0", "", "notes", ".txt")]
        [TestCase("", "0", "0", "0", "", ".txt")]
        [TestCase("title", "", "0", "0", "", ".txt")]
        [TestCase("title", "", "", "0", "", ".txt")]
        [TestCase("title", "", "", "", "", ".txt")]
        [TestCase("title", "", "", "", "", ".txt")]
        [TestCase("title", "0", "", "", "", ".txt")]
        [TestCase("title", "0", "0", "", "", ".txt")]
        [TestCase("title", "", "0", "", "", ".txt")]
        [TestCase("", "", "0", "0", "", ".txt")]
        [TestCase("", "", "", "0", "", ".txt")]
        [TestCase("", "", "", "", "", ".txt")]
        [TestCase("", "", "", "", "", ".txt")]
        [TestCase("", "0", "", "", "", ".txt")]
        [TestCase("", "0", "0", "", "", ".txt")]
        [TestCase("", "", "0", "", "", ".txt")]
        [TestCase("", "0", "0", "0", "notes", "bogus file")]
        [TestCase("title", "", "0", "0", "notes", "bogus file")]
        [TestCase("title", "", "", "0", "notes", "bogus file")]
        [TestCase("title", "", "", "", "notes", "bogus file")]
        [TestCase("title", "", "", "", "", "bogus file")]
        [TestCase("title", "0", "", "", "notes", "bogus file")]
        [TestCase("title", "0", "0", "", "notes", "bogus file")]
        [TestCase("title", "", "0", "", "notes", "bogus file")]
        [TestCase("", "0", "0", "0", "", "bogus file")]
        [TestCase("title", "", "0", "0", "", "bogus file")]
        [TestCase("title", "", "", "0", "", "bogus file")]
        [TestCase("title", "", "", "", "", "bogus file")]
        [TestCase("title", "", "", "", "", "bogus file")]
        [TestCase("title", "0", "", "", "", "bogus file")]
        [TestCase("title", "0", "0", "", "", "bogus file")]
        [TestCase("title", "", "0", "", "", "bogus file")]
        [TestCase("", "", "0", "0", "", "bogus file")]
        [TestCase("", "", "", "0", "", "bogus file")]
        [TestCase("", "", "", "", "", "bogus file")]
        [TestCase("", "", "", "", "", "bogus file")]
        [TestCase("", "0", "", "", "", "bogus file")]
        [TestCase("", "0", "0", "", "", "bogus file")]
        [TestCase("", "", "0", "", "", "bogus file")]
        public void InputFieldsUpdated_Test_Invalid_HasImageFilePath(string titleFieldEntry, string numberFieldEntry, string runningTimeFieldEntry, string releaseYearFieldEntry, string notesFieldEntry, 
            string ext)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns(titleFieldEntry);
            A.CallTo(() => fakeView.NumberFieldText).Returns(numberFieldEntry);
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns(releaseYearFieldEntry);
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\file" + ext);
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagRepo, fakeView, fakeImageFileReader);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [TestCase("", "0", "0", "0", "notes")]
        [TestCase("title", "", "0", "0", "notes")]
        [TestCase("title", "", "", "0", "notes")]
        [TestCase("title", "", "", "", "notes")]
        [TestCase("title", "", "", "", "")]
        [TestCase("title", "0", "", "", "notes")]
        [TestCase("title", "0", "0", "", "notes")]
        [TestCase("title", "", "0", "", "notes")]
        [TestCase("", "0", "0", "0", "")]
        [TestCase("title", "", "0", "0", "")]
        [TestCase("title", "", "", "0", "")]
        [TestCase("title", "", "", "", "")]
        [TestCase("title", "", "", "", "")]
        [TestCase("title", "0", "", "", "")]
        [TestCase("title", "0", "0", "", "")]
        [TestCase("title", "", "0", "", "")]
        [TestCase("", "", "0", "0", "")]
        [TestCase("", "", "", "0", "")]
        [TestCase("", "", "", "", "")]
        [TestCase("", "", "", "", "")]
        [TestCase("", "0", "", "", "")]
        [TestCase("", "0", "0", "", "")]
        [TestCase("", "", "0", "", "")]
        public void InputFieldsUpdated_Test_Invalid_NoImageFilePath(string titleFieldEntry, string numberFieldEntry, string runningTimeFieldEntry, string releaseYearFieldEntry, string notesFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns(titleFieldEntry);
            A.CallTo(() => fakeView.NumberFieldText).Returns(numberFieldEntry);
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns(releaseYearFieldEntry);
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns("");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagRepo, fakeView, fakeImageFileReader);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [TestCase("", "", "")]
        [TestCase("", "", @"C:\path\to\file.jpg")]
        [TestCase("", "notes", @"C:\path\to\file.jpg")]
        [TestCase("60", "notes", @"C:\path\to\file.jpg")]
        [TestCase("60", "", @"C:\path\to\file.jpg")]
        [TestCase("", "", @"C:\path\to\file.jpeg")]
        [TestCase("", "notes", @"C:\path\to\file.jpeg")]
        [TestCase("60", "notes", @"C:\path\to\file.jpeg")]
        [TestCase("60", "", @"C:\path\to\file.jpeg")]
        [TestCase("", "", @"C:\path\to\file.bmp")]
        [TestCase("", "notes", @"C:\path\to\file.bmp")]
        [TestCase("60", "notes", @"C:\path\to\file.bmp")]
        [TestCase("60", "", @"C:\path\to\file.bmp")]
        [TestCase("", "", @"C:\path\to\file.png")]
        [TestCase("", "notes", @"C:\path\to\file.png")]
        [TestCase("60", "notes", @"C:\path\to\file.png")]
        [TestCase("60", "", @"C:\path\to\file.png")]
        [TestCase("60", "notes", "")]
        [TestCase("60", "", "")]
        public void SaveButtonClicked_Test_ItemAlreadyExists(string runningTimeFieldEntry, string notesFieldEntry, string imageFilePathFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePathFieldEntry);
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.ExistsWithTitle("title")).Returns(true);
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagRepo, fakeView, fakeImageFileReader);

            // act
            presenter.SaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("title")).MustHaveHappened();
        }

        [TestCase("", "", "")]
        [TestCase("", "", @"C:\path\to\file.jpg")]
        [TestCase("", "notes", @"C:\path\to\file.jpg")]
        [TestCase("60", "notes", @"C:\path\to\file.jpg")]
        [TestCase("60", "", @"C:\path\to\file.jpg")]
        [TestCase("", "", @"C:\path\to\file.jpeg")]
        [TestCase("", "notes", @"C:\path\to\file.jpeg")]
        [TestCase("60", "notes", @"C:\path\to\file.jpeg")]
        [TestCase("60", "", @"C:\path\to\file.jpeg")]
        [TestCase("", "", @"C:\path\to\file.bmp")]
        [TestCase("", "notes", @"C:\path\to\file.bmp")]
        [TestCase("60", "notes", @"C:\path\to\file.bmp")]
        [TestCase("60", "", @"C:\path\to\file.bmp")]
        [TestCase("", "", @"C:\path\to\file.png")]
        [TestCase("", "notes", @"C:\path\to\file.png")]
        [TestCase("60", "notes", @"C:\path\to\file.png")]
        [TestCase("60", "", @"C:\path\to\file.png")]
        [TestCase("60", "notes", "")]
        [TestCase("60", "", "")]
        public void SaveButtonClicked_Test_ErrorWhenCheckingIfExists(string runningTimeFieldEntry, string notesFieldEntry, string imageFilePathFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePathFieldEntry);
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.ExistsWithTitle("title")).Throws(new Exception("error"));
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagRepo, fakeView, fakeImageFileReader);

            // act
            presenter.SaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Error checking title.", "error")).MustHaveHappened();
        }

        [TestCase("", "", @"C:\path\to\file.jpg")]
        [TestCase("", "notes", @"C:\path\to\file.jpg")]
        [TestCase("60", "notes", @"C:\path\to\file.jpg")]
        [TestCase("60", "", @"C:\path\to\file.jpg")]
        [TestCase("", "", @"C:\path\to\file.jpeg")]
        [TestCase("", "notes", @"C:\path\to\file.jpeg")]
        [TestCase("60", "notes", @"C:\path\to\file.jpeg")]
        [TestCase("60", "", @"C:\path\to\file.jpeg")]
        [TestCase("", "", @"C:\path\to\file.bmp")]
        [TestCase("", "notes", @"C:\path\to\file.bmp")]
        [TestCase("60", "notes", @"C:\path\to\file.bmp")]
        [TestCase("60", "", @"C:\path\to\file.bmp")]
        [TestCase("", "", @"C:\path\to\file.png")]
        [TestCase("", "notes", @"C:\path\to\file.png")]
        [TestCase("60", "notes", @"C:\path\to\file.png")]
        [TestCase("60", "", @"C:\path\to\file.png")]
        public void SaveButtonClicked_Test_ErrorWhenReadingImage(string runningTimeFieldEntry, string notesFieldEntry, string imageFilePathFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePathFieldEntry);
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.ExistsWithTitle("title")).Returns(false);
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Throws(new System.IO.IOException("error"));
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagRepo, fakeView, fakeImageFileReader);

            // act
            presenter.SaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Image file error", "error")).MustHaveHappened();
        }
    }//class

    class MockPresenter : AddMediaItemPresenter
    {
        public MockPresenter(IMediaItemService mediaItemService, TagRepository tagRepo, IAddMediaItemForm view, IImageFileReader imageFileReader)
            :base(mediaItemService, tagRepo, view, imageFileReader)
        {

        }
    }//class
}

﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.Utils;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    public class AddMediaItemPresenter_Tests
    {
        [Test]
        public async Task PopulateTagsList_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            List<Tag> tags = new List<Tag> { new Tag { Name = "tag" } };
            A.CallTo(() => fakeTagService.GetAll()).Returns(tags);
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

            // act
            await presenter.PopulateTagsList();

            // assert
            Assert.IsFalse(presenter.GetAllTagsValueByKey("tag"));
        }

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
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

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
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

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
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

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
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

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
        public async Task SaveButtonClicked_Test_ItemAlreadyExists(string runningTimeFieldEntry, string notesFieldEntry, string imageFilePathFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePathFieldEntry);
            A.CallTo(() => fakeView.SelectedCategory).Returns("Dvd");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i => i.Title == "title"))).Returns(false);
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("title")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
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
        public async Task SaveButtonClicked_Test_ErrorWhenReadingImage(string runningTimeFieldEntry, string notesFieldEntry, string imageFilePathFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePathFieldEntry);
            A.CallTo(() => fakeView.SelectedCategory).Returns("Dvd");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i => i.Title == "title"))).Returns(true);
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Throws(new System.IO.IOException("error"));
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Image file error", "error")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
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
        public async Task SaveButtonClicked_Test_Success(string runningTimeFieldEntry, string notesFieldEntry, string imageFilePathFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePathFieldEntry);
            A.CallTo(() => fakeView.SelectedCategory).Returns("Dvd");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i => i.Title == "title"))).Returns(true);
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i =>
            i.Title == "title" &&
            i.Number == 0123456789 &&
            i.ReleaseYear == 2021 &&
            i.Notes == notesFieldEntry &&
            i.Type == ItemType.Dvd))).MustHaveHappened();
            A.CallTo(() => fakeView.CloseDialog()).MustHaveHappened();
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
        public async Task SaveButtonClicked_Test_ErrorAddingItem(string runningTimeFieldEntry, string notesFieldEntry, string imageFilePathFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePathFieldEntry);
            A.CallTo(() => fakeView.SelectedCategory).Returns("Dvd");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i => i.Title == "title"))).Throws(new Exception("error"));
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Error creating item", "error")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public void HandleAddNewTagClicked_Test_NoEntry()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeAddTagDialogProvider = A.Fake<INewTagOrPublisherInputBoxProvider>();
            var fakeAddTagDialog = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeAddTagDialogProvider.Get()).Returns(fakeAddTagDialog);
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowTagAlreadyExistsDialog("")).MustNotHaveHappened();
        }

        [Test]
        public void HandleAddNewTagClicked_Test_TagAlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeAddTagDialogProvider = A.Fake<INewTagOrPublisherInputBoxProvider>();
            var fakeAddTagDialog = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeAddTagDialog.ShowAsDialog()).Returns("tag1");
            A.CallTo(() => fakeAddTagDialogProvider.Get()).Returns(fakeAddTagDialog);
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            presenter.SetAllTags(allTags);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowTagAlreadyExistsDialog("tag1")).MustHaveHappened();
        }

        [Test]
        public void HandleAddNewTagClicked_Test_TagDoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeAddTagDialogProvider = A.Fake<INewTagOrPublisherInputBoxProvider>();
            var fakeAddTagDialog = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeAddTagDialog.ShowAsDialog()).Returns("tag2");
            A.CallTo(() => fakeAddTagDialogProvider.Get()).Returns(fakeAddTagDialog);
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            presenter.SetAllTags(allTags);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            Assert.IsTrue(presenter.GetAllTagsValueByKey("tag2"));
        }

        [Test]
        public void HandleTagCheckedChanged_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            List<string> selectedTags = new List<string> { "tag1" };
            List<string> unselectedTags = new List<string> { "tag2" };
            A.CallTo(() => fakeView.SelectedTags).Returns(selectedTags);
            A.CallTo(() => fakeView.UnselectedTags).Returns(unselectedTags);
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeView, fakeImageFileReader, null);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", false);
            allTags.Add("tag2", true);
            presenter.SetAllTags(allTags);

            // act
            presenter.HandleTagCheckedChanged(null, null);

            // assert
            Assert.IsTrue(presenter.GetAllTagsValueByKey("tag1"));
            Assert.IsFalse(presenter.GetAllTagsValueByKey("tag2"));
        }
    }//class

    class MockPresenter : AddMediaItemPresenter
    {
        public MockPresenter(IMediaItemService mediaItemService, ITagService tagService, IAddMediaItemForm view, IImageFileReader imageFileReader,
            INewTagOrPublisherInputBoxProvider newTagOrPublisherInputBoxProvider)
            : base(mediaItemService, tagService, view, imageFileReader, newTagOrPublisherInputBoxProvider)
        {

        }

        public void SetAllTags(Dictionary<string, bool> allTags)
        {
            this._allTags = allTags;
        }

        public bool GetAllTagsValueByKey(string key)
        {
            return this._allTags[key];
        }
    }//class
}

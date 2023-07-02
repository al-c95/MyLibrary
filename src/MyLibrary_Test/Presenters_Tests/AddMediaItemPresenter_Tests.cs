//MIT License

//Copyright (c) 2021-2023

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

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
using MyLibrary.Models.Entities.Builders;
using System;

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
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, null);

            // act
            await presenter.PopulateTagsAsync();

            // assert
            Assert.IsFalse(presenter.GetAllTagsValueByKey("tag"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(@"C:\path\to\image.png")]
        [TestCase(@"C:\path\to\image.jpg")]
        [TestCase(@"C:\path\to\image.jpeg")]
        [TestCase(@"C:\path\to\image.bmp")]
        public void InputFieldsUpdated_Test_Valid(string imageFilePath)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.SelectedCategory).Returns("4k BluRay");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemBuilder = A.Fake<IMediaItemBuilder>();
            A.CallTo(() => fakeMediaItemBuilder.Build()).Returns(new MediaItem 
            {
                Title = "Title",
                ReleaseYear=2023,
                RunningTime=60,
                Number=0123
            });
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemBuilder, fakeView, fakeImageFileReader, null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(@"C:\path\to\image.png")]
        [TestCase(@"C:\path\to\image.jpg")]
        [TestCase(@"C:\path\to\image.jpeg")]
        [TestCase(@"C:\path\to\image.bmp")]
        public void InputFieldsUpdated_Test_InvalidItemDetails_ValidImageFilePath(string path)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedCategory).Returns("4k BluRay");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemBuilder = A.Fake<IMediaItemBuilder>();
            A.CallTo(() => fakeMediaItemBuilder.WithTitle("")).Throws<InvalidOperationException>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemBuilder, fakeView, fakeImageFileReader, null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [TestCase(@"C:\path\to\file.docx")]
        [TestCase(@"C:\path\to\<>:|?*file.jpeg")]
        public void InputFieldsUpdated_Test_ValidItemDetails_InvalidImageFilePath(string path)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.SelectedCategory).Returns("4k BluRay");
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(path);
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemBuilder = A.Fake<IMediaItemBuilder>();
            A.CallTo(() => fakeMediaItemBuilder.Build()).Returns(new MediaItem
            {
                Title = "Title",
                ReleaseYear = 2023,
                RunningTime = 60,
                Number = 0123
            });
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemBuilder, fakeView, fakeImageFileReader, null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [TestCase(@"C:\path\to\file.docx")]
        [TestCase(@"C:\path\to\<>:|?*file.jpeg")]
        public void InputFieldsUpdated_Test_AllInvalid(string path)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedCategory).Returns("4k BluRay");
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(path);
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemBuilder = A.Fake<IMediaItemBuilder>();
            A.CallTo(() => fakeMediaItemBuilder.WithTitle("")).Throws<InvalidOperationException>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemBuilder, fakeView, fakeImageFileReader, null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ItemAlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns("some notes");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns("60");
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\image.jpeg");
            A.CallTo(() => fakeView.SelectedCategory).Returns("Dvd");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i => i.Title == "title"))).Returns(false);
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Returns(new byte[0]);
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, new MediaItemBuilder(), fakeView, fakeImageFileReader, null);
            presenter.InputFieldsUpdated(null, null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("title")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ItemDoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns("some notes");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns("60");
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\image.jpeg");
            A.CallTo(() => fakeView.SelectedCategory).Returns("Dvd");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i => i.Title == "title"))).Returns(true);
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Returns(new byte[0]);
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, new MediaItemBuilder(), fakeView, fakeImageFileReader, null);
            Dictionary<string,bool> allTags = new Dictionary<string,bool>();
            allTags.Add("tag1", true);
            presenter.SetAllTags(allTags);
            presenter.InputFieldsUpdated(null, null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("title")).MustNotHaveHappened();
        }

        [TestCase(@"C:\path\to\image.png")]
        [TestCase(@"C:\path\to\image.jpg")]
        [TestCase(@"C:\path\to\image.jpeg")]
        [TestCase(@"C:\path\to\image.bmp")]
        public async Task SaveButtonClicked_Test_ErrorReadingImage(string imageFilePath)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns("80");
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns("notes");
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePath);
            A.CallTo(() => fakeView.SelectedCategory).Returns("Dvd");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i => i.Title == "title"))).Returns(true);
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Throws(new System.IO.IOException("error"));
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, new MediaItemBuilder(), fakeView, fakeImageFileReader, null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Image file error", "error")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ErrorAddingItem()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns("80");
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns("test");
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedCategory).Returns("Dvd");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(A<MediaItem>.That.Matches(i => i.Title == "title"))).Throws(new Exception("error"));
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, new MediaItemBuilder(), fakeView, fakeImageFileReader, null);
            presenter.InputFieldsUpdated(null, null);

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
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);

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
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);
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
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);
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
            MockPresenter presenter = new MockPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, null);
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
        public MockPresenter(IMediaItemService mediaItemService, ITagService tagService, IMediaItemBuilder mediaItemBuilder, IAddMediaItemForm view, IImageFileReader imageFileReader,
            INewTagOrPublisherInputBoxProvider newTagOrPublisherInputBoxProvider)
            : base(mediaItemService, tagService, mediaItemBuilder, view, imageFileReader, newTagOrPublisherInputBoxProvider)
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
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

using System;
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
using MyLibrary.Models.Entities.Factories;
using System.Linq;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    public class AddMediaItemPresenter_Tests
    {
        [Test]
        public async Task PopulateTagsAsync_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.FilterTagsFieldEntry).Returns("");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            List<Tag> tags = new List<Tag> { new Tag { Name = "tag" } };
            A.CallTo(() => fakeTagService.GetAll()).Returns(tags);
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, null);

            // act
            await presenter.PopulateTagsAsync();

            // assert
            Assert.AreEqual(1, presenter.AllTags.Count);
            Assert.IsFalse(presenter.AllTags["tag"]);
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
            A.CallTo(() => fakeView.TitleFieldText).Returns("Item");
            A.CallTo(() => fakeView.SelectedCategory).Returns("4k BluRay");
            A.CallTo(() => fakeView.NotesFieldText).Returns("this is a test");
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2023");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns("60");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemFactory = A.Fake<IMediaItemFactory>();
            List<string> selectedTags = new List<string>
            {
                "tag1",
                "tag2",
                "tag3"
            };
            A.CallTo(() => fakeView.SelectedTags).Returns(selectedTags);
            A.CallTo(() => fakeMediaItemFactory.Create("Item", "0123", "2023", "60", A<List<string>>.That.IsSameSequenceAs(new List<string> { "tag1", "tag3" }))).Returns(new MediaItem
            {
                Title="Item",
                ReleaseYear=2023,
                RunningTime=60,
                Number=0123,
                Tags=new List<Tag>()
                {
                    new Tag{Name="tag1"},
                    new Tag{Name="tag3"}
                }
            });        
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemFactory, fakeView, fakeImageFileReader, null);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            allTags.Add("tag2", false);
            allTags.Add("tag3", true);
            presenter.AllTags = allTags;

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.AreEqual("this is a test", presenter.NewItem.Notes);
            Assert.AreEqual(2023, presenter.NewItem.ReleaseYear);
            Assert.AreEqual(0123, presenter.NewItem.Number);
            Assert.AreEqual(60, presenter.NewItem.RunningTime);
            Assert.AreEqual("Item", presenter.NewItem.Title);
            Assert.AreEqual(2, presenter.NewItem.Tags.Count);
            Assert.IsTrue(presenter.NewItem.Tags.Any(t => t.Name == "tag1"));
            Assert.IsTrue(presenter.NewItem.Tags.Any(t => t.Name == "tag3"));
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
            A.CallTo(() => fakeView.NotesFieldText).Returns("");
            A.CallTo(() => fakeView.YearFieldEntry).Returns("");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns("");
            A.CallTo(() => fakeView.NumberFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag1" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemFactory = A.Fake<IMediaItemFactory>();
            A.CallTo(() => fakeMediaItemFactory.Create("", "", "", "", A<List<string>>.That.IsSameSequenceAs(new List<string> { "tag1" }))).Throws(new InvalidOperationException());
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemFactory, fakeView, fakeImageFileReader, null);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            allTags.Add("tag2", false);
            presenter.AllTags = allTags;

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
            A.CallTo(() => fakeView.TitleFieldText).Returns("Item");
            A.CallTo(() => fakeView.SelectedCategory).Returns("4k BluRay");
            A.CallTo(() => fakeView.NotesFieldText).Returns("this is a test");
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2023");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns("60");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123");
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(path);
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag1" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemFactory = A.Fake<IMediaItemFactory>();
            A.CallTo(() => fakeMediaItemFactory.Create("Item", "0123", "2023", "60", A<List<string>>.That.IsSameSequenceAs(new List<string> { "tag1" }))).Returns(new MediaItem
            {
                Title = "Item",
                ReleaseYear = 2023,
                RunningTime = 60,
                Number = 0123
            });
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemFactory, fakeView, fakeImageFileReader, null);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            allTags.Add("tag2", false);
            presenter.AllTags = allTags;

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
            A.CallTo(() => fakeView.NotesFieldText).Returns("");
            A.CallTo(() => fakeView.YearFieldEntry).Returns("");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns("");
            A.CallTo(() => fakeView.NumberFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag1" });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemFactory = A.Fake<IMediaItemFactory>();
            A.CallTo(() => fakeMediaItemFactory.Create("", "", "", "", A<List<string>>.That.IsSameSequenceAs(new List<string> { "tag1" }))).Throws(new InvalidOperationException());
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemFactory, fakeView, fakeImageFileReader, null);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            allTags.Add("tag2", false);
            presenter.AllTags = allTags;

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
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\image.jpeg");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemFactory = A.Fake<IMediaItemFactory>();
            MediaItem newItem = new MediaItem()
            {
                Title="title",
                Number=0123456789,
                ReleaseYear=2021,
                Notes="some notes",
                RunningTime=60,
                Type=ItemType.Dvd
            };
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemFactory, fakeView, fakeImageFileReader, null);
            presenter.NewItem= newItem;
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(presenter.NewItem)).Returns(false);

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
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\image.jpeg");
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemFactory = A.Fake<IMediaItemFactory>();
            MediaItem newItem = new MediaItem
            {
                Title = "title",
                Number = 0123456789,
                ReleaseYear = 2021,
                RunningTime = 60,
                Type = ItemType.Dvd
            };
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Returns(new byte[0]);
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemFactory, fakeView, fakeImageFileReader, null);
            presenter.NewItem = newItem;
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(presenter.NewItem)).Returns(true);

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
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imageFilePath);
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemFactory = A.Fake<IMediaItemFactory>();
            MediaItem newItem = new MediaItem
            {
                Title = "title",
                Number = 0123456789,
                RunningTime = 80,
                ReleaseYear = 2021
            };
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(newItem)).Returns(true);
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Throws(new System.IO.IOException("error"));
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemFactory, fakeView, fakeImageFileReader, null);
            presenter.NewItem = newItem;

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
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeMediaItemFactory = A.Fake<IMediaItemFactory>();
            MediaItem newItem = new MediaItem
            {
                Title = "title",
                Number = 0123456789,
                RunningTime = 80,
                ReleaseYear = 2021
            };
            A.CallTo(() => fakeMediaItemService.AddIfNotExistsAsync(newItem)).Throws(new Exception("error"));
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, fakeMediaItemFactory, fakeView, fakeImageFileReader, null);
            presenter.NewItem = newItem;

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
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);

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
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            presenter.AllTags = allTags;

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
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, fakeAddTagDialogProvider);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            presenter.AllTags = allTags;

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            Assert.IsTrue(presenter.AllTags["tag2"]);
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
            AddMediaItemPresenter presenter = new AddMediaItemPresenter(fakeMediaItemService, fakeTagService, null, fakeView, fakeImageFileReader, null);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", false);
            allTags.Add("tag2", true);
            presenter.AllTags = allTags;

            // act
            presenter.HandleTagCheckedChanged(null, null);

            // assert
            Assert.IsTrue(presenter.AllTags["tag1"]);
            Assert.IsFalse(presenter.AllTags["tag2"]);
        }
    }//class
}
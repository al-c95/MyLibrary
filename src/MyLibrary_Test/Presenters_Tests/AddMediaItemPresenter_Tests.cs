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
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Presenters;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class AddMediaItemPresenter_Tests
    {
        [TestCase("notes", "60")]
        [TestCase("notes", "")]
        [TestCase("notes", null)]
        [TestCase("", "60")]
        [TestCase(null, "60")]
        public void InputFieldsUpdated_Test_Valid(string notesFieldEntry, string runningTimeFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("new item");
            A.CallTo(() => fakeView.NumberFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns("2021");
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            var fakeMediaItemRepo = A.Fake<MediaItemRepository>();
            var fakeTagRepo = A.Fake<TagRepository>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemRepo, fakeTagRepo, fakeView);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
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
        public void InputFieldsUpdated_Test_Invalid(string titleFieldEntry, string numberFieldEntry, string runningTimeFieldEntry, string releaseYearFieldEntry, string notesFieldEntry)
        {
            // arrange
            var fakeView = A.Fake<IAddMediaItemForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns(titleFieldEntry);
            A.CallTo(() => fakeView.NumberFieldText).Returns(numberFieldEntry);
            A.CallTo(() => fakeView.RunningTimeFieldEntry).Returns(runningTimeFieldEntry);
            A.CallTo(() => fakeView.YearFieldEntry).Returns(releaseYearFieldEntry);
            A.CallTo(() => fakeView.NotesFieldText).Returns(notesFieldEntry);
            var fakeMediaItemRepo = A.Fake<MediaItemRepository>();
            var fakeTagRepo = A.Fake<TagRepository>();
            MockPresenter presenter = new MockPresenter(fakeMediaItemRepo, fakeTagRepo, fakeView);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }
    }//class

    public class MockPresenter : AddMediaItemPresenter
    {
        public MockPresenter(MediaItemRepository mediaItemRepo, TagRepository tagRepo, IAddMediaItemForm view)
            :base(mediaItemRepo, tagRepo, view)
        {

        }
    }//class
}

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
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.Utils;

namespace MyLibrary_Test.Presenters_Tests
{
    public class NewTagOrPublisherInputPresenter_Tests
    {
        [Test]
        public void Ctor_Test_Tag()
        {
            // arrange
            var fakeView = A.Fake<INewTagOrPublisher>();

            // act
            var presenter = new NewTagOrPublisherInputPresenter(fakeView, NewTagOrPublisherInputPresenter.InputBoxMode.Tag);

            // assert
            Assert.AreEqual("New Tag", fakeView.DialogTitle);
            Assert.AreEqual("New Tag:", fakeView.Label);
        }

        [Test]
        public void Ctor_Test_Publisher()
        {
            // arrange
            var fakeView = A.Fake<INewTagOrPublisher>();

            // act
            var presenter = new NewTagOrPublisherInputPresenter(fakeView, NewTagOrPublisherInputPresenter.InputBoxMode.Publisher);

            // assert
            Assert.AreEqual("New Publisher", fakeView.DialogTitle);
            Assert.AreEqual("New Publisher:", fakeView.Label);
        }

        [Test]
        public void InputChanged_Test_Tag_Valid()
        {
            // arrange
            var fakeView = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeView.Entry).Returns("tag");
            var presenter = new NewTagOrPublisherInputPresenter(fakeView, NewTagOrPublisherInputPresenter.InputBoxMode.Tag);

            // act
            presenter.InputChanged(null, null);

            // assert
            Assert.IsTrue(fakeView.OkButtonEnabled);
        }

        [Test]
        public void InputChanged_Test_Tag_Invalid()
        {
            // arrange
            var fakeView = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeView.Entry).Returns("tag,");
            var presenter = new NewTagOrPublisherInputPresenter(fakeView, NewTagOrPublisherInputPresenter.InputBoxMode.Tag);

            // act
            presenter.InputChanged(null, null);

            // assert
            Assert.IsFalse(fakeView.OkButtonEnabled);
        }

        [Test]
        public void InputChanged_Test_Publisher_Valid()
        {
            // arrange
            var fakeView = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeView.Entry).Returns("publisher");
            var presenter = new NewTagOrPublisherInputPresenter(fakeView, NewTagOrPublisherInputPresenter.InputBoxMode.Publisher);

            // act
            presenter.InputChanged(null, null);

            // assert
            Assert.IsTrue(fakeView.OkButtonEnabled);
        }

        [Test]
        public void InputChanged_Test_Publisher_Invalid()
        {
            // arrange
            var fakeView = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeView.Entry).Returns("");
            var presenter = new NewTagOrPublisherInputPresenter(fakeView, NewTagOrPublisherInputPresenter.InputBoxMode.Publisher);

            // act
            presenter.InputChanged(null, null);

            // assert
            Assert.IsFalse(fakeView.OkButtonEnabled);
        }
    }//class
}

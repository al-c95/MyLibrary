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
    public class NewAuthorInputPresenter_Tests
    {
        [TestCase("John", "Smith")]
        [TestCase("John H.", "Smith")]
        public void InputChanged_Test_Valid(string firstNameEntry, string lastNameEntry)
        {
            // arrange
            var fakeView = A.Fake<INewAuthor>();
            A.CallTo(() => fakeView.FirstNameEntry).Returns(firstNameEntry);
            A.CallTo(() => fakeView.LastNameEntry).Returns(lastNameEntry);
            var presenter = new NewAuthorInputPresenter(fakeView);

            // act
            presenter.InputChanged(null, null);

            // assert
            Assert.IsTrue(fakeView.OkButtonEnabled);
        }

        [TestCase("", "")]
        [TestCase("John", "Smith1")]
        [TestCase("John1", "Smith")]
        [TestCase("John", "")]
        [TestCase("", "Smith")]
        public void InputChanged_Test_Invalid(string firstNameEntry, string lastNameEntry)
        {
            // arrange
            var fakeView = A.Fake<INewAuthor>();
            A.CallTo(() => fakeView.FirstNameEntry).Returns(firstNameEntry);
            A.CallTo(() => fakeView.LastNameEntry).Returns(lastNameEntry);
            var presenter = new NewAuthorInputPresenter(fakeView);

            // act
            presenter.InputChanged(null, null);

            // assert
            Assert.IsFalse(fakeView.OkButtonEnabled);
        }
    }//class
}

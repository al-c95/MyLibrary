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
using MyLibrary;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.Utils;

namespace MyLibrary_Test.Presenters_Tests
{
    public class TipOfTheDayPresenter_Tests
    {
        [Test]
        public void ShowAllClicked_Test_InitiallyShowingOne()
        {
            // arrange
            var fakeDialog = A.Fake<ITipOfTheDay>();
            string[] tips = new string[]
            {
                "tip1",
                "tip2",
                "tip3"
            };
            A.CallTo(() => fakeDialog.ShowNextButtonEnabled).Returns(true);
            TipOfTheDayPresenter presenter = new TipOfTheDayPresenter(fakeDialog, tips);
            string expectedResult = "1. tip1\r\n\r\n" +
                "2. tip2\r\n\r\n" +
                "3. tip3\r\n\r\n";

            // act
            presenter.ShowAllClicked(null, null);
            string actualResult = fakeDialog.TipsText;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual("Show All", fakeDialog.ShowAllButtonText);
        }

        [Test]
        public void ShowAllClicked_Test_InitiallyShowingAll()
        {
            // arrange
            var fakeDialog = A.Fake<ITipOfTheDay>();
            string[] tips = new string[]
            {
                "tip1",
                "tip2",
                "tip3"
            };
            A.CallTo(() => fakeDialog.ShowNextButtonEnabled).Returns(false);
            MockTipOfTheDayPresenter presenter = new MockTipOfTheDayPresenter(fakeDialog, tips);
            presenter.Index = 0;
            string expectedResult = "1. tip1";

            // act
            presenter.ShowAllClicked(null, null);
            string actualResult = fakeDialog.TipsText;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual("Show One", fakeDialog.ShowAllButtonText);
        }
        
        [Test]
        public void NextClicked_Test_GoToNextTip()
        {
            // arrange
            var fakeDialog = A.Fake<ITipOfTheDay>();
            string[] tips = new string[]
            {
                "tip1",
                "tip2",
                "tip3"
            };
            MockTipOfTheDayPresenter presenter = new MockTipOfTheDayPresenter(fakeDialog, tips);
            presenter.Index = 0;

            // act
            presenter.NextClicked(null, null);

            // assert
            Assert.AreEqual(1, presenter.Index);
            Assert.AreEqual("2. tip2", fakeDialog.TipsText);
        }

        [Test]
        public void NextClicked_Test_GoToFirstTip()
        {
            // arrange
            var fakeDialog = A.Fake<ITipOfTheDay>();
            string[] tips = new string[]
            {
                "tip1",
                "tip2",
                "tip3"
            };
            MockTipOfTheDayPresenter presenter = new MockTipOfTheDayPresenter(fakeDialog, tips);
            presenter.Index = 2;

            // act
            presenter.NextClicked(null, null);

            // assert
            Assert.AreEqual(0, presenter.Index);
            Assert.AreEqual("1. tip1", fakeDialog.TipsText);
        }

        class MockTipOfTheDayPresenter : TipOfTheDayPresenter
        {
            public MockTipOfTheDayPresenter(ITipOfTheDay view, string[] tips)
                :base(view, tips)
            {

            }

            public int Index
            {
                get => this._index;
                set => this._index = value;
            }
        }//class
    }
}
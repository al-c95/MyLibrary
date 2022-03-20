using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.ValueObjects;

namespace MyLibrary_Test.Models_Tests.ValueObjects_Tests
{
    [TestFixture]
    class AppVersion_Tests
    {
        [Test]
        public void Constructor_Test()
        {
            AppVersion version = new AppVersion(1, 2, 3);

            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(2, version.Minor);
            Assert.AreEqual(3, version.Revision);
        }

        [Test]
        public void ToString_Test()
        {
            // arrange
            AppVersion version = new AppVersion(1, 2, 3);
            string expectedResult = "1.2.3";

            // act
            string actualResult = version.ToString();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(1,1,1,1,1,1,true)]
        [TestCase(1,1,1,1,1,2,false)]
        [TestCase(1,1,1,1,2,2,false)]
        [TestCase(1,1,1,1,2,1,false)]
        [TestCase(1,1,1,2,1,2,false)]
        [TestCase(1,1,1,2,2,2,false)]
        public void Equals_Test(int majorA, int minorA, int revisionA,
                                int majorB, int minorB, int revisionB,
            bool expectedResult)
        {
            // arrange
            AppVersion a = new AppVersion(majorA, minorA, revisionA);
            AppVersion b = new AppVersion(majorB, minorB, revisionB);

            // act
            bool actualResult = a.Equals(b);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void EqualsOperator_Test_BothNull()
        {
            // arrange
            AppVersion a = null;
            AppVersion b = null;

            // act/assert
            Assert.IsTrue(a == b);
        }

        [Test]
        public void EqualsOperator_Test_OneNull()
        {
            // arrange
            AppVersion a = null;
            AppVersion b = new AppVersion(1, 2, 3);

            // act/assert
            Assert.IsFalse(a == b);
        }
        
        [Test]
        public void EqualsOperator_Test_NeitherNullAreEqual()
        {
            // arrange
            AppVersion a = new AppVersion(1, 2, 3);
            AppVersion b = new AppVersion(1, 2, 3);

            // act/assert
            Assert.IsTrue(a == b);
        }

        [Test]
        public void EqualsOperator_Test_NeitherNullAreNotEqual()
        {
            // arrange
            AppVersion a = new AppVersion(1, 2, 3);
            AppVersion b = new AppVersion(4, 5, 6);

            // act/assert
            Assert.IsFalse(a == b);
        }

        [Test]
        public void NotEqualsOperator_Test_Equal()
        {
            // arrange
            AppVersion a = new AppVersion(1, 2, 3);
            AppVersion b = new AppVersion(1, 2, 3);

            // act/assert
            Assert.IsFalse(a != b);
        }

        [Test]
        public void NotEqualsOperator_Test_NotEqual()
        {
            // arrange
            AppVersion a = new AppVersion(1, 2, 3);
            AppVersion b = new AppVersion(4, 5, 6);

            // act/assert
            Assert.IsTrue(a != b);
        }

        [Test]
        public void GetHashCode_Test()
        {
            // arrange
            AppVersion version = new AppVersion(1, 2, 3);
            int expectedResult = Tuple.Create(1, 2, 3).GetHashCode();

            // act
            int actualResult = version.GetHashCode();

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

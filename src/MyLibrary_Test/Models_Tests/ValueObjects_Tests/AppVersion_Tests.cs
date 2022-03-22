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
            AppVersion lhs = new AppVersion(1, 2, 3);
            AppVersion rhs = new AppVersion(4, 5, 6);

            // act/assert
            Assert.IsTrue(lhs != rhs);
        }

        [TestCase("1.1.0","1.1.1")]
        [TestCase("1.0.0", "1.1.1")]
        [TestCase("0.0.0", "1.1.1")]
        [TestCase("0.0.1", "1.1.1")]
        [TestCase("0.1.0", "1.1.1")]
        [TestCase("1.0.1", "1.1.1")]
        public void LessThanOperator_Test_IsLessThan(string lhs, string rhs)
        {
            Assert.IsTrue(AppVersion.Parse(lhs) < AppVersion.Parse(rhs));
        }

        [TestCase("1.1.1", "1.1.1")]
        [TestCase("2.1.1", "1.1.1")]
        [TestCase("2.2.1", "1.1.1")]
        [TestCase("2.2.2", "1.1.1")]
        [TestCase("1.1.2", "1.1.1")]
        [TestCase("1.2.2", "1.1.1")]
        public void LessThanOperator_Test_NotLessThan(string lhs, string rhs)
        {
            Assert.IsFalse(AppVersion.Parse(lhs) < AppVersion.Parse(rhs));
        }

        [TestCase("1.1.1", "1.1.1")]
        [TestCase("1.0.0", "1.1.1")]
        [TestCase("0.0.0", "1.1.1")]
        [TestCase("0.0.1", "1.1.1")]
        [TestCase("0.1.0", "1.1.1")]
        [TestCase("1.0.1", "1.1.1")]
        public void LessThanOrEqualToOperator_Test_True(string lhs, string rhs)
        {
            Assert.IsTrue(AppVersion.Parse(lhs) <= AppVersion.Parse(rhs));
        }

        [TestCase("2.1.1", "1.1.1")]
        [TestCase("2.2.1", "1.1.1")]
        [TestCase("2.2.2", "1.1.1")]
        [TestCase("1.1.2", "1.1.1")]
        [TestCase("1.2.2", "1.1.1")]
        public void LessThanOrEqualToOperator_Test_False(string lhs, string rhs)
        {
            Assert.IsFalse(AppVersion.Parse(lhs) <= AppVersion.Parse(rhs));
        }

        [TestCase("2.2.2", "1.1.1")]
        [TestCase("2.2.1", "1.1.1")]
        [TestCase("2.1.1", "1.1.1")]
        [TestCase("2.1.2", "1.1.1")]
        [TestCase("1.2.1", "1.1.1")]
        [TestCase("1.1.2", "1.1.1")]
        public void GreaterThan_Test_True(string lhs, string rhs)
        {
            Assert.IsTrue(AppVersion.Parse(lhs) > AppVersion.Parse(rhs));
        }

        [TestCase("2.2.2", "2.2.2")]
        [TestCase("1.2.2", "2.2.2")]
        [TestCase("1.1.2", "2.2.2")]
        [TestCase("1.1.1", "2.2.2")]
        [TestCase("2.1.1", "2.2.2")]
        [TestCase("2.2.1", "2.2.2")]
        public void GreaterThan_Test_False(string lhs, string rhs)
        {
            Assert.IsFalse(AppVersion.Parse(lhs) > AppVersion.Parse(rhs));
        }

        [TestCase("2.2.2", "1.1.1")]
        [TestCase("2.2.1", "1.1.1")]
        [TestCase("2.1.1", "1.1.1")]
        [TestCase("2.1.2", "1.1.1")]
        [TestCase("1.2.1", "1.1.1")]
        [TestCase("1.1.2", "1.1.1")]
        [TestCase("1.1.1", "1.1.1")]
        public void GreaterThanOrEqualTo_Test_True(string lhs, string rhs)
        {
            Assert.IsTrue(AppVersion.Parse(lhs) >= AppVersion.Parse(rhs));
        }

        [TestCase("1.0.0", "1.1.1")]
        [TestCase("0.0.0", "1.1.1")]
        [TestCase("0.0.1", "1.1.1")]
        [TestCase("0.1.0", "1.1.1")]
        [TestCase("1.0.1", "1.1.1")]
        public void GreaterThanOrEqualTo_Test_False(string lhs, string rhs)
        {
            Assert.IsFalse(AppVersion.Parse(lhs) >= AppVersion.Parse(rhs));
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

        [TestCase("bogus version")]
        [TestCase("1.2.f")]
        [TestCase("1.2.3f")]
        [TestCase("1.2.3 test")]
        public void Parse_Test_InvalidFormat(string version)
        {
            Assert.Throws<FormatException>(() => AppVersion.Parse(version));
        }

        [Test]
        public void Parse_Test_Successful()
        {
            // arrange
            string version = "1.2.3";

            // act
            AppVersion result = AppVersion.Parse(version);

            // assert
            Assert.IsTrue(result.Major == 1);
            Assert.IsTrue(result.Minor == 2);
            Assert.IsTrue(result.Revision == 3);
        }
    }
}

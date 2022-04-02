using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary;

namespace MyLibrary_Test
{
    [TestFixture]
    public class Configuration_Tests
    {
        [Test]
        public void APP_DESCRIPTION_Test()
        {
            // arrange
            string expectedResult = "Application for keeping track of books and other \"library\" items." + "\r\n" + "\r\n" +
                "Thanks To: " + "\r\n" +
                "Newtonsoft.Json 13.0.1 by James Newton-King" + "\r\n" +
                "Dapper 2.0.123 by Sam Saffron, Marc Gravell and Nick Craver" + "\r\n" +
                "EPPlus 5.8.8 by EPPlus Software AB" + "\r\n" +
                "CircularProgressBar 2.8.0.16 by Soroush Falahati" + "\r\n" +
                "WinFormAnimation 1.6.0.4 by Soroush Falahati" + "\r\n" +
                "Microsoft.Data.Sqlite.Core by Microsoft" + "\r\n" +
                "EntityFramework 6.4.4 by Microsoft\r\n";

            // act
            string actualResult = Configuration.APP_DESCRIPTION;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

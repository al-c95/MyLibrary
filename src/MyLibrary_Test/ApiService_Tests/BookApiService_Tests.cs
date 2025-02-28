﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.ApiService;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.ApiService_Tests
{
    [TestFixture]
    class BookApiService_Tests
    {
        [Test]
        public void GetBookByIsbnAsync_Test_NotFound()
        {
            // arrange
            var fakeIsbnApiClient = A.Fake<IIsbnApiClient>();
            var fakeAuthorApiClient = A.Fake<IAuthorApiClient>();
            var fakeIsbnHttpResponse = A.Fake<HttpResponseWrapper>();
            A.CallTo(() => fakeIsbnHttpResponse.StatusCode).Returns(System.Net.HttpStatusCode.NotFound);
            A.CallTo(() => fakeIsbnApiClient.GetResponse("0123456789")).Returns(fakeIsbnHttpResponse);
            BookApiService service = new BookApiService(fakeIsbnApiClient, fakeAuthorApiClient);

            // act/assert
            Assert.ThrowsAsync<BookNotFoundException>(() => service.GetBookByIsbnAsync("0123456789"));
        }

        [Test]
        public async Task GetBookByIsbnAsync_Test_SuccessNoAuthorsNoPropertiesExist()
        {
            // arrange
            string bookJson = "{" + "\r\n" +
                "\"title\": \"Test book: this book is a test\"," + "\r\n" +
                "}";
            var fakeIsbnApiClient = A.Fake<IIsbnApiClient>();
            var fakeIsbnHttpResponse = A.Fake<HttpResponseWrapper>();
            A.CallTo(() => fakeIsbnHttpResponse.StatusCode).Returns(System.Net.HttpStatusCode.OK);
            A.CallTo(() => fakeIsbnHttpResponse.ReadAsStringAsync()).Returns(bookJson);
            A.CallTo(() => fakeIsbnApiClient.GetResponse("0123456789")).Returns(fakeIsbnHttpResponse);
            var fakeAuthorApiClient = A.Fake<IAuthorApiClient>();
            BookApiService service = new BookApiService(fakeIsbnApiClient, fakeAuthorApiClient);

            // act
            Book result = await service.GetBookByIsbnAsync("0123456789");

            // assert
            // title
            Assert.AreEqual("Test book: this book is a test", result.Title);
            // ISBNs
            Assert.AreEqual("", result.Isbn);
            Assert.AreEqual("", result.Isbn13);
            // number of pages
            Assert.AreEqual(0, result.Pages);
            // publication details
            Assert.AreEqual("No Publisher", result.Publisher.Name);
            Assert.AreEqual("", result.DatePublished);
            Assert.AreEqual("", result.PlaceOfPublication);
            // authors
            Assert.IsTrue(result.Authors.Count == 0);
        }

        [Test]
        public async Task GetBookByIsbnAsync_Test_Success_AuthorsAndAllPropertiesExist()
        {
            // arrange
            string bookJson = "{\r\n" +
    "\"publishers\": [\r\n" +
    "   \"some_publisher\"\r\n" +
    "   ],\r\n" +
    "   \"source_records\": [\r\n" +
    "       \"amazon:0123456789\"," + "\r\n" +
    "       \"bwb:0123456789012\"" + "\r\n" +
    "],\r\n" +
    "\"publish_places\": [" + "\r\n" +
    "   \"somewhere\"\r\n" +
    "],\r\n" +
    "\"title\": \"Test book: this book is a test\"," + "\r\n" +
    "\"number_of_pages\": 100," + "\r\n" +
    "\"covers\": [" + "\r\n" +
    "   8403152" + "\r\n" +
    "],\r\n" +
    "\"isbn_13\": [" + "\r\n" +
    "   \"0123456789012\"" + "\r\n" +
    "],\r\n" +
    "\"isbn_10\": [" + "\r\n" +
    "   \"0-123-45678-9\"" + "\r\n" +
    "],\r\n" +
    "\"publish_date\": \"Oct 28, 2017\"," + "\r\n" +
    "\"key\": \"/books/OL00000000M\"," + "\r\n" +
    "\"authors\": [" + "\r\n" +
    "   {\r\n" +
    "       \"key\": \"/authors/OL0000001A\"" + "\r\n" +
    "   },\r\n" +
    "   {\r\n" +
    "       \"key\": \"/authors/OL0000002A\"" + "\r\n" +
    "   }\r\n" +
    "],\r\n" +
    "\"works\": [" + "\r\n" +
    "   {\r\n" +
    "       \"key\": \"/works/OL00000000W\"" + "\r\n" +
    "   }\r\n" +
    "],\r\n" +
    "\"type\": {" + "\r\n" +
    "   \"key\": \"/type/edition\"" + "\r\n" +
    "}," + "\r\n" +
    "\"lc_classifications\": [" + "\r\n" +
    "   \"QA76.00.C000\"" + "\r\n" +
    "],\r\n" +
    "\"latest_revision\": 3," + "\r\n" +
    "\"revision\": 3," + "\r\n" +
    "\"created\": {" + "\r\n" +
    "   \"type\": \"/type/datetime\"," + "\r\n" +
    "   \"value\": \"2019-04-05T03:55:17.228685\"" + "\r\n" +
    "}," + "\r\n" +
    "\"last_modified\": {" + "\r\n" +
    "   \"type\": \"/type/datetime\"," + "\r\n" +
    "   \"value\": \"2021-10-03T19:54:17.544188\"" + "\r\n" +
    "}\r\n" + "\r\n" +
    "}";
            var fakeIsbnApiClient = A.Fake<IIsbnApiClient>();
            var fakeIsbnHttpResponse = A.Fake<HttpResponseWrapper>();
            A.CallTo(() => fakeIsbnHttpResponse.StatusCode).Returns(System.Net.HttpStatusCode.OK);
            A.CallTo(() => fakeIsbnHttpResponse.ReadAsStringAsync()).Returns(bookJson);
            A.CallTo(() => fakeIsbnApiClient.GetResponse("0123456789")).Returns(fakeIsbnHttpResponse);

            string author1Json = "{\r\n" +
    "\"name\": \"John Smith\"," + "\r\n" +
    "\"last_modified\": {" + "\r\n" +
    "   \"type\": \"/type/datetime\"," + "\r\n" +
    "   \"value\": \"2008-04-29 13:35:46.87638\"" + "\r\n" +
    "}," + "\r\n" +
    "\"key\": \"/authors/OL0000001A\"," + "\r\n" +
    "\"type\": {" + "\r\n" +
    "   \"key\": \"/type/author\"" + "\r\n" +
    "}," + "\r\n" +
    "\"id\": 00000001," + "\r\n" +
    "\"revision\": 1" + "\r\n" +
"}";
            string author2Json = "{\r\n" +
    "\"name\": \"Jane Doe\"," + "\r\n" +
    "\"last_modified\": {" + "\r\n" +
    "   \"type\": \"/type/datetime\"," + "\r\n" +
    "   \"value\": \"2008-04-29 13:35:46.87638\"" + "\r\n" +
    "}," + "\r\n" +
    "\"key\": \"/authors/OL0000002A\"," + "\r\n" +
    "\"type\": {" + "\r\n" +
    "   \"key\": \"/type/author\"" + "\r\n" +
    "}," + "\r\n" +
    "\"id\": 00000002," + "\r\n" +
    "\"revision\": 1" + "\r\n" +
"}";
            var fakeAuthorApiClient = A.Fake<IAuthorApiClient>();
            var fakeAuthor1HttpResponse = A.Fake<HttpResponseWrapper>();
            A.CallTo(() => fakeAuthor1HttpResponse.ReadAsStringAsync()).Returns(author1Json);
            A.CallTo(() => fakeAuthor1HttpResponse.StatusCode).Returns(System.Net.HttpStatusCode.OK);
            A.CallTo(() => fakeAuthorApiClient.GetResponse("/authors/OL0000001A")).Returns(fakeAuthor1HttpResponse);
            var fakeAuthor2HttpResponse = A.Fake<HttpResponseWrapper>();
            A.CallTo(() => fakeAuthor2HttpResponse.ReadAsStringAsync()).Returns(author2Json);
            A.CallTo(() => fakeAuthor2HttpResponse.StatusCode).Returns(System.Net.HttpStatusCode.OK);
            A.CallTo(() => fakeAuthorApiClient.GetResponse("/authors/OL0000002A")).Returns(fakeAuthor2HttpResponse);

            BookApiService service = new BookApiService(fakeIsbnApiClient, fakeAuthorApiClient);

            // act
            Book result = await service.GetBookByIsbnAsync("0123456789");

            // assert
            // title
            Assert.AreEqual("Test book: this book is a test", result.Title);
            // ISBNs
            Assert.AreEqual("0123456789", result.Isbn);
            Assert.AreEqual("0123456789012", result.Isbn13);
            // number of pages
            Assert.AreEqual(100, result.Pages);
            // publication details
            Assert.AreEqual("some_publisher", result.Publisher.Name);
            Assert.AreEqual("Oct 28, 2017", result.DatePublished);
            Assert.AreEqual("somewhere", result.PlaceOfPublication);
            // authors
            Assert.IsTrue(result.Authors.Count == 2);
            Assert.IsTrue(result.Authors.Any(a => a.FirstName.Equals("John") && a.LastName.Equals("Smith")));
            Assert.IsTrue(result.Authors.Any(a => a.FirstName.Equals("Jane") && a.LastName.Equals("Doe")));
        }
    }//class
}

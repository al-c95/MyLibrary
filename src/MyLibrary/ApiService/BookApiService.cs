using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;

namespace MyLibrary.ApiService
{
    /// <summary>
    /// Retrieves book information, by isbn.
    /// </summary>
    public class BookApiService : IDisposable
    {
        // injected values
        protected readonly IIsbnApiClient _isbnApiClient;
        protected readonly IAuthorApiClient _authorApiClient;

        /// <summary>
        /// Constructor with dependency injection of API clients.
        /// </summary>
        /// <param name="isbnApiClient"></param>
        /// <param name="authorApiClient"></param>
        public BookApiService(IIsbnApiClient isbnApiClient, IAuthorApiClient authorApiClient)
        {
            this._isbnApiClient = isbnApiClient;
            this._authorApiClient = authorApiClient;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BookApiService()
        {
            this._isbnApiClient = new IsbnApiClient();
            this._authorApiClient = new AuthorApiClient();
        }

        /// <summary>
        /// Retrieve a book by isbn.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public async Task<Book> GetBookByIsbnAsync(string isbn)
        {
            // retrieve and parse the book JSON data
            // currently we are assuming all properties exist
            var bookJson = await this._isbnApiClient.GetAsJson(isbn);
            JObject bookJsonObj = JObject.Parse(bookJson);
            string title = (string)bookJsonObj["title"];
            string publisherName = (string)bookJsonObj["publishers"][0];
            string placeOfPublication = (string)bookJsonObj["publish_places"][0];
            string _isbn = (string)bookJsonObj["isbn_10"][0];
            string _isbn13 = (string)bookJsonObj["isbn_13"][0];
            string publishDate = (string)bookJsonObj["publish_date"];
            int pages = (int)bookJsonObj["number_of_pages"];

            // retrieve and parse the authors JSON data
            var authorsJsons = await GetAuthorsJsonAsync((JArray)bookJsonObj["authors"]);
            List<Author> authors = new List<Author>();
            foreach (var authorJson in authorsJsons)
            {
                JObject authorJsonObj = JObject.Parse(authorJson); 
                string authorName = (string)authorJsonObj["name"];

                authors.Add(new Author(authorName));
            }

            // create the object
            Book book = new Book();
            book.Title = title;
            book.Publisher = new Publisher(publisherName);
            book.DatePublished = publishDate;
            book.PlaceOfPublication = placeOfPublication;
            book.Isbn = _isbn;
            book.Isbn13 = _isbn13;
            book.Pages = pages;
            book.Authors = authors;

            return book;
        }//GetBookByIsbnAsync

        private async Task<IEnumerable<string>> GetAuthorsJsonAsync(JArray authorsArray)
        {
            List<string> authorJsons = new List<string>();

            foreach (var authorEntry in authorsArray)
            {
                string authorKey = (string)authorEntry["key"];
                authorJsons.Add(await this._authorApiClient.GetAsJson(authorKey));
            }

            return authorJsons.AsEnumerable();
        }

        private string GetJsonProperty(JObject obj, string prop)
        {
            if (obj.Property(prop) != null)
            {
                return (string)obj.Property(prop).Value;
            }
            else
            {
                return "";
            }
        }

        public void Dispose()
        {
            this._isbnApiClient.Dispose();
            this._authorApiClient.Dispose();
        }
    }//class
}

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
    public class BookApiService : IDisposable, IBookApiService
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
            // TODO: update unit tests

            // make a request for the book data
            HttpResponseWrapper response = await this._isbnApiClient.GetResponse(isbn);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                // something went wrong
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // could not find book
                    throw new BookNotFoundException(isbn);
                }
            }

            // parse the book JSON data
            string bookJson = await response.ReadAsStringAsync();
            JObject bookJsonObj = JObject.Parse(bookJson);
            string title = (string)bookJsonObj["title"];
            string publisherName;
            if (JsonPropertyExists(bookJsonObj, "publishers"))
            {
                publisherName = (string)bookJsonObj["publishers"][0];
            }
            else
            {
                publisherName = "No Publisher";
            }
            string placeOfPublication;
            if (JsonPropertyExists(bookJsonObj, "publish_places"))
            {
                placeOfPublication = (string)bookJsonObj["publish_places"][0];
            }
            else
            {
                placeOfPublication = "";
            }
            string _isbn;
            if (JsonPropertyExists(bookJsonObj, "isbn_10"))
            {
                _isbn = (string)bookJsonObj["isbn_10"][0];
            }
            else
            {
                _isbn = "";
            }
            string _isbn13;
            if (JsonPropertyExists(bookJsonObj, "isbn_13"))
            {
                _isbn13 = (string)bookJsonObj["isbn_13"][0];
            }
            else
            {
                _isbn13 = "";
            }
            string publishDate;
            if (JsonPropertyExists(bookJsonObj, "publish_date"))
            {
                publishDate = (string)bookJsonObj["publish_date"];
            }
            else
            {
                publishDate = "";
            }
            int pages;
            if (JsonPropertyExists(bookJsonObj, "number_of_pages"))
            {
                pages = (int)bookJsonObj["number_of_pages"];
            }
            else
            {
                pages = 0;
            }

            // retrieve and parse the authors JSON data
            List<Author> authors = new List<Author>();
            if (JsonPropertyExists(bookJsonObj, "authors"))
            {
                var authorsJsons = await GetAuthorsJsonAsync((JArray)bookJsonObj["authors"]);
                foreach (var authorJson in authorsJsons)
                {
                    JObject authorJsonObj = JObject.Parse(authorJson);
                    string authorName = (string)authorJsonObj["name"];

                    authors.Add(new Author(authorName));
                }
            }

            // create the object
            Book book = new Book();
            book.Title = title;
            book.TitleLong = title;
            book.Publisher = new Publisher(publisherName);
            book.DatePublished = publishDate;
            book.PlaceOfPublication = placeOfPublication;
            book.Isbn = _isbn;
            book.Isbn13 = _isbn13;
            book.Pages = pages;
            book.Authors = authors;
            // set language to English by default
            book.Language = "English";

            return book;
        }//GetBookByIsbnAsync

        private async Task<IEnumerable<string>> GetAuthorsJsonAsync(JArray authorsArray)
        {
            List<string> authorJsons = new List<string>();

            foreach (var authorEntry in authorsArray)
            {
                string authorKey = (string)authorEntry["key"];

                var response = await this._authorApiClient.GetResponse(authorKey);
                string authorJson = await response.ReadAsStringAsync();

                authorJsons.Add(authorJson);
            }

            return authorJsons.AsEnumerable();
        }

        private bool JsonPropertyExists(JObject obj, string prop)
        {
            JToken token = obj[prop];
            if (token is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Dispose()
        {
            this._isbnApiClient.Dispose();
            this._authorApiClient.Dispose();
        }
    }//class
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities.Builders
{
    public sealed class BookBuilder
    {
        private Book book;

        private BookBuilder(string title, string titleLong)
        {
            this.book = new Book
            { 
                Title = title, 
                TitleLong = titleLong 
            };
        }

        private BookBuilder(int id, string title, string titleLong)
            : this(title, titleLong)
        {
            this.book.Id = id;
        }

        public static BookBuilder CreateBook(string title, string titleLong, Publisher publisher, string language, int pages)
        {
            return new BookBuilder(title, titleLong)
                .PublishedBy(publisher)
                .WrittenInLanguage(language)
                .Pages(pages);
        }

        public BookBuilder WithIsbn(string isbn)
        {
            if (Regex.IsMatch(isbn, Book.ISBN_10_PATTERN) || string.IsNullOrWhiteSpace(isbn))
            {
                this.book.Isbn = isbn;
            }
            else
            {
                throw new FormatException("ISBN: " + isbn + " does not have appropriate format.");
            }

            return this;
        }

        public BookBuilder WithIsbn13(string isbn)
        {
            if (Regex.IsMatch(isbn, Book.ISBN_13_PATTERN) || string.IsNullOrWhiteSpace(isbn))
            {
                this.book.Isbn13 = isbn;
            }
            else
            {
                throw new FormatException("ISBN: " + isbn + " does not have appropriate format.");
            }

            return this;
        }

        public BookBuilder WithDeweyDecimal(string deweyDecimal)
        {
            decimal? value;
            if (string.IsNullOrWhiteSpace(deweyDecimal))
            {
                value = null;
            }
            else
            {
                if (Regex.IsMatch(deweyDecimal, Book.DEWEY_DECIMAL_PATTERN))
                {
                    value = decimal.Parse(deweyDecimal);
                }
                else
                {
                    throw new FormatException("Dewey decimal: " + deweyDecimal + " does not have appropriate format.");
                }
            }
            this.book.DeweyDecimal = value;

            return this;
        }

        public BookBuilder InFormat(string format)
        {
            this.book.Format = format;
            return this;
        }

        public BookBuilder PublishedIn(string datePublished)
        {
            this.book.DatePublished = datePublished;
            return this;
        }

        public BookBuilder Edition(string edition)
        {
            this.book.Edition = edition;
            return this;
        }

        public BookBuilder Pages(int pages)
        {
            this.book.Pages = pages;
            return this;
        }

        public BookBuilder Sized(string dimensions)
        {
            this.book.Dimensions = dimensions;
            return this;
        }

        public BookBuilder WithOverview(string overview)
        {
            this.book.Overview = overview;
            return this;
        }

        public BookBuilder WithMsrp(string msrp)
        {
            this.book.Msrp = msrp;
            return this;
        }

        public BookBuilder WithExcerpt(string excerpt)
        {
            this.book.Excerpt = excerpt;
            return this;
        }

        public BookBuilder WithSynopsys(string synopsys)
        {
            this.book.Synopsys = synopsys;
            return this;
        }

        public BookBuilder WrittenBy(Author author)
        {
            this.book.Authors.Add(author);
            return this;
        }

        public BookBuilder PublishedBy(Publisher publisher)
        {
            this.book.Publisher = publisher;
            return this;
        }

        public BookBuilder PublishedAt(string placeOfPublication)
        {
            this.book.PlaceOfPublication = placeOfPublication;
            return this;
        }

        public BookBuilder WrittenInLanguage(string language)
        {
            this.book.Language = language;
            return this;
        }

        public BookBuilder WithTags(IEnumerable<Tag> tags)
        {
            foreach (var t in tags)
                this.book.Tags.Add(t);

            return this;
        }

        public BookBuilder WithAuthors(IEnumerable<Author> authors)
        {
            foreach (var a in authors)
                this.book.Authors.Add(a);

            return this;
        }

        public Book Get() => this.book;
    }//class
}

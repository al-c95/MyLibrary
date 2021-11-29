using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities.Builders
{
    public sealed class BookBuilder
    {
        private Book book;

        private BookBuilder(string title, string titleLong)
        {
            this.book = new Book { Title = title, TitleLong = titleLong };
        }

        private BookBuilder(int id, string title, string titleLong)
            : this(title, titleLong)
        {
            this.book.Id = id;
        }

        public static BookBuilder CreateBook(string title, string titleLong)
        {
            return new BookBuilder(title, titleLong);
        }

        public BookBuilder WithIsbn(string isbn)
        {
            switch (isbn.Length)
            {
                case 10:
                    this.book.Isbn = isbn;
                    break;
                case 13:
                    this.book.Isbn13 = isbn;
                    break;
                default:
                    throw new ArgumentException("ISBN must have 10 or 13 digits.");
            }

            return this;
        }

        public BookBuilder WithDeweyDecimal(double deweyDecimal)
        {
            this.book.DeweyDecimal = deweyDecimal;
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

        public BookBuilder WithTags(IEnumerable<Tag> tags)
        {
            foreach (var t in tags)
                this.book.Tags.Add(t);

            return this;
        }

        public Book Get() => this.book;
    }
}

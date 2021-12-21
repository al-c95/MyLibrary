using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public sealed class Book : Item
    {
        public override ItemType Type { get => ItemType.Book; set => throw new InvalidOperationException("Cannot change Book.Type."); }

        public Book()
        {
            this.Authors = new List<Author> { };
        }

        private string _titleLong;
        public string TitleLong
        {
            get => this._titleLong;
            set => this._titleLong = value;
        }

        // ensure 10 or 13 digits for now
        // TODO: allow dashes (hence it is a string)
        public static readonly string ISBN_10_PATTERN = @"^\d{10}$";
        public static readonly string ISBN_13_PATTERN = @"^\d{13}$";
        // TODO: ensure at least one of the isbn fields is populated
        private string _isbn;
        public string Isbn
        {
            get => this._isbn;
            set
            {
                if (Regex.IsMatch(value, ISBN_10_PATTERN) || string.IsNullOrWhiteSpace(value))
                    this._isbn = value;
                else
                    throw new FormatException("Isbn: " + value + " has incorrect format.");
            } 
        }

        private string _isbn13;
        public string Isbn13
        {
            get => this._isbn13;
            set
            {
                if (Regex.IsMatch(value, ISBN_13_PATTERN) || string.IsNullOrWhiteSpace(value))
                    this._isbn13 = value;
                else
                    throw new FormatException("Isbn: " + value + " has incorrect format.");
            } 
        }

        public string GetIsbn()
        {
            if (string.IsNullOrWhiteSpace(Isbn) && !(string.IsNullOrWhiteSpace(Isbn13)))
            {
                return Isbn13;
            }
            else if (string.IsNullOrWhiteSpace(Isbn13) && !(string.IsNullOrWhiteSpace(Isbn)))
            {
                return Isbn;
            }
            else if (string.IsNullOrWhiteSpace(Isbn13) && (string.IsNullOrWhiteSpace(Isbn)))
            {
                return "";
            }
            else
            {
                return (Isbn + "; " + Isbn13);
            }
        }

        public static readonly string DEWEY_DECIMAL_PATTERN = @"^\d+\.?\d*$";
        public decimal? DeweyDecimal 
        {
            get;
            set;
        }

        public string Format { get; set; }
        public string DatePublished { get; set; }
        public string Edition { get; set; }
        public int Pages { get; set; }
        public string Dimensions { get; set; }
        public string Overview { get; set; }
        public string Language { get; set; }

        public string Msrp { get; set; }
        public string Excerpt { get; set; }
        public string Synopsys { get; set; }

        public ICollection<Author> Authors { get; set; }
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Get a string of the authors in the format: Lastname, F. and Lastname, F. .... and Lastname, F.
        /// </summary>
        /// <returns></returns>
        public string GetAuthorList()
        {
            if (this.Authors.Count == 0)
                return null;

            StringBuilder authors = new StringBuilder();
            foreach (var author in this.Authors)
            {
                authors.Append(author.GetFullNameWithFirstInitial() + " and ");
            }
            string authorsString = authors.ToString().Substring(0, authors.ToString().Length-5);

            return authorsString;
        }//GetAuthorList

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());

            builder.AppendLine();
            builder.AppendLine();

            builder.AppendLine("ISBN: ");
            builder.AppendLine(this.Isbn);
            builder.AppendLine();

            builder.AppendLine("ISBN 13: ");
            builder.AppendLine(this.Isbn13);
            builder.AppendLine();

            builder.AppendLine("Long Title: ");
            builder.AppendLine(this.TitleLong);
            builder.AppendLine();

            builder.AppendLine("Dewey Decimal: ");
            if (!(DeweyDecimal is null))
            {
                builder.AppendLine(this.DeweyDecimal.ToString());
                builder.AppendLine();
            }
            else
            {
                builder.AppendLine(); 
                builder.AppendLine();
            }

            builder.AppendLine("Format: ");
            builder.AppendLine(this.Format);
            builder.AppendLine();

            builder.AppendLine("Publisher: ");
            builder.AppendLine(this.Publisher.Name);
            builder.AppendLine();

            builder.AppendLine("Date Published: ");
            builder.AppendLine(this.DatePublished);
            builder.AppendLine();

            builder.AppendLine("Edition: ");
            builder.AppendLine(this.Edition);
            builder.AppendLine();

            builder.AppendLine("Pages: ");
            builder.AppendLine(this.Pages.ToString());
            builder.AppendLine();

            builder.AppendLine("Dimensions: ");
            builder.AppendLine(this.Dimensions);
            builder.AppendLine();

            builder.AppendLine("Overview: ");
            builder.AppendLine(this.Overview);
            builder.AppendLine();

            builder.AppendLine("Language: ");
            builder.AppendLine(this.Language);
            builder.AppendLine();

            builder.AppendLine("MSRP: ");
            builder.AppendLine(this.Msrp);
            builder.AppendLine();

            builder.AppendLine("Excerpt: ");
            builder.AppendLine(this.Excerpt);
            builder.AppendLine();

            builder.AppendLine("Synopsys: ");
            builder.AppendLine(this.Synopsys);
            builder.AppendLine();

            builder.AppendLine("Authors: ");
            builder.Append(this.GetAuthorList());

            return builder.ToString();
        }//ToString
    }//class
}
